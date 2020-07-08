using iTextSharp.text;
using iTextSharp.text.pdf;

using System;
using System.Drawing;
using System.IO;

namespace Pdf.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            AddTextToPdf(args[0], args[1], args[2], new Point(Convert.ToInt32(args[3].Split(',')[0]), Convert.ToInt32(args[3].Split(',')[1])));
        }

        public static void AddTextToPdf(string inputPdfPath, string outputPdfPath, string textToAdd, Point point)
        {
            //variables
            string pathin = inputPdfPath;
            string pathout = outputPdfPath;

            //create PdfReader object to read from the existing document
            using (PdfReader reader = new PdfReader(pathin))
            //create PdfStamper object to write to get the pages from reader 
            using (PdfStamper stamper = new PdfStamper(reader, new FileStream(pathout, FileMode.Create)))
            {
                //select two pages from the original document
                reader.SelectPages("1-2");

                //gettins the page size in order to substract from the iTextSharp coordinates
                var pageSize = reader.GetPageSize(1);

                // PdfContentByte from stamper to add content to the pages over the original content
                PdfContentByte pbover = stamper.GetOverContent(1);

                //add content to the page using ColumnText
                Font font = new Font();
                font.Size = 45;

                //setting up the X and Y coordinates of the document
                int x = point.X;
                int y = point.Y;

                y = (int)(pageSize.Height - y);

                ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase(textToAdd, font), x, y, 0);
            }
        }
    }
}
