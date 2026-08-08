using SMSC.Exceptions;
using SMSC.Http;
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
}
