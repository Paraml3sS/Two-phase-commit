using Application.Database.Contexts.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Database.Contexts
{
    public class AccountContext: DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options): base(options) { }

        public DbSet<Account> Accounts { get; set; }
    }
}