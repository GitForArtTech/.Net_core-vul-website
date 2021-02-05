using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using firstMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductDB Product;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly static Dictionary<string, string> _contentTypes = new Dictionary<string, string>
        {
            {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},  
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                { ".json", "application/json"}

        };

        public ProductsController(ProductDB product , IHostingEnvironment hostingEnvironment)
        {
            this.Product = product;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Sql_injection()
        {
            return View(await Product.Product.ToArrayAsync());
        }

        [HttpPost]
        public IActionResult Sql_injection(string Name, string Password)
        {

            
            var sqlResult = Product.Product.FromSqlRaw("select * from Product where Password='" + Password + "'and Name='" + Name + "'").ToList();
            
            List<Product> Member = new List<Product>();

            for (int i = 0; i < sqlResult.Count; i++)
            {
                Member.Add(sqlResult[i]);
            }

            ViewBag.List = Member;
            return View();
        }
        [HttpGet]
        public IActionResult Download()
        {
            //return Content($"WebRootPath = {_hostingEnvironment.WebRootPath}\n" +
            //              $"ContentRootPath = {_hostingEnvironment.ContentRootPath}");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Download(string fileName)
        {
            var dirPath = _hostingEnvironment.ContentRootPath + "/wwwroot/DownloadFolder/";
            //組路徑字串
            string filePath = dirPath+fileName;
            var memoryStream = new MemoryStream();
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memoryStream);
                }
                memoryStream.Seek(0, SeekOrigin.Begin);
                return File(memoryStream, _contentTypes[Path.GetExtension(filePath).ToLowerInvariant()], fileName);
            }
            catch(Exception ex)
            {
                return Content("檔案不存在");
            }

        }
}
}