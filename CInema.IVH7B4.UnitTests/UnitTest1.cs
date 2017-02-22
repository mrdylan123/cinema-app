using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;
using System.Linq;

namespace CInema.IVH7B4.UnitTests {
    [TestClass]
    public class UnitTest1 {

       // [TestMethod]
        public void TestInsertLocation()
        {
            var context = new EFDbContext();

            context.Locations.Add(new Location()
            {
                City = "Breda",
                kidDiscount = 0.0m,
                Country = "Netherlands",
                Name = "Pathe Breda",
                Province = "Noord Brant",
                Rooms = 4,
                SeniorDiscount = 0.0m,
                StudentDiscount = 0.0m,
                TicketPriceLong = 0.0m,
                TicketPriceNormal = 0.0m,
            });

            context.SaveChanges();
      //      Assert.IsNotNull(context.Locations.ToList().Find(l => l.Name == "Pathe Breda"));
        }


      //  [TestMethod]
        public void TestInsertFilm() {
            var context = new EFDbContext();

            context.Films.Add(new Film() {
                Name = "50 shades of Maikel",
                Language = "NL",
                LanguageSubs = "",
                Age = 18,
                FilmType = 1,
                Description = "Niet een familiefilm",
                Image = null,
                Length = 90,
                Is3D = false,
                Location = context.Locations.ToList()[0],
            });

            context.SaveChanges();
     //       Assert.AreEqual(0, context.Films.Count());
        }

      //  [TestMethod]
        public void TestInsertRoomLayout()
        {
            var context = new EFDbContext();

            context.RoomLayouts.Add(new RoomLayout()
            {
                BackX = 5,
                BackY = 5,
                FrontX = 5,
                FrontY = 4
            });

            context.SaveChanges();
      //      Assert.AreEqual(0, context.RoomLayouts.Count());
        }

//        [TestMethod]
        public void TestInsertCustomer()
        {
            var context = new EFDbContext();

            context.Customers.Add(new Customer()
            {
                Name = "Maikel",
                BirthDate = new DateTime(1992, 04, 18),
                BirthPlace = "Tilburg",
                Email = "test@test.nl",
                MaDiWoDoFreeTickets = 0, 
                Telephone = "065757",
                NationaleBioscoopBonCredit = 0.0m
            });

            context.SaveChanges();
      //      Assert.AreEqual(1, context.Customers.Count());
        }

    //    [TestMethod]
        public void TestInsertManager()
        {
            var context = new EFDbContext();

            context.Managers.Add(new Manager()
            {
                Name = "Maikel",
                BirthDate = new DateTime(1992, 04, 18),
                BirthPlace = "Tilburg",
                Email = "test@test.nl",
                Telephone = "065757",
            });

            context.SaveChanges();
     //       Assert.AreEqual(1, context.Managers.Count());
        }

      //  [TestMethod]
        public void TestInsertRoom()
        {
            var context = new EFDbContext();

            context.Rooms.Add(new Room()
            {
                RoomNumber = 1,
                Layout = context.RoomLayouts.ToList()[0],
            });

            context.SaveChanges();
      //      Assert.AreEqual(1, context.Rooms.Count());
        }

       // [TestMethod]
        public void TestInsertSeat()
        {
            var context = new EFDbContext();

            context.Seats.Add(new Seat()
            {
                RowX = 1,
                RowY = 1,
                seatNo = 1,
                SeatType = 0,
                Room = context.Rooms.ToList()[0]
            });

            context.SaveChanges();
    //        Assert.AreEqual(1, context.Seats.Count());
        }

      //  [TestMethod]
        public void TestInsertShowing() { 
            var context = new EFDbContext();

            context.Showings.Add(new Showing()
            {
                BeginDateTime = new DateTime(2017, 4, 18),
                EndDateTime = new DateTime(2017, 4, 19),
                Film = context.Films.ToList()[0],
                Room = context.Rooms.ToList()[0]
            });

            context.SaveChanges();
    //        Assert.AreEqual(1, context.Showings.Count());
        }

       // [TestMethod]
        public void TestInsertTickest()
        {
            var context = new EFDbContext();

            context.Tickets.Add(new Ticket()
            {
                Customer = context.Customers.ToList()[0],
                Discount = 0.0m,
                Price = 0.0m,
                Seat = context.Seats.ToList()[0],
                SecretKey = "test",
                TicketType = 0,
            });

            context.SaveChanges();
    //        Assert.AreEqual(1, context.Tickets.Count());
        }

    }
}
