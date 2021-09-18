using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText;
using tourplanner.Models;
using System.Configuration;
using iText.Layout;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Collections.ObjectModel;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas.Draw;

namespace tourplanner
{
    class PdfGenerator
    {
        private string reportpath = ConfigurationManager.AppSettings.Get("reportpath");
        private string mapspath = ConfigurationManager.AppSettings.Get("mapspath");


        public void CreateReport(Tour t)
        {
            PdfWriter writer = new PdfWriter(reportpath + t.tour_Name +".pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4.Rotate());
            Paragraph header = new Paragraph(t.tour_Name)
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);
            Paragraph subheader = new Paragraph(t.tour_From + " - " + t.tour_To)
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(15);
            Image img = new Image(ImageDataFactory
                .Create(mapspath + t.tour_ID + ".jpg"))
                .SetTextAlignment(TextAlignment.CENTER);
            Paragraph details = new Paragraph(
                "Description: " + t.tour_Description + "\n" +
                "Distance: " + t.tour_Distance + " km\n" )
                .SetFontSize(12);
            document.Add(header);
            document.Add(subheader);
            document.Add(img);
            document.Add(details);
            document.Add(new AreaBreak());
            foreach (Log l in t.Logs)
            {
                Paragraph log = new Paragraph(
                    $"Name: {l.log_Name} \n" +
                    $"Author: {l.log_Author} \n" +
                    $"Date: {l.log_Date} \n" +
                    $"Transport: {l.log_Transport} \n" +
                    $"Report: {l.log_Report} \n" +
                    $"Rating: {l.log_Rating}/5 \n" +
                    $"Distance: {l.log_Distance} km \n" +
                    $"Duration: {l.log_Duration} min \n" +
                    $"Speed: {l.log_Speed} km/h \n" +
                    $"Energy: {l.log_Energy} joule \n");
                    LineSeparator ls = new LineSeparator(new SolidLine());
                    document.Add(log);
                    document.Add(ls);
            }


            
            document.Close();
        }

        public void CreateSummary(ObservableCollection<Tour> tours)
        {
            PdfWriter writer = new PdfWriter(reportpath + "Summary.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("Summary")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);
            double totaldistance = 0;
            double totaldistancewalked = 0;
            double totaldistancecycled = 0;
            double totaldistancebiked = 0;
            double totaldistancedriven = 0;
            int totaltours = tours.Count();
            int totallogs = 0;
            int totaltime = 0;
            int totalwalkers = 0;
            int totalcyclers = 0;
            int totalbikers = 0;
            int totaldrivers = 0;
            double totalspeed = 0;
            double averagespeed = 0;
            
            
            foreach(Tour tour in tours)
            {
                foreach(Log log in tour.Logs)
                {
                    totallogs++;
                    totaldistance += log.log_Distance;
                    totalspeed += log.log_Speed;
                    totaltime += log.log_Duration;
                    if(log.log_Transport == "Walk")
                    {
                        totalwalkers++;
                        totaldistancewalked += log.log_Distance;
                    }
                    if (log.log_Transport == "Bicycle")
                    {
                        totalcyclers++;
                        totaldistancecycled += log.log_Distance;
                    }
                    if (log.log_Transport == "Motorbike")
                    {
                        totalbikers++;
                        totaldistancebiked += log.log_Distance;
                    }
                    if (log.log_Transport == "Car")
                    {
                        totaldrivers++;
                        totaldistancedriven += log.log_Distance;
                    }
                }
            }
            averagespeed = totalspeed / totallogs;
            Paragraph stats = new Paragraph(
                "\n Number of Tours: " + totaltours.ToString() +
                "\n Number of Logs: " + totallogs.ToString() +
                "\n Total Distance traveled: " + totaldistance.ToString() + " km" +
                "\n Average Speed: " + averagespeed.ToString() + " km/h" +
                "\n Number of Walks walked: " + totalwalkers.ToString() +
                "\n Total Distance walked: " + totaldistancewalked.ToString() + " km" +
                "\n Number of Bicycle tours cycled: " + totalcyclers.ToString() +
                "\n Total Distance cycled: " + totaldistancecycled.ToString() + " km" +
                "\n Number of Bike tours ridden: " + totalbikers.ToString() +
                "\n Distance biked: " + totaldistancebiked.ToString() + " km" +
                "\n Number of car drived driven: " + totaldrivers.ToString() +
                "\n Distance driven: " + totaldistancedriven.ToString() + " km" );

            document.Add(header);
            document.Add(stats);
            document.Close();
        }

    }
}
