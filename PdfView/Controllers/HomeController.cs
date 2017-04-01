using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PdfView.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Handle();
            return View();
        }

        private void Handle()
        {
            string cmdStr = "C:/Program Files (x86)/SWFTools/pdf2swf.exe";
            //string cmdStr =HttpContext.Current.Server.MapPath("E://Program Files (x86)//SWFTools//pdf2swf.exe");
            string savePath = Server.MapPath("/SWF");
            string filePath = Server.MapPath("/PDF/1.pdf");
            string args = "  -t " + filePath + "  -o " + savePath + "//MYTEST.swf";
            args = " -t e:/1.pdf -o e:/1.swf";
            //args = "  -t c:/users/sks/documents/visual studio 2015/Projects/PdfView/PdfView/PDF/1.pdf  -o c:/users/sks/documents/visual studio 2015/Projects/PdfView/PdfView/SWF/MYTEST.swf";
            PDF2SWF.ExecutCmd(cmdStr, args);
        }
    }
}