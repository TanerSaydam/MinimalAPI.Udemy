using Library.API.Models;

namespace Library.API.Services;

public interface IBookService
{
    Task<bool> CreateAsync(Book book, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Book book, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string isbn, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> SearchByTitleAsync(string title, CancellationToken cancellationToken = default);
    Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default);
}
