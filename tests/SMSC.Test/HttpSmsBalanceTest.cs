using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

using SMSC.Http;
using SMSC.Types;

namespace SMSC.Test;

public class HttpSmsBalanceTest
{
    private readonly StringValues PhoneForTest;
    private readonly string ApiKey;
    private readonly string Login;
    private readonly string Password;

    private ProviderConfiguration? _provider;
    private HttpSmsBalance? _httpSmsBalance;

    public HttpSmsBalanceTest()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets(GetType().Assembly)
            .Build();

        PhoneForTest = new StringValues(config["Phone"]!);
        ApiKey = config["ApiKey"]!;
        Login = config["Login"]!;
        Password = config["Password"]!;
    }

    [Fact]
    public async Task TestGetBalanceWithApi()
    {
        _provider = new ProviderConfiguration(ApiKey);
        SetSmsSender();
        Assert.NotNull(_httpSmsBalance);

        var smsConfig = new SmsBalanceConfiguration();

        var res = await _httpSmsBalance.CheckBalance(smsConfig);
        Assert.NotNull(res);
        Assert.NotNull(res.Balance);
    }
    [Fact]
    public async Task TestGetBalanceWithLoginAndPassword()
    {
        _provider = new ProviderConfiguration(Login, Password);
        SetSmsSender();
        Assert.NotNull(_httpSmsBalance);

        var smsConfig = new SmsBalanceConfiguration();

        var res = await _httpSmsBalance.CheckBalance(smsConfig);
        Assert.NotNull(res);
        Assert.NotNull(res.Balance);
    }

    [Fact]
    public async Task TestGetBalanceWithCurrencyWithApi()
    {
        _provider = new ProviderConfiguration(ApiKey);
        SetSmsSender();
        Assert.NotNull(_httpSmsBalance);

        var smsConfig = new SmsBalanceConfiguration()
        {
            Currency = true
        };

        var res = await _httpSmsBalance.CheckBalance(smsConfig);
        Assert.NotNull(res);
        Assert.NotNull(res.Balance);
        Assert.NotNull(res.Currency);
    }
    [Fact]
    public async Task TestGetBalanceWithCurrencyWithLoginAndPassword()
    {
        _provider = new ProviderConfiguration(Login, Password);
        SetSmsSender();
        Assert.NotNull(_httpSmsBalance);

        var smsConfig = new SmsBalanceConfiguration()
        {
            Currency = true
        };

        var res = await _httpSmsBalance.CheckBalance(smsConfig);
        Assert.NotNull(res);
        Assert.NotNull(res.Balance);
        Assert.NotNull(res.Currency);
    }

    private void SetSmsSender() => _httpSmsBalance = new HttpSmsBalance(_provider!);
}
