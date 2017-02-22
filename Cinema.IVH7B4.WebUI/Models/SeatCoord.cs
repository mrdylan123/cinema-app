using Cinema.IVH7B4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class SeatCoord
    {
        public int Y;
        public int X;

        public SeatCoord(int y, int x)
        {
            this.Y = y;
            this.X = x;
        }

        public int GetSeatNumber(RoomLayout layout)
        {
            int rowSeats = Y > layout.FrontY - 1 ? layout.BackX : layout.FrontX;
            return (Y * rowSeats) + X + 1; // + 1 because the first seat is seat 0 in the code and needs to be seat 1
        }
    }
}