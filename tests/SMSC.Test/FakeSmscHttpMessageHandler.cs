using System.Net;
using System.Text;
using System.Text.Json;

using SMSC.Http;
using SMSC.Types;

namespace SMSC.Test;

/// <summary>
/// Имитация ответов SMSC API для unit-тестов без сети.
/// </summary>
internal sealed class FakeSmscHttpMessageHandler : HttpMessageHandler
{
    public const string ApiKey = "test-api-key";
    public const string Login = "test-login";
    public const string Password = "test-password";
    public const string Phone1 = "+79991111111";
    public const string Phone2 = "+79992222222";

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var payload = await ReadPayloadAsync(request, cancellationToken);

        if (!IsAuthorized(payload))
            return Json("""{"error":"Wrong credentials","error_code":2}""");

        var path = request.RequestUri?.AbsolutePath ?? string.Empty;

        if (path.Contains("balance.php", StringComparison.OrdinalIgnoreCase))
            return CreateBalanceResponse(payload);

        if (path.Contains("status.php", StringComparison.OrdinalIgnoreCase))
            return CreateStatusResponse(payload);

        return CreateSendResponse(payload);
    }

    private static HttpResponseMessage CreateBalanceResponse(IReadOnlyDictionary<string, string> payload)
    {
        if (payload.ContainsKey("cur"))
            return Json("""{"balance":"150.25","currency":"RUB","credit":"0"}""");

        return Json("""{"balance":"150.25","credit":"0"}""");
    }

    private static HttpResponseMessage CreateStatusResponse(IReadOnlyDictionary<string, string> payload)
    {
        var phones = (payload.GetValueOrDefault("phone") ?? string.Empty)
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        payload.TryGetValue("all", out var all);

        if (phones.Length > 1)
        {
            var items = phones.Select(phone => CreateStatusObject(phone, all));
            return Json(JsonSerializer.Serialize(items));
        }

        var singlePhone = phones.FirstOrDefault() ?? Phone1;
        return Json(JsonSerializer.Serialize(CreateStatusObject(singlePhone, all)));
    }

    private static Dictionary<string, object?> CreateStatusObject(string phone, string? all)
    {
        var status = new Dictionary<string, object?>
        {
            ["status"] = 1,
            ["last_date"] = "08.08.2026 12:00:00",
            ["phone"] = phone,
            ["status_name"] = "Delivered"
        };

        if (all is "1" or "2")
        {
            status["net"] = "TestOperator";
            status["mnc"] = 99;
            status["mcc"] = 250;
            status["cn"] = "Russia";
        }

        return status;
    }

    private static HttpResponseMessage CreateSendResponse(IReadOnlyDictionary<string, string> payload)
    {
        if (payload.ContainsKey("soc") || payload.ContainsKey("viber"))
            return Json("""{"error":"Cannot deliver","error_code":8}""");

        if (payload.TryGetValue("bot", out var bot))
        {
            if (bot.Equals("@test", StringComparison.Ordinal))
                return Json("""{"error":"Forbidden","error_code":6}""");

            if (bot.StartsWith("wa:", StringComparison.OrdinalIgnoreCase))
                return Json("""{"error":"Cannot deliver","error_code":8}""");
        }

        var cnt = 1;
        if (payload.TryGetValue("maxsms", out var maxSms) && int.TryParse(maxSms, out var max) && max > 0)
            cnt = max;

        var response = new Dictionary<string, object?>
        {
            ["id"] = "42",
            ["cnt"] = cnt
        };

        if (payload.TryGetValue("cost", out var costMode))
        {
            response["cost"] = "1.5";
            if (costMode is "3")
                response["balance"] = "100.0";
        }

        if (payload.ContainsKey("err") || payload.ContainsKey("op"))
        {
            var phone = payload.GetValueOrDefault("phones") ?? Phone1;
            var firstPhone = phone.Split([';', ','], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .FirstOrDefault() ?? Phone1;

            response["phones"] = new[]
            {
                new Dictionary<string, object?>
                {
                    ["phone"] = firstPhone,
                    ["mccmnc"] = "25099",
                    ["cost"] = "1.5",
                    ["status"] = "0"
                }
            };
        }

        return Json(JsonSerializer.Serialize(response));
    }

    private static bool IsAuthorized(IReadOnlyDictionary<string, string> payload)
    {
        if (payload.TryGetValue("apikey", out var apiKey))
            return apiKey == ApiKey;

        payload.TryGetValue("login", out var login);
        payload.TryGetValue("psw", out var password);
        return login == Login && password == Password;
    }

    private static async Task<Dictionary<string, string>> ReadPayloadAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (request.RequestUri?.Query is { Length: > 1 } query)
            MergeQuery(result, query);

        if (request.Content is null)
            return result;

        var mediaType = request.Content.Headers.ContentType?.MediaType ?? string.Empty;
        var body = await request.Content.ReadAsStringAsync(cancellationToken);

        if (mediaType.Contains("json", StringComparison.OrdinalIgnoreCase))
        {
            using var doc = JsonDocument.Parse(string.IsNullOrWhiteSpace(body) ? "{}" : body);
            foreach (var prop in doc.RootElement.EnumerateObject())
            {
                result[prop.Name] = prop.Value.ValueKind == JsonValueKind.String
                    ? prop.Value.GetString() ?? string.Empty
                    : prop.Value.ToString();
            }

            return result;
        }

        if (!string.IsNullOrEmpty(body) && !mediaType.Contains("multipart", StringComparison.OrdinalIgnoreCase))
            MergeQuery(result, body.StartsWith('?') ? body : "?" + body);

        return result;
    }

    private static void MergeQuery(IDictionary<string, string> target, string query)
    {
        var trimmed = query.StartsWith('?') ? query[1..] : query;
        foreach (var pair in trimmed.Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = pair.Split('=', 2);
            var key = Uri.UnescapeDataString(parts[0]);
            var value = parts.Length > 1
                ? Uri.UnescapeDataString(parts[1].Replace('+', ' '))
                : string.Empty;
            target[key] = value;
        }
    }

    private static HttpResponseMessage Json(string content) =>
        new(HttpStatusCode.OK)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };
}

internal static class SmscTestFactory
{
    public static HttpClient CreateHttpClient() =>
        new(new FakeSmscHttpMessageHandler(), disposeHandler: true);

    public static ProviderConfiguration ApiKeyConfig() =>
        new(FakeSmscHttpMessageHandler.ApiKey);

    public static ProviderConfiguration LoginConfig() =>
        new(FakeSmscHttpMessageHandler.Login, FakeSmscHttpMessageHandler.Password);

    public static HttpSms CreateSms(ProviderConfiguration? config = null) =>
        new(config ?? ApiKeyConfig(), CreateHttpClient());

    public static HttpSmsStatus CreateStatus(ProviderConfiguration? config = null) =>
        new(config ?? ApiKeyConfig(), CreateHttpClient());

    public static HttpSmsBalance CreateBalance(ProviderConfiguration? config = null) =>
        new(config ?? ApiKeyConfig(), CreateHttpClient());
}
