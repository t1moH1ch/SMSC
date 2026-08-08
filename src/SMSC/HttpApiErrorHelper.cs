namespace SMSC;

internal static class HttpApiErrorHelper
{
    public static async Task<string> ReadJsonContentAsync(HttpResponseMessage response, IConfiguration config)
    {
        response.EnsureSuccessStatusCode();
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var content = await response.Content.ReadAsStringAsync();
        ArgumentException.ThrowIfNullOrEmpty(content);
        if (config.ResponseFormat != ResponseFormat.Json)
            throw new NotSupportedException("Only JSON format is supported for server response.");

        return content;
    }

    public static void ThrowIfError(string content)
    {
        if (!content.Contains("error_code", StringComparison.OrdinalIgnoreCase))
            return;

        var error = JsonSerializer.Deserialize<HttpSmsErrorResponse>(content)
            ?? throw new InvalidOperationException("Failed to parse SMSC error response.");

        var code = error.ErrorCode ?? 0;
        throw code switch
        {
            1 => new HttpSmsParametersException(),
            2 => new HttpSmsWrongCredentialsException(),
            3 => new HttpSmsFundLimitException(),
            4 => new HttpSmsIPAddressBlockException(),
            5 => new HttpSmsDateFormatException(),
            6 => new HttpSmsMessageForbiddenException(),
            7 => new HttpSmsPhoneNumberFormatException(),
            8 => new HttpSmsMessageDeliveringException(),
            9 => new HttpSmsTooManyConcurrentRequestsException(),
            _ => new InvalidCastException($"Unknown server exception. error_code={code}"),
        };
    }
}
