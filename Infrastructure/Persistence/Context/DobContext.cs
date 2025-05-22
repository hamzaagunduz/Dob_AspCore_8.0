using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class DobContext:IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-O82KITD;initial Catalog=DobDb;integrated Security=true; TrustServerCertificate=True");

        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<FlashCard> FlashCards { get; set; }
        public DbSet<UserStatistics> UserStatistics { get; set; }

    }
}
