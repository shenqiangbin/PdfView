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
            //ExecutCmd(cmdStr, args);
            ExecuteCmdAdmin($"{cmdStr} {args}");

            return Redirect($"/html/{name}.html");
        }

        private string BuildAgrs(string fileName)
        {
            //D:\Git\PdfView\PdfViewViaHTML\pdf2htmlEX>pdf2htmlEX.exe --dest-dir ../html ../pdf/1.pdf
            string pdfPath = Server.MapPath("~/PDF");
            return $" --zoom 3.6 --embed cfijo --split-pages 1 --dest-dir e:/pdfhtml1234 --page-filename {fileName}-%d.page  {pdfPath}/{fileName}.pdf";
        }

        private void ExecutCmd(string cmd, string args)
        {
            string output = "";
            using (Process p = new Process())
            {
                p.StartInfo.FileName = cmd;
                p.StartInfo.Arguments = args;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.PriorityClass = ProcessPriorityClass.High;
                output += p.StandardOutput.ReadToEnd();
                output += p.StandardError.ReadToEnd();
                p.WaitForExit();               
            }
            System.IO.File.WriteAllText("d:/1.txt",output);
        }

        private void ExecuteCmdAdmin(string sql)
        {
            //c:\windows\system32\inetsrv>exit
            //Cannot create temp directory: Permission denied
            string output = "";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c C:\\Windows\\System32\\cmd.exe";
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.Verb = "RunAs";
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.StandardInput.WriteLine(sql);
            process.StandardInput.WriteLine("exit");
            output += process.StandardOutput.ReadToEnd();
            output += process.StandardError.ReadToEnd();

            process.WaitForExit();
            System.IO.File.AppendAllText("d:/1.txt", output);
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