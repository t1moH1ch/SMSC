using System.Text;

using Microsoft.Extensions.Primitives;

using SMSC.Exceptions.HttpSms;
using SMSC.Http;
using SMSC.Types;
using SMSC.Types.Enums;

namespace SMSC.Test;

public class HttpSmsTest
{
    private const string Phone1 = FakeSmscHttpMessageHandler.Phone1;
    private const string Phone2 = FakeSmscHttpMessageHandler.Phone2;

    [Fact]
    public async Task TestDefaultSmsSendWithApiKey()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var res = await smsSender.SendSms(Phone1, new StringValues("Hello"), new SmsConfiguration());

        Assert.NotNull(res);
        Assert.False(res.Id is null);
        Assert.False(res.SmsCount is null);
    }

    [Fact]
    public async Task TestDefaultSmsSendWithLoginAndPassword()
    {
        var smsSender = SmscTestFactory.CreateSms(SmscTestFactory.LoginConfig());
        var res = await smsSender.SendSms(Phone2, new StringValues("Hello"), new SmsConfiguration());

        Assert.NotNull(res);
        Assert.False(res.Id is null);
        Assert.False(res.SmsCount is null);
    }

    [Fact]
    public async Task TestDefaultSmsSendWithInvalidLogin()
    {
        var providerConfig = new ProviderConfiguration("bad-login", FakeSmscHttpMessageHandler.Password);
        var smsSender = SmscTestFactory.CreateSms(providerConfig);

        await Assert.ThrowsAsync<HttpSmsWrongCredentialsException>(() =>
            smsSender.SendSms(Phone1, new StringValues("Hello"), new SmsConfiguration { Id = Guid.NewGuid().ToString() }));
    }

    [Fact]
    public async Task TestDefaultSmsSendWithInvalidPassword()
    {
        var providerConfig = new ProviderConfiguration(FakeSmscHttpMessageHandler.Login, "bad-password");
        var smsSender = SmscTestFactory.CreateSms(providerConfig);

        await Assert.ThrowsAsync<HttpSmsWrongCredentialsException>(() =>
            smsSender.SendSms(Phone2, new StringValues("Hello"), new SmsConfiguration()));
    }

    [Fact]
    public async Task TestCallSmsSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { SmsType = SmsType.Call };

        var res = await smsSender.SendSms(Phone1, "123456", smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestCallSmsVoiceFemale1Send()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            SmsType = SmsType.Call,
            FullErrorPhonesResponse = true
        };
        smsConfig.CallConfiguration.Voice = Voice.Female1;

        var res = await smsSender.SendSms(Phone2, "123456", smsConfig);
        Assert.NotNull(res);
        Assert.NotNull(res.Phones);
        Assert.Single(res.Phones);
    }

    [Fact]
    public async Task TestCallSmsVoiceMaleWithEnglishLanguageSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            SmsType = SmsType.Call,
            FullErrorPhonesResponse = true
        };
        smsConfig.CallConfiguration.Voice = Voice.Male3;
        smsConfig.CallConfiguration.VoiceLanguage = "en";

        var res = await smsSender.SendSms(Phone1, "123456", smsConfig);
        Assert.NotNull(res);
        Assert.NotNull(res.Phones);
        Assert.Single(res.Phones);
    }

    [Fact]
    public async Task TestHLRSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { SmsType = SmsType.HLR };

        var res = await smsSender.SendSms(Phone2, "123456", smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestMMSSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            SmsType = SmsType.MMS,
            Subject = "Test"
        };

        var res = await smsSender.SendSms(Phone1, "Hello", smsConfig);
        Assert.NotNull(res);
        Assert.Equal(1, res.SmsCount);
    }

    [Fact]
    public async Task TestMMSSendWithFilesInBody()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            SmsType = SmsType.MMS,
            Subject = "Test"
        };

        using var multipart = new MultipartFormDataContent();
        using var file = new FileStream("./images/f18ab673b66711ef9871ca21692ca115_1.jpeg", FileMode.Open, FileAccess.Read);
        multipart.Add(new StreamContent(file), "file", "test.jpeg");
        smsConfig.Files = multipart;

        var res = await smsSender.SendSms(Phone2, "Hello", smsConfig);
        Assert.NotNull(res);
        Assert.Equal(1, res.SmsCount);
    }

    [Fact]
    public async Task TestMMSSendWithFileUrl()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            SmsType = SmsType.MMS,
            Subject = "Test",
            FileUrl = "https://farm4.static.flickr.com/3090/2630374993_7acb582747_m.jpg"
        };

        var res = await smsSender.SendSms(Phone1, "Hello", smsConfig);
        Assert.NotNull(res);
        Assert.Equal(1, res.SmsCount);
    }

    [Fact]
    public async Task TestBinaryHexSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            SmsType = SmsType.Bin,
            UseBinary = BinaryMessage.Hex
        };

        var buf = Encoding.Default.GetBytes("hello, world!");
        var hexString = "00" + Convert.ToHexString(buf);
        var res = await smsSender.SendSms(Phone2, hexString, smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestBinaryHexWithHeaderLengthSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            SmsType = SmsType.Bin,
            UseBinary = BinaryMessage.Hex
        };

        var buf = Encoding.Default.GetBytes("Test SMS binary message!");
        var hexString = Convert.ToHexString(BitConverter.GetBytes(buf.Length)) + Convert.ToHexString(buf);
        var res = await smsSender.SendSms(Phone1, hexString, smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestWapPushSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { SmsType = SmsType.WapPush };

        var res = await smsSender.SendSms(Phone2, "Test WAP push SMS send", smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestFlashSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            SmsType = SmsType.Flash,
            Sender = "CoinToCoin"
        };

        var res = await smsSender.SendSms(Phone2, "Hellow, world!", smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestPingSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { SmsType = SmsType.Ping };

        var res = await smsSender.SendSms(Phone1, "", smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestTelegramSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { SmsType = SmsType.Telegram };

        var res = await smsSender.SendSms(Phone2, "123456", smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestSocialsSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { SmsType = SmsType.Socials };

        await Assert.ThrowsAsync<HttpSmsMessageDeliveringException>(() =>
            smsSender.SendSms(Phone1, "123456", smsConfig));
    }

    [Fact]
    public async Task TestWhatsAppSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            SmsType = SmsType.WhatsApp,
            BotName = "wa:79991234567"
        };

        await Assert.ThrowsAsync<HttpSmsMessageDeliveringException>(() =>
            smsSender.SendSms(Phone2, "123456", smsConfig));
    }

    [Fact]
    public async Task TestViberSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { SmsType = SmsType.Viber };

        await Assert.ThrowsAsync<HttpSmsMessageDeliveringException>(() =>
            smsSender.SendSms(Phone1, "Hello", smsConfig));
    }

    [Fact]
    public async Task TestBotNameSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            BotName = "@test",
            Sender = "SMSC"
        };

        await Assert.ThrowsAsync<HttpSmsMessageForbiddenException>(() =>
            smsSender.SendSms(Phone2, "test", smsConfig));
    }

    [Fact]
    public async Task TestErrorPhonesResponseSend()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            SmsType = SmsType.Telegram,
            ErrorPhonesResponse = true,
            FullErrorPhonesResponse = true,
        };

        var res = await smsSender.SendSms(Phone1, "test", smsConfig);
        Assert.NotNull(res);
        Assert.NotNull(res.Phones);
        Assert.NotEmpty(res.Phones);
    }

    [Fact]
    public async Task TestDefaultSmsSendWithValidProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { Valid = TimeSpan.FromHours(5) };

        var res = await smsSender.SendSms(Phone1, new StringValues("Hello"), smsConfig);
        Assert.NotNull(res);
        Assert.False(res.Id is null);
        Assert.False(res.SmsCount is null);
    }

    [Fact]
    public async Task TestDefaultSmsSendWithMaxSmsProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { MaxSms = 2 };

        const string text = "Практи́ческая транскри́пция — запись иноязычных имён и " +
            "названий с помощью исторически сложившейся орфографической системы языка, на который они передаются. " +
            "Практическая транскрипция использует обычные знаки (буквы) языка-приёмника без введения дополнительных знаков. " +
            "Практической транскрипцией на русский язык слово записывается буквами кириллицы с приблизительным сохранением " +
            "его звукового облика в исходном языке, а также с возможным учётом написания в оригинале и сложившихся традиций.";

        var res = await smsSender.SendSms(Phone2, new StringValues(text), smsConfig);
        Assert.NotNull(res);
        Assert.Equal(2, res.SmsCount);
    }

    [Fact]
    public async Task TestDefaultSmsSendWithCostProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { GetCost = Cost.CostAndBalance };

        var res = await smsSender.SendSms(Phone1, new StringValues("Hello world!"), smsConfig);
        Assert.NotNull(res);
        Assert.NotNull(res.Cost);
        Assert.NotNull(res.Balance);
    }

    [Fact]
    public async Task TestDefaultSmsSendWithCostSendProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { GetCost = Cost.CostSend };

        var res = await smsSender.SendSms(Phone1, new StringValues("Hello world!"), smsConfig);
        Assert.NotNull(res);
        Assert.NotNull(res.Cost);
    }

    [Fact]
    public async Task TestDefaultSmsSendWithSmsReqProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { SmsRequire = 11 };

        var res = await smsSender.SendSms(Phone2, new StringValues("Hello world!"), smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestDefaultSmsSendWithtTransliterationProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { Transliteration = Transliteration.Translit };

        var res = await smsSender.SendSms(Phone1, new StringValues("транслит"), smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestDefaultSmsSendWithtTransliterationExtendedProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { Transliteration = Transliteration.TranslitExtend };

        var res = await smsSender.SendSms(Phone2, new StringValues("транслит"), smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestDefaultSmsSendWithTinyUrlProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { TinyUrl = true };

        var res = await smsSender.SendSms(Phone1, new StringValues("Hello world! from https://cointocoin.ru/manual"), smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestTimeToSendFormatFullProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            Time = TimeFormat.Full,
            TimeToSend = DateTime.Now.AddDays(1).ToString("ddMMyyHHmm")
        };

        var res = await smsSender.SendSms(Phone1, new StringValues("Hello world!"), smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestTimeToSendFormatFull1Property()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            Time = TimeFormat.Full,
            TimeToSend = DateTime.Now.AddMinutes(5).ToString("dd.MM.yy HH:mm")
        };

        var res = await smsSender.SendSms(Phone2, new StringValues("Hello world!"), smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestTimeToSendFormatTimeSpanProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            Time = TimeFormat.TimeSpan,
            TimeToSend = $"{DateTime.Now.AddHours(1).Hour}-{DateTime.Now.AddHours(2).Hour}"
        };

        var res = await smsSender.SendSms(Phone1, new StringValues("Hello world!"), smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestTimeToSendFormatTimeStampProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            Time = TimeFormat.TimeStamp,
            TimeToSend = ((long)(DateTime.Now.ToUniversalTime().AddMinutes(25) - DateTime.UnixEpoch).TotalSeconds).ToString()
        };

        var res = await smsSender.SendSms(Phone2, new StringValues("Hello world!"), smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestTimeToSendFormatTimeShiftProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            Time = TimeFormat.TimeShift,
            TimeToSend = ((int)TimeSpan.FromMinutes(31).TotalMinutes).ToString()
        };

        var res = await smsSender.SendSms(Phone1, new StringValues("Hello world!"), smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestTimeToSendTzProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            Time = TimeFormat.Full,
            TimeToSend = DateTime.Now.AddHours(4).ToString("dd.MM.yy HH:mm"),
            TimeZone = 3
        };

        var res = await smsSender.SendSms(Phone1, new StringValues("Hello world!"), smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestGroupSmsPeriodAndFrequencyProperty()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration
        {
            Period = TimeSpan.FromHours(1),
            Frequency = TimeSpan.FromMinutes(10)
        };

        var res = await smsSender.SendSms([Phone1, Phone2], new StringValues("Hello world!"), smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestCharsetWin1251()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { Charset = Charset.WIN1251 };

        var res = await smsSender.SendSms(Phone1, new StringValues("Hello world!"), smsConfig);
        Assert.NotNull(res);
    }

    [Fact]
    public async Task TestCharsetKoi8r()
    {
        var smsSender = SmscTestFactory.CreateSms();
        var smsConfig = new SmsConfiguration { Charset = Charset.KOI8R };

        var res = await smsSender.SendSms(Phone2, new StringValues("Hello world!"), smsConfig);
        Assert.NotNull(res);
    }
}
