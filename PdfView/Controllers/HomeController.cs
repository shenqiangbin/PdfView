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
            return View();
        }

        public ActionResult ViewPDF(string name)
        {
            //ViewBag.Msg = Handle(name);
            ViewBag.FileName = name;
            return View();
        }

        private string Handle(string name)
        {
            string cmdStr = "C:/Program Files/SWFTools/pdf2swf.exe";
            cmdStr = "C:/Program Files (x86)/SWFTools/pdf2swf.exe";

            string savePath = Server.MapPath("/SWF");
            string filePath = Server.MapPath($"/PDF/{name}.pdf");
            string args = BuildAgrs(filePath, savePath + $"/{name}.swf");
            //string args = BuildAgrs(filePath,savePath + $"/{name}%.swf"); 分页

            string result = PDF2SWF.ExecutCmd(cmdStr, args);
            return result;
        }

        private string BuildAgrs(string filePath, string savePath)
        {
            //args = " -t d:/1.pdf -o d:/1.swf -T 9 -f";// -T 9 表示版本9 -f 实现搜索时，高亮显示    
            return $" -t {filePath} -o {savePath} -T 9 -f  -s languagedir=e:/xpdf/chinese-simplified";
        }

    }
}