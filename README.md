# [![SMSC](https://raw.githubusercontent.com/t1moH1ch/SMSC/main/images/smsc_logo.png)](https://smsc.ru)

SMSC is an unofficial client .NET for the service [smsc.ru](https://smsc.ru) that allows developers to use HTTP-based API for sending and receiving phone calls and text messages.

## Quick start
### Send SMS
The examples below show how to have your application initiate and outbound phone call and send an SMS message using the SMSC helper library.

To get started, first add the SMSC package to your project by running the following command:

```sh
dotnet add package SMSC
```

or for Visual Studio package manager:

```sh
Install-Package SMSC
```

You need to initiate a new instance of `ProviderConfiguration` with credentials:

```csharp
var providerConfig = new ProviderConfiguration(login, password);
```

or you can initiate `ProviderConfiguration` with an `apikey` that you created in advance ([access passwords page](https://smsc.ru/passwords/))

```csharp
var providerConfig = new ProviderConfiguration(apiKey);
```

Create request manager from `HttpSms` class:

```csharp
var httpSmsSender = new HttpSms(providerConfig);
```

Next, you need to create a configuration class object of `SmsConfiguration` with properties you need:

```csharp
var smsConfig = new SmsConfiguration();
```

if you need to send a code in the form of a call:

```csharp
var smsConfig = new SmsConfiguration()
{
    SmsType = SmsType.Call
};
```

WhatsApp (bot required in `wa:` format per [docs](https://smsc.ru/api/http/send/whatsapp/)):

```csharp
var smsConfig = new SmsConfiguration()
{
    SmsType = SmsType.WhatsApp,
    BotName = "wa:79888888888"
};
var result = await httpSmsSender.SendSms("+79999999999", "Hello {button,tel:+79999999999,Call}", smsConfig);
```

Telegram:

```csharp
var smsConfig = new SmsConfiguration()
{
    SmsType = SmsType.Telegram,
    BotName = "@botname_bot"
};
```

after that, we send a message

```csharp
var result = await httpSmsSender.SendSms("+79999999999", "123456", smsConfig);
```

That is all you need to send a message to the client. The result is returned as a class object `HttpSmsResponse`.

Optional JSON REST body (`https://smsc.ru/rest/send/`):

```csharp
httpSmsSender.UseJsonRequestFormat = true;
```

#
### Get status

```csharp
var providerConfig = new ProviderConfiguration(login, password);
var httpSmsStatus = new HttpSmsStatus(providerConfig);
var smsStatusConfig = new SmsStatusConfiguration()
{
    Id = id,
    StatusType = StatusType.Additional
};
var statusResult = await httpSmsStatus.CheckSms("+79999999999", smsStatusConfig);
```

Batch check (comma-separated ids/phones in API):

```csharp
var statusResult = await httpSmsStatus.CheckSms(
    phones: ["+79999999999", "+78888888888"],
    ids: ["123", "456"],
    config: new SmsStatusConfiguration { Id = null });
```

Telegram-bot message status:

```csharp
var statusResult = await httpSmsStatus.CheckSms(
    client: string.Empty,
    config: new SmsStatusConfiguration { Id = null, Bot = true });
```

#
### Get balance

```csharp
var httpSmsBalance = new HttpSmsBalance(providerConfig);
var balanceResult = await httpSmsBalance.CheckBalance(new SmsBalanceConfiguration());
```

#
### Wait call (phone confirmation)

```csharp
var waitCall = new HttpWaitCall(providerConfig);
var waitResult = await waitCall.RequestAsync("+79999999999");
// waitResult.Phone — number to call within 15 minutes
```

#
### Contacts

```csharp
var contacts = new HttpContacts(providerConfig);
await contacts.AddAsync("+79999999999", name: "Contact", groupId: "321");
await contacts.GetGroupsAsync();
```

#
### Jobs (mailings)

```csharp
var jobs = new HttpJobs(providerConfig);
await jobs.AddAsync(new JobsConfiguration
{
    Name = "Privet",
    Phones = "79999999999",
    Message = "Hello!"
});
```

#
### History / senders / users / receive / downloads

```csharp
var history = new HttpHistory(providerConfig);
await history.GetMessagesAsync(new HistoryConfiguration { Start = "01.01.2024", End = "01.02.2024" });

var senders = new HttpSenders(providerConfig);
await senders.GetAsync();

var users = new HttpUsers(providerConfig);
await users.GetStatAsync();

var receive = new HttpReceivePhones(providerConfig);
await receive.GetAsync();

var downloads = new HttpDownloads(providerConfig);
await downloads.GetListAsync();
```

## Documentation

This README aims to give a quick start guide - including enough to get you to quickly start sending SMS. For deeper detail of all properties go to [API Documentation](https://smsc.ru/api/#menu), and many other aspects of [smsc.ru](https://smsc.ru).

## Implemented features
- Http SMS send manager ([documentation](https://smsc.ru/api/http/send/#menu))
- Http SMS status manager ([documentation](https://smsc.ru/api/http/status_messages/#menu))
- Http balance manager ([documentation](https://smsc.ru/api/http/balance/#menu))
- Http wait call manager ([documentation](https://smsc.ru/api/http/miscellaneous/waitcall/#menu))
- Http contacts manager ([documentation](https://smsc.ru/api/http/contact/#menu))
- Http mailing (jobs) manager ([documentation](https://smsc.ru/api/http/jobs/#menu))
- Http history / data manager ([documentation](https://smsc.ru/api/http/get_data/#menu))
- Http sender names manager ([documentation](https://smsc.ru/api/http/senders/#menu))
- Http clients (users) manager ([documentation](https://smsc.ru/api/http/users/#menu))
- Dedicated receive numbers ([documentation](https://smsc.ru/api/http/miscellaneous/receive/#menu))
- Deferred downloads ([documentation](https://smsc.ru/api/http/miscellaneous/downloads/#menu))

Transport notes aligned with SMSC docs:
- Host failover `www1`…`www5.smsc.ru` on empty responses
- POST for mail/call, attachments, and long query strings
- Message max length 1000 characters (`MessageMaxLengthException`)
- WhatsApp via `bot=wa:…` (not legacy `whatsapp=1`)
