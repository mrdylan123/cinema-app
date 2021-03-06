﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cinema.IVH7B4.Domain.Entities;
using System.Collections.Generic;
using Cinema.IVH7B4.WebUI.Models;

namespace CInema.IVH7B4.UnitTests
{
    [TestClass]
    public class AutomaticSeatSelectionTest
    {
        private bool AreCoordListsEqual(List<SeatCoord> l1, List<SeatCoord> l2)
        {
            if (l1.Count != l2.Count)
            {
                return false;
            }

            for (int i = 0; i < l1.Count; i++)
            {
                if (l1[i].X != l2[i].X)
                {
                    return false;
                }

                if (l1[i].Y != l2[i].Y)
                {
                    return false;
                }
            }

            return true;
        }

        [TestMethod]
        public void BackRowCheckPassTest()
        {
            var room = new Room()
            {
                Layout = new RoomLayout()
                {
                    BackX = 5,
                    BackY = 6,
                    FrontX = 0,
                    FrontY = 0,
                    LayoutID = 888
                }
            };

            List<Seat> occupiedSeats = new List<Seat>()
            {
            };

            var ass = new AutomaticSeatSelection();
            List<SeatCoord> ret = ass.CalculateSeats(room, 3, occupiedSeats);
            List<SeatCoord> expected = new List<SeatCoord>()
            {
                new SeatCoord(3,2),
                new SeatCoord(3,3),
                new SeatCoord(3,4)
            };

            Assert.IsTrue(AreCoordListsEqual(ret, expected));
        }

        [TestMethod]
        public void BackRowCheckFailTest()
        {
            var room = new Room()
            {
                Layout = new RoomLayout()
                {
                    BackX = 5,
                    BackY = 6,
                    FrontX = 0,
                    FrontY = 0,
                    LayoutID = 888
                }
            };

            List<Seat> occupiedSeats = new List<Seat>()
            {
            };

            var ass = new AutomaticSeatSelection();
            List<SeatCoord> ret = ass.CalculateSeats(room, 3, occupiedSeats);
            List<SeatCoord> expected = new List<SeatCoord>()
            {
                new SeatCoord(3,2),
                new SeatCoord(3,3),
                new SeatCoord(0,0) //  THIS COORD IS INCORRECT SO WE CAN TEST
            };

            Assert.IsFalse(AreCoordListsEqual(ret, expected));
        }

        [TestMethod]
        public void FrontRowCheckPassTest()
        {
            var room = new Room()
            {
                Layout = new RoomLayout()
                {
                    BackX = 0,
                    BackY = 0,
                    FrontX = 3,
                    FrontY = 3,
                    LayoutID = 888
                }
            };

            List<Seat> occupiedSeats = new List<Seat>()
            {
            };

            var ass = new AutomaticSeatSelection();
            List<SeatCoord> ret = ass.CalculateSeats(room, 3, occupiedSeats);
            List<SeatCoord> expected = new List<SeatCoord>()
            {
                new SeatCoord(1,0),
                new SeatCoord(1,1),
                new SeatCoord(1,2)
            };

            Assert.IsTrue(AreCoordListsEqual(ret, expected));
        }

        [TestMethod]
        public void FrontRowCheckFailTest()
        {
            var room = new Room()
            {
                Layout = new RoomLayout()
                {
                    BackX = 0,
                    BackY = 0,
                    FrontX = 3,
                    FrontY = 3,
                    LayoutID = 888
                }
            };

            List<Seat> occupiedSeats = new List<Seat>()
            {
            };

            var ass = new AutomaticSeatSelection();
            List<SeatCoord> ret = ass.CalculateSeats(room, 3, occupiedSeats);
            List<SeatCoord> expected = new List<SeatCoord>()
            {
                new SeatCoord(1,0),
                new SeatCoord(1,5), //  THIS COORD IS INCORRECT SO WE CAN TEST
                new SeatCoord(1,2) 
            };

            Assert.IsFalse(AreCoordListsEqual(ret, expected));
        }

        [TestMethod]
        public void BackRowPlusFrontRowCheckPassTest()
        {
            var room = new Room()
            {
                Layout = new RoomLayout()
                {
                    BackX = 5,
                    BackY = 6,
                    FrontX = 10,
                    FrontY = 4,
                    LayoutID = 888
                }

            };

           List<Seat> occupiedSeats = new List<Seat>()
           {
           };

            var ass = new AutomaticSeatSelection();
            List<SeatCoord> ret = ass.CalculateSeats(room, 3, occupiedSeats);
            List<SeatCoord> expected = new List<SeatCoord>()
            {
                new SeatCoord(7,2),
                new SeatCoord(7,3),
                new SeatCoord(7,4)
            };

            Assert.IsTrue(AreCoordListsEqual(ret, expected));
        }

        [TestMethod]
        public void BackRowPlusFrontRowCheckFailTest()
        {
            var room = new Room()
            {
                Layout = new RoomLayout()
                {
                    BackX = 5,
                    BackY = 6,
                    FrontX = 10,
                    FrontY = 4,
                    LayoutID = 888
                }

            };

            List<Seat> occupiedSeats = new List<Seat>()
            {
            };

            var ass = new AutomaticSeatSelection();
            List<SeatCoord> ret = ass.CalculateSeats(room, 3, occupiedSeats);
            List<SeatCoord> expected = new List<SeatCoord>()
            {
                new SeatCoord(7,2),
                new SeatCoord(7,3),
                new SeatCoord(8,4) //  THIS COORD IS INCORRECT SO WE CAN TEST
            };

            Assert.IsFalse(AreCoordListsEqual(ret, expected));
        }

        [TestMethod]
        public void BackRowPlusFrontRowPlusOccupiedFirstCheckedRowCheckPassTest()
        {
            var room = new Room()
            {
                Layout = new RoomLayout()
                {
                    BackX = 5,
                    BackY = 6,
                    FrontX = 10,
                    FrontY = 4,
                    LayoutID = 888
                }

            };

            List<Seat> occupiedSeats = new List<Seat>()
           {
               new Seat() {RowX = 1, RowY = 7 },
               new Seat() {RowX = 2, RowY = 7 },
               new Seat() {RowX = 3, RowY = 7 },
           };

            var ass = new AutomaticSeatSelection();
            List<SeatCoord> ret = ass.CalculateSeats(room,3, occupiedSeats);
            List<SeatCoord> expected = new List<SeatCoord>()
            {
                new SeatCoord(8,2),
                new SeatCoord(8,3),
                new SeatCoord(8,4)
            };

            Assert.IsTrue(AreCoordListsEqual(ret, expected));
        }

        [TestMethod]
        public void BackRowPlusFrontRowPlusOccupiedFirstCheckedRowCheckFailTest()
        {
            var room = new Room()
            {
                Layout = new RoomLayout()
                {
                    BackX = 5,
                    BackY = 6,
                    FrontX = 10,
                    FrontY = 4,
                    LayoutID = 888
                }

            };

            List<Seat> occupiedSeats = new List<Seat>()
           {
               new Seat() {RowX = 1, RowY = 7 },
               new Seat() {RowX = 2, RowY = 7 },
               new Seat() {RowX = 3, RowY = 7 },
           };

            var ass = new AutomaticSeatSelection();
            List<SeatCoord> ret = ass.CalculateSeats(room, 3, occupiedSeats);
            List<SeatCoord> expected = new List<SeatCoord>()
            {
                new SeatCoord(8,2),
                new SeatCoord(8,3),
                new SeatCoord(8,1) //  THIS COORD IS INCORRECT SO WE CAN TEST
            };

            Assert.IsFalse(AreCoordListsEqual(ret, expected));
        }

        [TestMethod]
        public void BackRowPlusFrontRowPlusFiveSeatsCheckPassTest()
        {
            var room = new Room()
            {
                Layout = new RoomLayout()
                {
                    BackX = 5,
                    BackY = 6,
                    FrontX = 10,
                    FrontY = 4,
                    LayoutID = 888
                }

            };

            List<Seat> occupiedSeats = new List<Seat>()
           {
           };

            var ass = new AutomaticSeatSelection();
            List<SeatCoord> ret = ass.CalculateSeats(room, 5, occupiedSeats);
            List<SeatCoord> expected = new List<SeatCoord>()
            {
                new SeatCoord(7,0),
                new SeatCoord(7,1),
                new SeatCoord(7,2),
                new SeatCoord(7,3),
                new SeatCoord(7,4)
            };

            Assert.IsTrue(AreCoordListsEqual(ret, expected));
        }

        [TestMethod]
        public void BackRowPlusFrontRowPlusFiveSeatsCheckFailTest()
        {
            var room = new Room()
            {
                Layout = new RoomLayout()
                {
                    BackX = 5,
                    BackY = 6,
                    FrontX = 10,
                    FrontY = 4,
                    LayoutID = 888
                }

            };

            List<Seat> occupiedSeats = new List<Seat>()
            {
            };

            var ass = new AutomaticSeatSelection();
            List<SeatCoord> ret = ass.CalculateSeats(room, 5, occupiedSeats);
            List<SeatCoord> expected = new List<SeatCoord>()
            {
                new SeatCoord(7,0),
                new SeatCoord(7,1),
                new SeatCoord(7,2),
                // ONLY 3 out of 5 expected Coords so we can test FAIL 
            };

            Assert.IsFalse(AreCoordListsEqual(ret, expected));
        }
    }
}
