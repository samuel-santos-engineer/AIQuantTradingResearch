var builder = WebApplication.CreateSlimBuilder();
builder.Logging.ClearProviders();
builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
    options.Listen(System.Net.IPAddress.Any, 52731);
});

await using var app = builder.Build();
app.Run(async context =>
{
    if (context.Request.Method == HttpMethods.Get && context.Request.Path == "/health")
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Alive", context.RequestAborted);
        return;
    }
    context.Response.StatusCode = StatusCodes.Status404NotFound;
});

await app.StartAsync();
await Console.Error.WriteLineAsync("Parallel API control. stage=LISTENER_READY");
await Console.Error.FlushAsync();
await app.WaitForShutdownAsync();
