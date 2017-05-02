using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PdfOpen.Controllers
{
    public class ABCController : Controller
    {
        public ActionResult Abc()
        {
            var htmlContent = System.IO.File.ReadAllText(Server.MapPath("~/Views/abc/abc.html"));
            //var htmlContent = "<p>hiii </p>";
            Createpdf(htmlContent);
            return new EmptyResult();
        }

        public void Createpdf(string htmlContent)
        {
            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
            var appDataPath = Server.MapPath("~/App_Data");
            var filename = string.Format(@"{0}.pdf", DateTime.Now.Ticks);
            var path = Path.Combine(appDataPath, filename);
            System.IO.File.WriteAllBytes(path, pdfBytes.ToArray());

            Response.ContentType = "application/pdf";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("Content-Disposition", "Inline; filename=TEST.pdf");
            Response.BinaryWrite(pdfBytes);
            Response.Flush();
            Response.End();
        }
    }
}