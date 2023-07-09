using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace QRX.Utils
{
    public class Test
    {
        public void Pdf()
        {

            PdfWriter pw = new PdfWriter("C:\\demo.pdf");
            PdfDocument pdfDoc = new PdfDocument(pw);

            Document document = new Document(pdfDoc);
            Paragraph header = new Paragraph("HEADER")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);

            document.Add(header);
            document.Close();
        }

    }
}