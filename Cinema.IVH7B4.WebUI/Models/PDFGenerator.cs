using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Cinema.IVH7B4.Domain.Entities;
using System.Web.Mvc;
using Font = iTextSharp.text.Font;
using Image = System.Drawing.Image;


namespace Cinema.IVH7B4.WebUI.Models {
    public class PDFGenerator {
        private Document doc;
        private PdfWriter writer;
        private MemoryStream ms;
        private Font normalFont;
        private Font largeFont;
        private Font smallFont;
        private PdfImage image;

        public PDFGenerator(List<Ticket> tickets, Location loc) {
            normalFont = FontFactory.GetFont("Segoe UI", 8.0f, BaseColor.BLACK);
            smallFont = FontFactory.GetFont("Segoe UI", 6.0f, BaseColor.BLACK);
            largeFont = FontFactory.GetFont("Segoe UI", 10.0f, BaseColor.BLACK);

            doc = new Document(PageSize.A6);
            ms = new MemoryStream();
            writer = PdfWriter.GetInstance(doc, ms);
            Write(tickets, loc);

        }

        public bool Write(List<Ticket> tickets, Location loc) {
            writer.Open();
            doc.Open();

            bool first = true;
            foreach (Ticket t in tickets) {
                if (first == false) {
                    doc.NewPage();
                }
                first = false;
                addText(loc.Name, largeFont);

                // add two empty lines
                AddEmptyLine();
                AddEmptyLine();

                // start ticket info
                addText("TicketID: " + t.TicketID);
                addText("TicketType: " + GetDutchString(t.TicketType));
                addText("Reserveringsnummer: " + t.SecretKey);
                AddImage(t.Showing.Film.Image);
                String ot = t.Showing.Film.LanguageSubs != "" ? t.Showing.Film.LanguageSubs : "geen";
                addText("Film: " + t.Showing.Film.Name + "       " + "Taal: " + t.Showing.Film.Language + "       " + "Ondertiteling: " + ot);
                addText("Leeftijd: " + t.Showing.Film.Age + " jaar en ouder" + "                         " + "Categorie: " + ((FilmType)t.Showing.Film.FilmType).GetDutchString());
                addText("Duur: " + t.Showing.Film.Length + " minuten" + "              " + "3D: " + (t.Showing.Film.Is3D ? "Ja" : "Nee") + "        "
                        + "Prijs: " + t.Price.ToString());


                /// example: maandag 1 Februari 2017
                addText("Datum: " + t.Showing.BeginDateTime.ToString("dddd d MMMM yyyy"), smallFont);
                // example: 23:45
                addText("Begintijd: " + t.Showing.BeginDateTime.ToString("HH:mm") + " - " + "Eindtijd: " + t.Showing.EndDateTime.ToString("HH:mm"), smallFont);
                addText("Locatie: " + loc.Name + "   " + "Zaal: " + t.Seat.Room.RoomNumber, smallFont);
                addText("Stoelnummer: " + t.Seat.seatNo + "   " + "Rijnummer:" + t.Seat.RowY, smallFont);
            }

            doc.Close();
            writer.Close();
            return true;
        }


        public MemoryStream getMemoryStream() {
            return ms;
        }

        public void addText(String txt, Font font) {
            doc.Add(new Paragraph(txt, font));
        }

        public void addText(String txt) {
            doc.Add(new Paragraph(txt, normalFont));
        }

        public void AddImage(byte[] img) {
            iTextSharp.text.Image Image = iTextSharp.text.Image.GetInstance(img);

            Image.Alignment = 3;
            Image.ScaleAbsolute(150, 150);
            doc.Add(Image);
        }

        public void AddEmptyLine() {
            addText(" ");
        }

        public ActionResult SendPdf() {
            MemoryStream stream = getMemoryStream();
            FileStreamResult result = new FileStreamResult(new MemoryStream(stream.GetBuffer()), "pdf/application");
            result.FileDownloadName = "image.pdf";
            return result;
        }

        public static String GetDutchString(int ticketType) {
            switch ((TicketType)ticketType) {
                case TicketType.NormalTicket: return "Normaal"; // 0
                case TicketType.SeniorTicket: return "65+"; // 1
                case TicketType.ChildTicket: return "Kind"; // 2
                case TicketType.StudentTicket: return "Student"; // 3

                default: return "ERROR";
            }
        }
    }
}