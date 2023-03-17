using projectMVC.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace projectMVC
{
    public partial class projectDbContext : DbContext
    {
        public DbSet<Admin> Admines { get; set; }
        public DbSet<Employe> Employies { get; set; }
        public DbSet<Food> Foodies { get; set; }
        public DbSet<Drink> Drinkies { get; set; }
        public DbSet<Seet> Seets { get; set; }

        public DbSet<Casher> Cashers { get; set; }
        public DbSet<Bill>   Bills { get; set; }
        public DbSet<Requirement> Requirement { get; set; }




        public projectDbContext()
            : base("name=Model1")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
