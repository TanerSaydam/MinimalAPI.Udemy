using FluentValidation.Results;
using Library.API.Models;
using Library.API.Services;
using Library.API.Validators;
using Microsoft.AspNetCore.Authorization;

namespace Library.API.Endpoints;

public static class BookEndpoints
{
    public static void UseBookEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("login", (JwtProvider jwtProvider) =>
        {
            return Results.Ok(new { Token = jwtProvider.CreateToken() });
        });

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

            return Results.CreatedAtRoute("GetBook", new { isbn = book.Isbn });
            //return Results.Created($"/books/{book.Isbn}", book);
        });

        app.MapGet("books", [Authorize] async (IBookService bookService, CancellationToken cancellationToken) =>
        {
            var books = await bookService.GetAllAsync(cancellationToken);
            return Results.Ok(books);
        });

        app.MapGet("books/{isbn}", async (string isbn, IBookService bookService, CancellationToken cancellationToke) =>
        {
            Book? book = await bookService.GetByIsbnAsync(isbn, cancellationToke);
            return Results.Ok(book);
        }).WithName("GetBook");

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

        app.MapDelete("books/{isbn}", async (string isbn, IBookService bookService, CancellationToken cancellationToken) =>
        {
            var result = await bookService.DeleteAsync(isbn, cancellationToken);
            if (!result) return Results.BadRequest("Something went wrong!");

            return Results.Ok(new { Message = "Book delete is successful" });
        });
    }
}
