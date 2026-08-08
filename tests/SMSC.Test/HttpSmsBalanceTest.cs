using SMSC.Http;
using SMSC.Types;

namespace SMSC.Test;

public class HttpSmsBalanceTest
{
    [Fact]
    public async Task TestGetBalanceWithApi()
    {
        var httpSmsBalance = SmscTestFactory.CreateBalance();
        var res = await httpSmsBalance.CheckBalance(new SmsBalanceConfiguration());

        Assert.NotNull(res);
        Assert.NotNull(res.Balance);
    }

    [Fact]
    public async Task TestGetBalanceWithLoginAndPassword()
    {
        var httpSmsBalance = SmscTestFactory.CreateBalance(SmscTestFactory.LoginConfig());
        var res = await httpSmsBalance.CheckBalance(new SmsBalanceConfiguration());

        Assert.NotNull(res);
        Assert.NotNull(res.Balance);
    }

    [Fact]
    public async Task TestGetBalanceWithCurrencyWithApi()
    {
        var httpSmsBalance = SmscTestFactory.CreateBalance();
        var smsConfig = new SmsBalanceConfiguration { Currency = true };

        var res = await httpSmsBalance.CheckBalance(smsConfig);
        Assert.NotNull(res);
        Assert.NotNull(res.Balance);
        Assert.NotNull(res.Currency);
    }

    [Fact]
    public async Task TestGetBalanceWithCurrencyWithLoginAndPassword()
    {
        var httpSmsBalance = SmscTestFactory.CreateBalance(SmscTestFactory.LoginConfig());
        var smsConfig = new SmsBalanceConfiguration { Currency = true };

        var res = await httpSmsBalance.CheckBalance(smsConfig);
        Assert.NotNull(res);
        Assert.NotNull(res.Balance);
        Assert.NotNull(res.Currency);
    }
}
