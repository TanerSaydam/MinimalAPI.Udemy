using FluentValidation.Results;
using Library.API.Context;
using Library.API.Models;
using Library.API.Services;
using Library.API.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("MyDb");
});

builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("books", async (Book book, IBookService bookService, CancellationToken cancellationToken) =>
{
    BookValidator validator = new();
    ValidationResult validationResult = validator.Validate(book);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));
    }

    var result = await bookService.CreateAsync(book, cancellationToken);
    if (!result) return Results.BadRequest("Something went wrong!");

    return Results.Ok(new { Message = "Book create is successful" });
});

app.MapGet("books", async (IBookService bookService, CancellationToken cancellationToken) =>
{
    var books = await bookService.GetAllAsync(cancellationToken);
    return Results.Ok(books);
});

app.MapGet("books/{isbn}", async (string isbn, IBookService bookService, CancellationToken cancellationToke) =>
{
    Book? book = await bookService.GetByIsbnAsync(isbn, cancellationToke);
    return Results.Ok(book);
});

app.MapGet("get-books-by-title/{title}", async (string title, IBookService bookService, CancellationToken cancellationToke) =>
{
    var books = await bookService.SearchByTitleAsync(title, cancellationToke);
    return Results.Ok(books);
});

app.MapPut("books", async (Book book, IBookService bookService, CancellationToken cancellationToken) =>
{
    BookValidator validator = new();
    ValidationResult validationResult = validator.Validate(book);
    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors.Select(s => s.ErrorMessage));
    }

    var result = await bookService.UpdateAsync(book, cancellationToken);
    if (!result) return Results.BadRequest("Something went wrong!");

    return Results.Ok(new { Message = "Book update is successful" });
});

app.Run();
