using System;
using System.Collections.Generic;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CInema.IVH7B4.UnitTests {
    [TestClass]
    public class UnitTestPDF {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestMethodWrite() {

            List<Ticket> tickets = new List<Ticket>();
            Ticket ticket = new Ticket();
            tickets.Add(ticket);

            Location loc = new Location();

            PDFGenerator generator = new PDFGenerator(tickets,loc);
                var result = generator.Write(tickets, loc);
        }
    }
}
