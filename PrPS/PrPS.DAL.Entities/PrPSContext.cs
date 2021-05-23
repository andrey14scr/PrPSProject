using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrPS.DAL.Core.Entities;

namespace PrPS.DAL.Core
{
    public class PrPsContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocAction> Actions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> Types { get; set; }
        public DbSet<Subdivision> Subdivisions { get; set; }

        public PrPsContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Comp2;Database=PrPsDb;Trusted_Connection=True;");
        }
    }
}
