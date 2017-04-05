using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PdfViewViaHTML.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ViewPDF(string name)
        {
            string cmdStr = Server.MapPath("/pdf2htmlEX/pdf2htmlEX.exe");
            string args = BuildAgrs(name);
            ExecutCmd(cmdStr, args);

            using (Process p = new Process())
            {
                p.StartInfo.FileName = cmdStr;
                p.StartInfo.Arguments = args;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.PriorityClass = ProcessPriorityClass.High;
                p.WaitForExit();
                return Redirect($"/html/{name}.html");
            }
           
        }

        private string BuildAgrs(string fileName)
        {
            //D:\Git\PdfView\PdfViewViaHTML\pdf2htmlEX>pdf2htmlEX.exe --dest-dir ../html ../pdf/1.pdf
            return $" --dest-dir ../html ../pdf/{fileName}.pdf";
        }

        private void ExecutCmd(string cmd, string args)
        {
            
        }
    }
}