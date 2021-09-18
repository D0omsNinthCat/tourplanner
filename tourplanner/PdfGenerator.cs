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

namespace tourplanner
{
    class PdfGenerator
    {
        private string filePath = ConfigurationManager.AppSettings.Get("filepath") + "Reports\\";

        public void CreateReport(Tour t)
        {
            PdfWriter writer = new PdfWriter(filePath + "Reportdemo.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("HEADER")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);

            document.Add(header);
            document.Close();
        }
        public void CreateSummary(List<Tour> tours)
        {
            PdfWriter writer = new PdfWriter(filePath + "Summarydemo.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph("HEADER")
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(20);

            document.Add(header);
            document.Close();
        }

    }
}
