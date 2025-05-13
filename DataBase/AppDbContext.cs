using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using RetoTecnico.Models;

namespace RetoTecnico.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}