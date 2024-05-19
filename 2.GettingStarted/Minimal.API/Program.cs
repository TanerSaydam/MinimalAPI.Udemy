var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("get-example", () => "Hello from GET");
app.MapPost("post-example", () => "Hello from POST");

app.MapGet("ok-object", () => Results.Ok(new { Message = "API is working..." }));

app.MapGet("slow-request", async () =>
{
    await Task.Delay(2000);

    return Results.Ok(new
    {
        Message = "Slow API is working..."
    });
});

app.Run();
