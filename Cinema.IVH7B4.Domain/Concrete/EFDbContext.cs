﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.Domain.Concrete {
    public class EFDbContext : DbContext {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Showing> Showings { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<RoomLayout> RoomLayouts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers{ get; set; }
        public DbSet<NewsletterRegistration> NewsletterRegistrations { get; set; }
        public DbSet<CashRegisterLogin> CashRegisterLogins { get; set; }
        public DbSet<LostAndFound> LostAndFounds { get; set; }
        public DbSet<ManagerLogin> ManagerLogins { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<BackOfficeLogin> BackOfficeLogins { get; set; }
        public DbSet<TenRidesCard> TenRidesCards { get; set; }
        public DbSet<WeekDayCard> WeekDayCards { get; set; }
        public DbSet<SubscriptionHolder> SubscriptionHolders { get; set; }

        public EFDbContext() : base("EFDbContext")
        {

        }

    }
}
