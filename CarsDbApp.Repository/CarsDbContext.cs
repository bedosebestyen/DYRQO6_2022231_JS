using DYRQO6_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DYRQO6_HFT_2022231.Repository
{
    public class CarsDbContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CarShop> Carshop { get; set; }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public CarsDbContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder
                   .UseInMemoryDatabase("cars")
                   .UseLazyLoadingProxies();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cars>()
                .HasOne(c => c.Customer)
                .WithMany(c => c.Cars)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cars>()
                .HasOne(r => r.Shop)
                .WithMany(r => r.Cars)
                .HasForeignKey(c => c.ShopId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarShop>()
                .HasMany(r => r.Customers)
                .WithMany(c => c.Shop);

            modelBuilder.Entity<Manager>()
                .HasOne(m => m.CarShop)
                .WithOne(c => c.Manager)
                .HasForeignKey<CarShop>(m => m.ManagerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cars>().HasData(new Cars[]
            {
                new Cars("1#Skoda#king blue#2#2#2009*04*15#5000000"),
                new Cars("2#Peugeot#white#1#1#2019*06*08#4000000"),
                new Cars("3#Volkswagen#black#3#3#2022*01*30#6000000"),
                new Cars("4#Fiat#red#4#3#2000*09*19#3000000"),
                new Cars("5#BMW#black#4#1#2020*02*22#11000000"),
                new Cars("6#Audi#white#1#1#2019*04*15#10000000")
            });
            modelBuilder.Entity<Customer>().HasData(new Customer[]
            {
                new Customer("1#25#Winton Dickinson#695 Willison Street"),
                new Customer("2#22#Osmond Chambers#2111 Sand Fork Road"),
                new Customer("3#31#Talia Cooke#2390 Carriage Court"),
                new Customer("4#50#Isaiah Motley#217 Emeral Dreams Drive")
            });
            modelBuilder.Entity<CarShop>().HasData(new CarShop[]
            {
                new CarShop("1#Best cars#5#3300 Fittro Street#1"),
                new CarShop("2#Awesome machines#4#1714 Mulberry Lane#2"),
                new CarShop("3#Carscarscars#10#3799 Marie Street#3")
            });
            modelBuilder.Entity<Manager>().HasData(new Manager[]
            {
                new Manager("1#Pradip Xanthippe#500000#36#1"),
                new Manager("2#Lorrin Matthei#600000#47#2"),
                new Manager("3#Rúni Surayya#800000#59#3")
            });
        }
    }
   
}
