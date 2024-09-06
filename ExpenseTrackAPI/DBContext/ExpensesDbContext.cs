using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using ExpenseTrackAPI.Models;

namespace ExpenseTrackAPI.DBContext
{
    public class ExpensesDbContext : DbContext
    {
        public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options)
       : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite("DataSource=expenses.db");
        }
    }
}
