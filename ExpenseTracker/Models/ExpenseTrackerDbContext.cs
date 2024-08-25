using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Models
{
	public class ExpenseTrackerDbContext: DbContext
	{
		// DbSet is simillar to a table in database

		public DbSet<Expense> Expenses { get; set; }

        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext>options): base(options)
        {
        }
    }
}
