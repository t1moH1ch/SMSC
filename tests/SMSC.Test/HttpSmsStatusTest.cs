using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using SMSC.Http;
using SMSC.Types;
using SMSC.Types.Enums;

namespace SMSC.Test;

public class HttpSmsStatusTest
{
    private readonly StringValues Phone;
    private readonly StringValues Phone2;
    private readonly string ApiKey;
    private readonly string Login;
    private readonly string Password;

    private HttpSmsStatus? _httpSmsStatus;
    private HttpSms? _httpSmsSender;

    public HttpSmsStatusTest()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets(GetType().Assembly)
            .Build();

        Phone = new StringValues(config["Phone"]!);
        Phone2 = new StringValues(config["Phone2"]!);
        ApiKey = config["ApiKey"]!;
        Login = config["Login"]!;
        Password = config["Password"]!;
    }

    [Fact]
    public async Task GetDefaultStatusWithApi()
    {
        var id = await CreateItemAsyncTest(Phone);
        Assert.NotNull(id);

        _httpSmsStatus = new HttpSmsStatus(new ProviderConfiguration(ApiKey));
        var statusConfig = new SmsStatusConfiguration()
        {
            Id = id,
        };

        var statusRes = await _httpSmsStatus.CheckSms(Phone.ToString(), statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
    }
    [Fact]
    public async Task GetDefaultStatusWithLoginAndPassword()
    {
        var id = await CreateItemAsyncTest(Phone);
        Assert.NotNull(id);

        _httpSmsStatus = new HttpSmsStatus(new ProviderConfiguration(Login, Password));
        var statusConfig = new SmsStatusConfiguration()
        {
            Id = id,
        };

        var statusRes = await _httpSmsStatus.CheckSms(Phone.ToString(), statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
    }

    [Fact]
    public async Task GetDefaultStatusWithApiWithStatusTypeFull()
    {
        var id = await CreateItemAsyncTest(Phone);
        Assert.NotNull(id);

        _httpSmsStatus = new HttpSmsStatus(new ProviderConfiguration(ApiKey));
        var statusConfig = new SmsStatusConfiguration()
        {
            Id = id,
            StatusType = StatusType.Full,
        };

        var statusRes = await _httpSmsStatus.CheckSms(Phone.ToString(), statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
    }
    [Fact]
    public async Task GetDefaultStatusWithApiWithStatusTypeAdditional()
    {
        var id = await CreateItemAsyncTest(Phone);
        Assert.NotNull(id);

        _httpSmsStatus = new HttpSmsStatus(new ProviderConfiguration(ApiKey));
        var statusConfig = new SmsStatusConfiguration()
        {
            Id = id,
            StatusType = StatusType.Additional,
        };

        var statusRes = await _httpSmsStatus.CheckSms(Phone.ToString(), statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
    }

    [Fact]
    public async Task GetHLRStatusWithApi()
    {
        var id = await CreateHLRItemAsyncTest(Phone);
        Assert.NotNull(id);

        _httpSmsStatus = new HttpSmsStatus(new ProviderConfiguration(ApiKey));
        var statusConfig = new SmsStatusConfiguration()
        {
            Id = id,
        };

        var statusRes = await _httpSmsStatus.CheckSms(Phone.ToString(), statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
    }
    [Fact]
    public async Task GetHLRStatusWithApiWithStatusTypeFull()
    {
        var id = await CreateHLRItemAsyncTest(Phone2);
        Assert.NotNull(id);

        _httpSmsStatus = new HttpSmsStatus(new ProviderConfiguration(ApiKey));
        var statusConfig = new SmsStatusConfiguration()
        {
            Id = id,
            StatusType = StatusType.Full,
        };

        var statusRes = await _httpSmsStatus.CheckSms(Phone2.ToString(), statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
        Assert.NotNull(statusRes.First().HLROperator);
    }
    [Fact]
    public async Task GetHLRStatusWithApiWithStatusTypeAdditional()
    {
        var id = await CreateHLRItemAsyncTest(Phone);
        Assert.NotNull(id);

        _httpSmsStatus = new HttpSmsStatus(new ProviderConfiguration(ApiKey));
        var statusConfig = new SmsStatusConfiguration()
        {
            Id = id,
            StatusType = StatusType.Additional,
        };

        var statusRes = await _httpSmsStatus.CheckSms(Phone.ToString(), statusConfig);
        Assert.NotNull(statusRes);
        Assert.NotEmpty(statusRes);
        Assert.NotNull(statusRes.First().MNC);
    }

    [Fact]
    public async Task GetDefaultStatusWithApiGroup()
    {
        var list = new List<Tuple<string, string>>();

        var id = await CreateItemAsyncTest(Phone);
        Assert.NotNull(id);
        list.Add(new Tuple<string, string>(id, Phone.ToString()));

        id = await CreateItemAsyncTest(Phone2);
        Assert.NotNull(id);
        list.Add(new Tuple<string, string>(id, Phone2.ToString()));

        _httpSmsStatus = new HttpSmsStatus(new ProviderConfiguration(ApiKey));
        var statusConfig = new SmsStatusConfiguration()
        {
            Id = string.Join(",", list.Select(d => d.Item1))
        };

        var statuses = await _httpSmsStatus.CheckSms(string.Join(",", list.Select(p => p.Item2)), statusConfig);
        Assert.NotNull(statuses);
        Assert.True(statuses.Count() > 1);
    }
    [Fact]
    public async Task GetDefaultStatusWithApiGroupWithStatusTypeFull()
    {
        var list = new List<Tuple<string, string>>();

        var id = await CreateItemAsyncTest(Phone);
        Assert.NotNull(id);
        list.Add(new Tuple<string, string>(id, Phone.ToString()));

        id = await CreateItemAsyncTest(Phone2);
        Assert.NotNull(id);
        list.Add(new Tuple<string, string>(id, Phone2.ToString()));

        _httpSmsStatus = new HttpSmsStatus(new ProviderConfiguration(ApiKey));
        var statusConfig = new SmsStatusConfiguration()
        {
            Id = string.Join(",", list.Select(d => d.Item1)),
            StatusType = StatusType.Full
        };

        var statuses = await _httpSmsStatus.CheckSms(string.Join(",", list.Select(p => p.Item2)), statusConfig);
        Assert.NotNull(statuses);
        Assert.True(statuses.Count() > 1);
    }
    [Fact]
    public async Task GetDefaultStatusWithApiGroupWithStatusTypeAdditional()
    {
        var list = new List<Tuple<string, string>>();

        var id = await CreateItemAsyncTest(Phone);
        Assert.NotNull(id);
        list.Add(new Tuple<string, string>(id, Phone.ToString()));

        id = await CreateItemAsyncTest(Phone2);
        Assert.NotNull(id);
        list.Add(new Tuple<string, string>(id, Phone2.ToString()));

        _httpSmsStatus = new HttpSmsStatus(new ProviderConfiguration(ApiKey));
        var statusConfig = new SmsStatusConfiguration()
        {
            Id = string.Join(",", list.Select(d => d.Item1)),
            StatusType = StatusType.Additional
        };

        var statuses = await _httpSmsStatus.CheckSms(string.Join(",", list.Select(p => p.Item2)), statusConfig);
        Assert.NotNull(statuses);
        Assert.True(statuses.Count() > 1);
    }

    private async Task<string?> CreateItemAsyncTest(StringValues phone)
    {
        _httpSmsSender = new HttpSms(new ProviderConfiguration(ApiKey));

        var smsConfig = new SmsConfiguration();

        var res = await _httpSmsSender.SendSms(phone, new StringValues($"Test SMS from {GetType().Name}"), smsConfig);
        return res?.Id;
    }
    private async Task<string?> CreateHLRItemAsyncTest(StringValues phone)
    {
        _httpSmsSender = new HttpSms(new ProviderConfiguration(ApiKey));

        var smsConfig = new SmsConfiguration()
        {
            SmsType = SmsType.HLR
        };

        var res = await _httpSmsSender.SendSms(phone, new StringValues($"Test SMS from {GetType().Name}"), smsConfig);
        return res?.Id;
    }
}
