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
            string cmdStr = "C:/Program Files/SWFTools/pdf2swf.exe";
            string savePath = Server.MapPath("/SWF");
            string filePath = Server.MapPath("/PDF/1.pdf");
            string args = "  -t " + filePath + "  -o " + savePath + "//MYTEST.swf";
            //args = " -t d:/1.pdf -o d:/1.swf -T 9 -f";// -T 9 表示版本9 -f 实现搜索时，高亮显示            
            args = BuildAgrs(filePath, savePath + "//1.swf");
            PDF2SWF.ExecutCmd(cmdStr, args);
        }

        private string BuildAgrs(string filePath, string savePath)
        {
            return $" -t {filePath} -o {savePath} -T 9 -f";
        }

    }
}