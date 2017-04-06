using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            string cmdStr = Server.MapPath("~/pdf2htmlEX/pdf2htmlEX.exe");
            string args = BuildAgrs(name);
            ExecutCmd(cmdStr, args);
            
            return Redirect($"/html/{name}.html");
        }

        private string BuildAgrs(string fileName)
        {
            //D:\Git\PdfView\PdfViewViaHTML\pdf2htmlEX>pdf2htmlEX.exe --dest-dir ../html ../pdf/1.pdf
            string pdfPath = Server.MapPath("~/PDF");
            return $" --zoom 3.6 --embed cfijo --split-pages 1 --dest-dir pdfhtml1234 --page-filename {fileName}-%d.page  {pdfPath}/{fileName}.pdf";
        }

        private void ExecutCmd(string cmd, string args)
        {
            using (Process p = new Process())
            {
                p.StartInfo.FileName = cmd;
                p.StartInfo.Arguments = args;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.PriorityClass = ProcessPriorityClass.High;
                p.WaitForExit();               
            }
        }

        private void CopyFile(string sources, string dest)
        {
            DirectoryInfo dinfo = new DirectoryInfo(sources);//注，这里面传的是路径，并不是文件，所以不能保含带后缀的文件
            foreach (FileSystemInfo f in dinfo.GetFileSystemInfos())
            {
                //目标路径destName = 目标文件夹路径 + 原文件夹下的子文件(或文件夹)名字
                //Path.Combine(string a ,string b) 为合并两个字符串
                String destName = Path.Combine(dest, f.Name);
                if (f is FileInfo)//如果是文件就复制
                {
                    System.IO.File.Copy(f.FullName, destName, true);//true代表可以覆盖同名文件
                }
                else//如果是文件夹就创建文件夹然后复制然后递归复制
                {
                    Directory.CreateDirectory(destName);
                    CopyFile(f.FullName, destName);
                }
            }
        }
    }
}