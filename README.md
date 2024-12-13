# [<img align="center" src="https://smsc.ru/im/new/smsc_logo.png" width="150" alt="The SMSC logo" />](https://smsc.ru/)&nbsp;&nbsp;&nbsp;SMSC

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

You need to inititate a new instance of `ProviderConfiguration` with credentials:

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

after that, we send a message

```csharp
var result = await httpSmsSender.SendSms("+79999999999", "123456", smsConfig);
```

That is all you need to send a message to the client. The result is returned as a class object `HttpSmsResponse`.

### Get status

To check the SMS or e-mail delivery status, you must initiate `ProviderConfiguration`:

```csharp
var providerConfig = new ProviderConfiguration(login, password);
```

or you can initiate `ProviderConfiguration` with an `apikey` that you created in advance ([access passwords page](https://smsc.ru/passwords/))

```csharp
var providerConfig = new ProviderConfiguration(apiKey);
```

Create request manager from `HttpSmsStatus` class:

```csharp
var httpSmsStatus = new HttpSmsStatus(providerConfig);
```

Next, you need to create a configuration class object of `SmsStatusConfiguration` with properties you need:

```csharp
var smsStatusConfig = new SmsStatusConfiguration()
{
    Id = id
};
```

if you need to get additional information:

```csharp
var smsStatusConfig = new SmsStatusConfiguration()
{
    Id = id,
    StatusType = StatusType.Additional
};
```

`id` - the ID sent in response from previous step. And finally we send a request to server:

```csharp
var statusResult = await httpSmsStatus.CheckSms("+79999999999", smsStatusConfig);
```

The result is returned as a class object `HttpSmsStatusResponse`.

### Get balance

To get a balance you need to inititate a new instance of `ProviderConfiguration` with credentials:

```csharp
var providerConfig = new ProviderConfiguration(login, password);
```

or you can initiate `ProviderConfiguration` with an `apikey` that you created in advance ([access passwords page](https://smsc.ru/passwords/))

```csharp
var providerConfig = new ProviderConfiguration(apiKey);
```

Create request manager from `HttpSmsBalance` class:

```csharp
var httpSmsBalance = new HttpSmsBalance(providerConfig);
```

Next, you need to create a configuration class object of `SmsBalanceConfiguration` with properties you need:

```csharp
var smsBalanceConfig = new SmsBalanceConfiguration();
```

And finally we send a request to server:

```csharp
var balanceResult = await httpSmsBalance.CheckBalance(smsBalanceConfig);
```

The result is returned as a class object `HttpSmsBalanceResponse`.

## Documentation

This README aims to give a quick start guide - including enough to get you to quickly start sending SMS. For deeper detail of all properties go to [API Documentation](https://smsc.ru/api/#menu), and many other aspects of [smsc.ru](https://smsc.ru).