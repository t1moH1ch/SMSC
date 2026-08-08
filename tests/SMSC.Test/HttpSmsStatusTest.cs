using SMSC.Http;
using SMSC.Types;
using SMSC.Types.Enums;

namespace SMSC.Test;

public class HttpSmsStatusTest
{
    private const string Phone = FakeSmscHttpMessageHandler.Phone1;
    private const string Phone2 = FakeSmscHttpMessageHandler.Phone2;
    private const string MessageId = "42";

    [Fact]
    public async Task GetDefaultStatusWithApi()
    {
        var httpSmsStatus = SmscTestFactory.CreateStatus();
        var statusConfig = new SmsStatusConfiguration { Id = MessageId };

        var statusRes = await httpSmsStatus.CheckSms(Phone, statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
    }

    [Fact]
    public async Task GetDefaultStatusWithLoginAndPassword()
    {
        var httpSmsStatus = SmscTestFactory.CreateStatus(SmscTestFactory.LoginConfig());
        var statusConfig = new SmsStatusConfiguration { Id = MessageId };

        var statusRes = await httpSmsStatus.CheckSms(Phone, statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
    }

    [Fact]
    public async Task GetDefaultStatusWithApiWithStatusTypeFull()
    {
        var httpSmsStatus = SmscTestFactory.CreateStatus();
        var statusConfig = new SmsStatusConfiguration
        {
            Id = MessageId,
            StatusType = StatusType.Full,
        };

        var statusRes = await httpSmsStatus.CheckSms(Phone, statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
    }

    [Fact]
    public async Task GetDefaultStatusWithApiWithStatusTypeAdditional()
    {
        var httpSmsStatus = SmscTestFactory.CreateStatus();
        var statusConfig = new SmsStatusConfiguration
        {
            Id = MessageId,
            StatusType = StatusType.Additional,
        };

        var statusRes = await httpSmsStatus.CheckSms(Phone, statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
    }

    [Fact]
    public async Task GetHLRStatusWithApi()
    {
        var httpSmsStatus = SmscTestFactory.CreateStatus();
        var statusConfig = new SmsStatusConfiguration { Id = MessageId };

        var statusRes = await httpSmsStatus.CheckSms(Phone, statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
    }

    [Fact]
    public async Task GetHLRStatusWithApiWithStatusTypeFull()
    {
        var httpSmsStatus = SmscTestFactory.CreateStatus();
        var statusConfig = new SmsStatusConfiguration
        {
            Id = MessageId,
            StatusType = StatusType.Full,
        };

        var statusRes = await httpSmsStatus.CheckSms(Phone2, statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
        Assert.NotNull(statusRes.First().HLROperator);
    }

    [Fact]
    public async Task GetHLRStatusWithApiWithStatusTypeAdditional()
    {
        var httpSmsStatus = SmscTestFactory.CreateStatus();
        var statusConfig = new SmsStatusConfiguration
        {
            Id = MessageId,
            StatusType = StatusType.Additional,
        };

        var statusRes = await httpSmsStatus.CheckSms(Phone, statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
        Assert.NotNull(statusRes.First().MNC);
    }

    [Fact]
    public async Task GetDefaultStatusWithApiGroup()
    {
        var httpSmsStatus = SmscTestFactory.CreateStatus();
        var statusConfig = new SmsStatusConfiguration
        {
            Id = $"{MessageId},43"
        };

        var statuses = await httpSmsStatus.CheckSms($"{Phone},{Phone2}", statusConfig);
        Assert.NotNull(statuses);
        Assert.True(statuses.Count() > 1);
    }

    [Fact]
    public async Task GetDefaultStatusWithApiGroupWithStatusTypeFull()
    {
        var httpSmsStatus = SmscTestFactory.CreateStatus();
        var statusConfig = new SmsStatusConfiguration
        {
            Id = $"{MessageId},43",
            StatusType = StatusType.Full
        };

        var statuses = await httpSmsStatus.CheckSms($"{Phone},{Phone2}", statusConfig);
        Assert.NotNull(statuses);
        Assert.True(statuses.Count() > 1);
    }

    [Fact]
    public async Task GetDefaultStatusWithApiGroupWithStatusTypeAdditional()
    {
        var httpSmsStatus = SmscTestFactory.CreateStatus();
        var statusConfig = new SmsStatusConfiguration
        {
            Id = $"{MessageId},43",
            StatusType = StatusType.Additional
        };

        var statuses = await httpSmsStatus.CheckSms($"{Phone},{Phone2}", statusConfig);
        Assert.NotNull(statuses);
        Assert.True(statuses.Count() > 1);
    }
}
