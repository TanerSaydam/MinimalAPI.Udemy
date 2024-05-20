using Library.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
}
