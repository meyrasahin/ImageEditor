using ImageEditor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;

namespace ImageEditor.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            string[] fileEntries = Directory.GetFiles("wwwroot/Images/");
            for(int a = 0; a<fileEntries.Length; a++)
            {
                fileEntries[a] = fileEntries[a].Substring(fileEntries[a].LastIndexOf('/')+1);
            }
            return View(fileEntries);
        }

       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
