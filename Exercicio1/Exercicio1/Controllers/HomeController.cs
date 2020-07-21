using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Exercicio1.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Exercicio1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendCSV(IFormFile fileUpload)
        {
            if(fileUpload != null && fileUpload.Length > 0)
            {
                if(fileUpload.FileName.EndsWith(".csv"))
                {
                    var ms = new MemoryStream();

                    fileUpload.CopyTo(ms);
                    var fileBytes = ms.ToArray();

                    var text = System.Text.Encoding.UTF8.GetString(fileBytes, 0, fileBytes.Length);

                    var sendModel = new Formatacao().CreateObjects(text);
                    ViewBag.msg = sendModel.Response.Message;
                    ViewBag.Items = sendModel.MediaValorList;
                }
            }
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
