﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class AutomaticSeatSelection
    {
        public List<String> debug { get; set; }

        public AutomaticSeatSelection()
        {
            debug = new List<String>();
        }

        public struct Coord
        {
            public int Y;
            public int X;

            public Coord(int y, int x)
            {
                this.Y = y;
                this.X = x;
            }
        }

        public void VisualizeSeats(Room room, List<Seat> occupiedSeats)
        {
            // get seats where roomid is equal to room roomid and where there are tickets off with the ticketid
           // List<Seat> occupiedSeats = context.Seats.ToList().Where(s => s.RoomID == room.RoomID && context.Tickets.ToList().Find(t => t.SeatID == s.SeatID) != null).ToList();
            var layout = room.Layout;
            debug.Add("Front: ");

            for (int i = 0; i < layout.FrontY; i++)
            {
                String str = "";
                for(int j = 0; j < layout.FrontX; j++)
                {
                    str += (IsOccupiedSeat(occupiedSeats, i, j, false) ? "X" : "O");
                }
                debug.Add(str);
            }
            debug.Add("Back:");
            for (int i = 0; i < layout.BackY; i++)
            {
                String str = "";

                for (int j = 0; j < layout.BackX; j++)
                {
                    str += (IsOccupiedSeat(occupiedSeats, i + layout.FrontY, j, false) ? "X" : "O");
                }
                debug.Add(str);
            }
        }

        public List<Coord> VisualizeSelectedSeats(Room room, int seats, List<Seat> occupiedSeats)
        {
            List<Coord> coordList = new List<Coord>();
            if (seats == 0) return coordList;

            // get seats where roomid is equal to room roomid and where there are tickets off with the ticketid
            // List<Seat> occupiedSeats = context.Seats.ToList().Where(s => s.RoomID == room.RoomID && context.Tickets.ToList().Find(t => t.SeatID == s.SeatID) != null).ToList();


            var layout = room.Layout;
            bool found = false;

            int currentY = layout.BackY / 2;
            // find in middle and back of backY
            while (found == false && currentY < layout.BackY && layout.BackY != 0)
            {
                int x = CheckRow(currentY + layout.FrontY, layout.BackX, seats, occupiedSeats);

                if (x != -1)
                {
                    AddCoords(coordList, currentY + layout.FrontY, x, seats);
                    found = true;
                    break;
                }
                currentY++;
            }

            // find in front of frontY
            currentY = (layout.BackY / 2) - 1;
            while (found == false && currentY >= 0 && layout.BackY != 0)
            {
                int x = CheckRow(currentY + layout.FrontY, layout.BackX, seats, occupiedSeats);
                if (x != -1)
                {
                    AddCoords(coordList, currentY + layout.FrontY, x, seats);

                }

                currentY--;
            }

            // mid row
            currentY = layout.FrontY / 2;

            // find in middle and back of frontY
            while (found == false && currentY < layout.FrontY && layout.FrontY != 0)
            {
                int x = CheckRow(currentY, layout.FrontX, seats, occupiedSeats);

                if (x != -1)
                {
                    AddCoords(coordList, currentY, x, seats);
                    found = true;
                    break;
                }
                currentY++;
            }

            // find in front of frontY
            currentY = (layout.FrontY / 2) - 1;
            while (found == false && currentY >= 0 && layout.FrontY !=0)
            {
                int x = CheckRow(currentY, layout.FrontX, seats, occupiedSeats);
                if (x != -1)
                {
                    AddCoords(coordList, currentY, x, seats);
                    found = true;
                    break;
                }

                currentY--;
            }

            

            return coordList;
        }

        public void AddCoords(List<Coord> coordList, int Y, int X, int numseats)
        {
            for (int i = 0;  i < numseats; i++)
            {
                coordList.Add(new Coord(Y, X + i));
            }        
        }

        // find X coordinate for first row of seats, or -1 if none found
        public int CheckRow(int Y, int maxX, int numSeats, List<Seat> occupiedSeats)
        {
            int midX = maxX / 2;
            int leftX = midX;
            int rightX = midX;

            while (leftX >= 0 || rightX <= maxX)
            {
                if (leftX >= 0)
                {
                    int pos = CheckAdjacentRows(occupiedSeats, leftX, maxX, numSeats, Y);

                    if (pos != -1)
                    {
                        return pos;
                    }
                }

                if (rightX <= maxX)
                {
                    int pos = CheckAdjacentRows(occupiedSeats, rightX, maxX, numSeats, Y);

                    if (pos != -1)
                    {
                        return pos;
                    }
                }

                leftX--;
                rightX++;
            }

            return -1;
        }

        public int CheckAdjacentRows(List<Seat> occupiedSeats, int checkX, int maxX, int numSeats, int y)
        {
            debug.Add(String.Format("Start checking Y = {0}, checkX = {1}, maxX = {2}, numSeats = {3}", y, checkX, maxX, numSeats));
            for (int i = 0; i < numSeats; i++)
            {
                int currentX = checkX + i;
                if (currentX >= maxX)
                {
                    break;
                }
                    // check if there is a seat with same Y and X as current seat
                    // and stop the inner j loop if its the case
                if (IsOccupiedSeat(occupiedSeats, y, currentX, true) == true)
                    {
                        break;
                    }

                if (i == numSeats - 1)
                {
                    debug.Add("Found!");
                    //return -1; // DEBUG
                    return checkX; // i has x position of row, first of seats
                }
            }
            return -1;
        }

        public bool IsOccupiedSeat(List<Seat> seats, int y, int x, bool debug_)
        {
            if (debug_)
            {
                debug.Add(String.Format("   Checking: Y = {0}, X = {1}", y, x));
            }
            foreach(Seat s in seats)
            {
                if (s.RowX == x && s.RowY == y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}