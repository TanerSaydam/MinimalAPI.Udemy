var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("get-example", () => "Hello from GET");
app.MapPost("post-example", () => "Hello from POST");

app.Run();
