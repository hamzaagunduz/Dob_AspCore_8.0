﻿using Domain.Entities;
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
        public DobContext(DbContextOptions<DobContext> options) : base(options)
        {
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<FlashCard> FlashCards { get; set; }
        public DbSet<UserStatistics> UserStatistics { get; set; }
        public DbSet<DailyMission> DailyMissions { get; set; }
        public DbSet<UserDailyMission> UserDailyMissions { get; set; }
        public DbSet<ShopItem> ShopItems { get; set; }
        public DbSet<UserShopItem> UserShopItems { get; set; }
        public DbSet<UserTopicPerformance> UserTopicPerformances { get; set; }
        public DbSet<QuestionImage> QuestionImages { get; set; }
        public DbSet<TestGroup> TestGroups { get; set; }
        public DbSet<DiamondPackItem> DiamondPackItems { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistories { get; set; }
        public DbSet<AppUserFlashCard> AppUserFlashCards { get; set; }
        public DbSet<UserCustomFlashCard> UserCustomFlashCards { get; set; }
        public DbSet<AISetting> AISettings { get; set; }
        public DbSet<IyzipaySetting> IyzipaySettings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique(); 
        }



    }
}
