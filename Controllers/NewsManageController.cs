using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using javora.Models;
using javora.Models.Database;
using javora.Models.View;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace javora.Controllers
{
    public class NewsManageController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly JavoraContext db;


        public NewsManageController(IWebHostEnvironment environment,
            JavoraContext db
            )
        {
            _environment = environment;
            this.db = db;
        }
        public IActionResult AddNews()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveNews(NewsModel model, int newsId)
        {


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoadPhoto(IFormFileCollection formData)
        {
            var guid = Guid.NewGuid();

            List<Image> ls = new List<Image>();
            List < ImageModel > viewList = new List<ImageModel>();
            foreach (var item in formData)
            {
                string path = "/Images/" + guid + "-" + item.FileName;
                using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                {
                    await item.CopyToAsync(fileStream);
                }
                ls.Add(new Image { Name =guid +"-"+ item.FileName, Puth = path });
                viewList.Add(new ImageModel { Name = item.FileName, Path = path });
            }
            db.News.Add(new News{ NewsGuid = guid, Images = ls});
            await db.SaveChangesAsync();
            return Ok(viewList);
        }
    }
}