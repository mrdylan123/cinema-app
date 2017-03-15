using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class CinemaViewModel
    {
        public List<List<AutomaticSeatSelection.OccupyType>> SeatSelectionGUI;
        public Location SelectedLocation;
        public Film SelectedFilm;
        public Showing SelectedShowing;
        public List<SeatCoord> SeatCoordList;
        private String pinValue;
        public string WrongPingValue { get; set; }
        public NormalTicketOrder NormalTicketOrder;
        public ChildTicketOrder ChildTicketOrder;
        public StudentTicketOrder StudentTicketOrder;
        public SeniorTicketOrder SeniorTicketOrder;
        public PopcornTicketOrder PopcornTicketOrder;
        public LadiesTicketOrder LadiesTicketOrder;

        public CinemaViewModel()
        {
            SeatCoordList = new List<SeatCoord>();

            NormalTicketOrder = new NormalTicketOrder(this);
            ChildTicketOrder = new ChildTicketOrder(this);
            StudentTicketOrder = new StudentTicketOrder(this);
            SeniorTicketOrder = new SeniorTicketOrder(this);
            PopcornTicketOrder = new PopcornTicketOrder(this);
            LadiesTicketOrder = new LadiesTicketOrder(this);

            pinValue = "";
        }

        public String PinValue {
            get { return pinValue; }
            set {
                if (value.Length > 4) {
                    pinValue = value.Remove(4);
                } else {
                    pinValue = value;
                }
            }
        }

        public int GetSeatsRow()
        {
            return SeatCoordList[0].Y;
        }

        public String getSeatNumbersString()
        {
            String str = "";

            foreach(SeatCoord sc in SeatCoordList)
            {
                str += sc.GetSeatNumber(SelectedShowing.Room.Layout);
                str += ", ";
            }

            str = str.Remove(str.Length - 2);

            return str;
        }

        public void ResetTickets()
        {
            NormalTicketOrder = new NormalTicketOrder(this);
            ChildTicketOrder = new ChildTicketOrder(this);
            StudentTicketOrder = new StudentTicketOrder(this);
            SeniorTicketOrder = new SeniorTicketOrder(this);
            PopcornTicketOrder = new PopcornTicketOrder(this);
            LadiesTicketOrder = new LadiesTicketOrder(this);
        }

        public void SetTicketQuantity(int quantity, TicketType tt) {
            if (quantity < 0) return;

            switch(tt)
            {
                case TicketType.ChildTicket: ChildTicketOrder.Quantity = quantity; break;
                case TicketType.SeniorTicket: SeniorTicketOrder.Quantity = quantity; break;
                case TicketType.StudentTicket: StudentTicketOrder.Quantity = quantity; break;
                case TicketType.NormalTicket: NormalTicketOrder.Quantity = quantity; break;
                case TicketType.PopcornTicket: PopcornTicketOrder.Quantity = quantity; break;
                case TicketType.LadiesTicket: LadiesTicketOrder.Quantity = quantity; break;
                default: break;
            }
        }

        public int GetAllTicketsQuantity() {
            return ChildTicketOrder.Quantity +
                SeniorTicketOrder.Quantity +
                StudentTicketOrder.Quantity +
                NormalTicketOrder.Quantity +
                PopcornTicketOrder.Quantity +
                LadiesTicketOrder.Quantity;
        }


        public Decimal GetTotalPriceForAllTickets()
        {
            return ChildTicketOrder.GetTotalPrice() +
                SeniorTicketOrder.GetTotalPrice() +
                StudentTicketOrder.GetTotalPrice() +
                NormalTicketOrder.GetTotalPrice() +
                PopcornTicketOrder.GetTotalPrice() + 
                LadiesTicketOrder.GetTotalPrice();
        }

        public bool IsChildTicketSamePriceAsNormalTicket()
        {
            return ChildTicketOrder.GetPrice() == NormalTicketOrder.GetPrice();
        }

        public bool IsStudentTicketValid(DateTime dt)
        {
            return IsMondayTuesdayWednesdayThursday(dt);
        }
        public bool IsSeniorTicketValid(DateTime dt)
        {
            return IsMondayTuesdayWednesdayThursday(dt) && !IsHoliday(dt);
        }

        public bool IsMondayTuesdayWednesdayThursday(DateTime dt)
        {
            return IsMondayTuesdayWednesdayThursday(dt.DayOfWeek);
        }
        public int TotalFreeSeats()
        {
            var rl = SelectedShowing.Room.Layout;
            int roomSeats = (rl.BackX * rl.BackY) + (rl.FrontX * rl.FrontY);
            int seatsUsed = (new EFDbContext()).Tickets.Where(t => t.ShowingID == SelectedShowing.ShowingID)
                .GroupBy(x => x.SeatID).Select(y => y.FirstOrDefault()).ToList().Count();

            return roomSeats - seatsUsed;
        }

        public String GetEuroSign()
        {
            return "€";
        }

        public String GetCurrencyString(Decimal d)
        {
            return GetEuroSign() + d.ToString();
        }

        public List<Seat> GetSeatsList()
        {
            var list = new List<Seat>();

            foreach(SeatCoord sc in SeatCoordList)
            {
                list.Add(new Seat()
                {
                    //Room = SelectedShowing.Room,
                    RoomID = SelectedShowing.Room.RoomID,
                    RowX = sc.X,
                    RowY = sc.Y,
                    seatNo = sc.GetSeatNumber(SelectedShowing.Room.Layout),
                    SeatType = 0,
                });
            }

            return list;
        }

        public List<Ticket> GetTicketsList(List<Seat> seatsList, Customer customer)
        {
            var list = new List<Ticket>();
            //var seatsList = GetSeatsList();

            // To do check per location 3D movies discount/extra cost?
            // 3D movies have extra cost so return minus
            Decimal discount = SelectedFilm.Is3D ? -2.5m : 0.0m;

            //TODO APPLY ADDITIONAL PER LOCATION DISCOUNTS

            String secretKey = GenerateTicketSecretKey();
            foreach (SeatCoord sc in SeatCoordList)
            {
                var ticket = new Ticket()
                {
                    Discount = discount,
                    //Customer = customer,
                    CustomerID =  customer.CustomerID,
                    Price = 0.0m,
                    //Seat = seatsList.Find(s => s.seatNo == sc.GetSeatNumber(SelectedShowing.Room.Layout)),
                    SeatID = seatsList.Where(s => s.RowX == sc.X && s.RowY == s.RowY).Last().SeatID,
                    SecretKey = secretKey, // TODO
                    TicketType = (int)TicketType.InvalidTicket,
                    //Showing = SelectedShowing,
                    ShowingID = SelectedShowing.ShowingID
                };
                list.Add(ticket);

                int normal = 0; int senior = 0; int student = 0; int child = 0; int popcorn = 0; int ladies = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    if (NormalTicketOrder.Quantity > normal)
                    {
                        list[i].TicketType = (int)TicketType.NormalTicket;
                        list[i].Price = NormalTicketOrder.GetPrice();
                        // NO DISCOUNT
                        normal++;
                    }
                    else if (SeniorTicketOrder.Quantity > senior)
                    {
                        list[i].TicketType = (int)TicketType.SeniorTicket;
                        list[i].Price = SeniorTicketOrder.GetPrice();
                        list[i].Discount += SeniorTicketOrder.GetDiscount();
                        senior++;
                    }
                    else if (ChildTicketOrder.Quantity > child)
                    {
                        list[i].TicketType = (int)TicketType.ChildTicket;
                        list[i].Price = ChildTicketOrder.GetPrice();
                        list[i].Discount += ChildTicketOrder.GetDiscount();
                        child++;
                    }
                    else if (StudentTicketOrder.Quantity > student)
                    {
                        list[i].TicketType = (int)TicketType.StudentTicket;
                        list[i].Price = StudentTicketOrder.GetPrice();
                        list[i].Discount += StudentTicketOrder.GetDiscount();
                        student++;
                    }
                    else if (PopcornTicketOrder.Quantity > popcorn)
                    {
                        list[i].TicketType = (int)TicketType.PopcornTicket;
                        list[i].Price = PopcornTicketOrder.GetPrice();
                        list[i].Discount += PopcornTicketOrder.GetDiscount();
                        popcorn++;
                    }
                    else if (LadiesTicketOrder.Quantity > ladies)
                    {
                        list[i].TicketType = (int)TicketType.LadiesTicket;
                        list[i].Price = LadiesTicketOrder.GetPrice();
                        list[i].Discount += LadiesTicketOrder.GetDiscount();
                    }

                }
            }

            return list;
        }

        public String GenerateTicketSecretKey()
        {
            Random rnd1 = new Random(Guid.NewGuid().GetHashCode());
            Random rnd2 = new Random(Guid.NewGuid().GetHashCode());
            Random rnd3 = new Random(Guid.NewGuid().GetHashCode());
            Random rnd4 = new Random(Guid.NewGuid().GetHashCode());

            return rnd1.Next(0, 999999).ToString() + rnd2.Next(0, 999999).ToString()
                + rnd3.Next(0, 999999).ToString() + rnd4.Next(0, 999999).ToString();
        }

        public void PinRemoveChar()
        {
            if (PinValue.Length > 0)
            {
                PinValue = PinValue.Remove(PinValue.Length - 1);
            }
        }

        // use within <img> tags, inside the "src=" arg of <img>
        public String GetFilmBase64String(Film film)
        {
            var base64 = Convert.ToBase64String(film.Image);
            return String.Format("data:image/gif;base64,{0}", base64);
        }

        public bool IsMondayTuesdayWednesdayThursday(DayOfWeek sd)
        {
            if (sd == DayOfWeek.Monday || sd == DayOfWeek.Tuesday
                || sd == DayOfWeek.Wednesday || sd == DayOfWeek.Thursday)
            {
                return true;
            }
            return false;
        }

        public bool IsHoliday(DateTime d) {
            // Kerstvakantie 2017
            if (d >= new DateTime(2017, 12, 23) && d <= new DateTime(2018, 1, 7)) {
                return true;
            }

            // Krokusvakantie/Carnavalsvakantie 2017
            if (d >= new DateTime(2017, 2, 25) && d <= new DateTime(2017, 3, 5)) {
                return true;
            }

            // Meivakantie 2017
            if (d >= new DateTime(2017, 4, 22) && d <= new DateTime(2017, 4, 30)) {
                return true;
            }

            // Zomervakantie 2017
            if (d >= new DateTime(2017, 7, 15) && d <= new DateTime(2017, 8, 27)) {
                return true;
            }

            // Herfstvakantie 2017
            if (d >= new DateTime(2017, 10, 14) && d <= new DateTime(2017, 10, 22)) {
                return true;
            }

            return false;
        }

        public Location findLocationByName(String name)
        {
            return (new EFDbContext()).Locations.ToList().Find(l => l.Name == name);
        }
    }
}