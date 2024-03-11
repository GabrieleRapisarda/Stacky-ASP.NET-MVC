using Microsoft.EntityFrameworkCore;
using StackyWeb.Models;

namespace StackyWeb.Data
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //posso creare tabelle direttamente con DbSet<ModelName> NomeTable {get;set;}
        public DbSet<Shoe> Shoes { get; set; }

        /*
         Posso sovrascrivere questo metodo per riempire la table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shoe>.HasData(
                new Shoe { atr },
                new Shoe { atr },
                new Shoe { atr },
                new Shoe { atr }
            )
        }
        */
    }
}
