using System.Net;
using System.Text;
using System.Text.Json;

using SMSC.Exceptions;
using SMSC.Http;
using SMSC.Responses;
using SMSC.Types;
using SMSC.Types.Enums;

namespace SMSC.Test;

/// <summary>
/// Локальные проверки без обращения к API SMSC.
/// </summary>
public class HttpSmsValidationTest
{
    private static HttpSms CreateSender() => new(new ProviderConfiguration("test-api-key"));

    [Fact]
    public async Task SendSms_Throws_WhenMessageExceedsMaxLength()
    {
        var sender = CreateSender();
        var message = new string('a', 1001);

        await Assert.ThrowsAsync<MessageMaxLengthException>(() =>
            sender.SendSms("+79999999999", message, new SmsConfiguration()));
    }

    [Fact]
    public async Task SendSms_Throws_WhenWhatsAppBotNameMissing()
    {
        var sender = CreateSender();
        var config = new SmsConfiguration { SmsType = SmsType.WhatsApp };

        await Assert.ThrowsAnyAsync<ArgumentException>(() =>
            sender.SendSms("+79999999999", "Hello", config));
    }

    [Fact]
    public async Task SendSms_Throws_WhenWhatsAppBotNameInvalid()
    {
        var sender = CreateSender();
        var config = new SmsConfiguration
        {
            SmsType = SmsType.WhatsApp,
            BotName = "@not_whatsapp_bot"
        };

        await Assert.ThrowsAnyAsync<ArgumentException>(() =>
            sender.SendSms("+79999999999", "Hello", config));
    }

    [Fact]
    public async Task SendGroupSms_Throws_WhenGroupIdContainsDelimiter()
    {
        var sender = CreateSender();

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            sender.SendGroupSms("1,2", "Hello", new SmsConfiguration()));
    }

    [Fact]
    public void StatusConfiguration_BotProperty_DefaultsToFalse()
    {
        var config = new SmsStatusConfiguration { Id = "1" };
        Assert.False(config.Bot);
    }

    [Fact]
    public void StatusResponse_HasDtmfProperty()
    {
        var response = new Responses.HttpSmsStatusResponse { Dtmf = "1234" };
        Assert.Equal("1234", response.Dtmf);
    }

    [Fact]
    public async Task SendSms_Throws_WhenTelegramCodeIsNotFourToEightDigits()
    {
        var sender = CreateSender();
        var config = new SmsConfiguration { SmsType = SmsType.Telegram };

        await Assert.ThrowsAsync<ArgumentException>(() =>
            sender.SendSms("+79999999999", "test", config));
    }

    [Fact]
    public async Task SendSms_TelegramBot_OmitsTgParameter()
    {
        var probe = new RequestProbe();
        var sender = new HttpSms(new ProviderConfiguration("test-api-key"), new HttpClient(probe));
        var config = new SmsConfiguration
        {
            SmsType = SmsType.Telegram,
            BotName = "@mybot_bot"
        };

        await sender.SendSms("+79999999999", "Hello", config);

        Assert.Contains("bot=@mybot_bot", probe.RequestUri, StringComparison.Ordinal);
        Assert.DoesNotContain("tg=", probe.RequestUri, StringComparison.Ordinal);
    }

    [Fact]
    public async Task SendSms_TelegramCode_SendsTgParameter()
    {
        var probe = new RequestProbe();
        var sender = new HttpSms(new ProviderConfiguration("test-api-key"), new HttpClient(probe));

        await sender.SendSms("+79999999999", "123456", new SmsConfiguration { SmsType = SmsType.Telegram });

        Assert.Contains("tg=1", probe.RequestUri, StringComparison.Ordinal);
        Assert.DoesNotContain("bot=", probe.RequestUri, StringComparison.Ordinal);
    }

    [Fact]
    public async Task CheckSms_BotStatus_SendsPhoneIdAndBot()
    {
        var probe = new RequestProbe();
        var status = new HttpSmsStatus(new ProviderConfiguration("test-api-key"), new HttpClient(probe));

        await status.CheckSms("+79999999999", new SmsStatusConfiguration { Id = "15", Bot = true });

        Assert.Contains("bot=1", probe.RequestUri, StringComparison.Ordinal);
        Assert.Contains("id=15", probe.RequestUri, StringComparison.Ordinal);
        Assert.Contains("phone=+79999999999", probe.RequestUri, StringComparison.Ordinal);
    }

    [Fact]
    public void StatusResponse_MapsCurrentMessageTypeCodes()
    {
        var telegram = JsonSerializer.Deserialize<HttpSmsStatusResponse>("""{"status":1,"type":6,"id":"42"}""");
        var viber = JsonSerializer.Deserialize<HttpSmsStatusResponse>("""{"status":1,"type":10}""");
        var socials = JsonSerializer.Deserialize<HttpSmsStatusResponse>("""{"status":1,"type":12}""");
        var bots = JsonSerializer.Deserialize<HttpSmsStatusResponse>("""{"status":1,"type":13}""");

        Assert.Equal(SmsType.Telegram, telegram!.MessageType);
        Assert.Equal("42", telegram.Id);
        Assert.Equal(SmsType.Viber, viber!.MessageType);
        Assert.Equal(SmsType.Socials, socials!.MessageType);
        Assert.Equal(SmsType.Bots, bots!.MessageType);
    }

    private sealed class RequestProbe : HttpMessageHandler
    {
        public string RequestUri { get; private set; } = string.Empty;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestUri = request.RequestUri?.ToString() ?? string.Empty;
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("""{"id":"1","cnt":1,"status":1}""", Encoding.UTF8, "application/json")
            };
            return Task.FromResult(response);
        }
    }
}
