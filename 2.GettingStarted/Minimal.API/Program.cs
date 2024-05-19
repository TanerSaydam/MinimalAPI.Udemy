using Microsoft.AspNetCore.Mvc;
using Minimal.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<PeopleService>();
builder.Services.AddScoped<GuidGenerator>();

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


app.MapGet("get", () => "This is a GET");
app.MapPost("post", () => "This is a POST");
app.MapPut("put", () => "This is a PUT");
app.MapDelete("delete", () => "This is a DELETE");

app.MapMethods("options-or-head", new[] { "HEAD", "OPTIONS" },
    () => "Hello from either options or head");

var handler = () => "This is coming from a var";
app.MapGet("handler", handler);
app.MapGet("fromclass", Example.SomeMethod);

app.MapGet("get-params/{age:int}", (int age) =>
{
    return $"Age provided was {age}";
});

app.MapGet("cars/{carId:regex(^[a-z0-9]+$)}", (string carId) =>
{
    return $"Car id provided was: {carId}";
});

app.MapGet("books/{isbn:length(13)}", (string isbn) =>
{
    return $"Isbn provided was: {isbn}";
});




app.MapGet("people/search", (string? searchTerm, PeopleService peopleService) =>
{
    if (searchTerm is null) return Results.NotFound();

    var result = peopleService.Search(searchTerm);

    return Results.Ok(result);
});

app.MapGet("mix/{routeParams}", (
    string routeParams,
    [FromQuery(Name = "q")] int queryParams,
    GuidGenerator guidGenerator) =>
{
    return $"{routeParams} {queryParams} {guidGenerator.NewGuid}";
});


app.MapPost("people", (Person person) =>
{
    return Results.Ok(person);
});

















app.Run();
