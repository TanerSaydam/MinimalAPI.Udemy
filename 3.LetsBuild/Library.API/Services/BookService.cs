using Library.API.Context;
using Library.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Services;

public sealed class BookService(ApplicationDbContext context) : IBookService
{
    public async Task<bool> CreateAsync(Book book, CancellationToken cancellationToken = default)
    {
        await context.Books.AddAsync(book, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteAsync(string isbn, CancellationToken cancellationToken = default)
    {
        Book? book = await context.Books.FindAsync(isbn, cancellationToken);
        if (book is null) return false;

        context.Books.Remove(book);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Books.ToListAsync(cancellationToken);
    }

    public async Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
    {
        return await context.Books.FindAsync(isbn, cancellationToken);
    }

    public async Task<IEnumerable<Book>> SearchByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await context.Books.Where(p => p.Title.Contains(title)).ToListAsync();
    }

    public async Task<bool> UpdateAsync(Book book, CancellationToken cancellationToken = default)
    {
        context.Update(book);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}
