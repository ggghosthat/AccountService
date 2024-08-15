using AccountService.Entity;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Repository.Context;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {}

    public DbSet<User> Users { get; set; }
}