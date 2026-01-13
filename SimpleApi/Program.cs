using System.Net;  // ← Добавили для WebProxy

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "✅ YouTube Proxy: /youtube");

app.MapGet("/youtube", async (HttpContext context) =>
{
    var proxy = new WebProxy("http://20.235.72.187:80");

    var handler = new HttpClientHandler()
    {
        Proxy = proxy,
        ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    using var client = new HttpClient(handler);

    try
    {
        var html = await client.GetStringAsync("https://www.youtube.com");
        context.Response.ContentType = "text/html; charset=UTF-8";
        await context.Response.WriteAsync(html);
    }
    catch
    {
        // Fallback без прокси
        using var fallback = new HttpClient();
        var html = await fallback.GetStringAsync("https://www.youtube.com");
        context.Response.ContentType = "text/html; charset=UTF-8";
        await context.Response.WriteAsync(html);
    }
});

app.Run(); //Done