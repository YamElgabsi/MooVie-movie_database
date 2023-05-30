using Microsoft.EntityFrameworkCore;
using MovieReviewsProjectAPI.Models;

namespace MovieReviewsProjectAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=MoViewDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        //entities
        public DbSet<Company> Companies { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Star> Stars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieStar> MovieStar { get; set; }

    }
}
