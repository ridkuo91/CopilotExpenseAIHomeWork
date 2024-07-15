using Microsoft.EntityFrameworkCore;

namespace GithubCopilotExpenseService.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
    }
}