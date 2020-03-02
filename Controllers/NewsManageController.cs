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
using Microsoft.EntityFrameworkCore;
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
        #region Admin region
        public IActionResult AddNews()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveNews(NewsModel model)
        {

            List<Image> ls = new List<Image>();
            string imgName = Guid.NewGuid() + Path.GetExtension(model.MainImage.FileName);

            string path = Path.Combine("Images", "News", imgName);
            using (var fileStream = new FileStream( Path.Combine(_environment.WebRootPath,path), FileMode.Create))
            {
                await model.MainImage.CopyToAsync(fileStream);
            }
            ls.Add(new Image { Name = imgName, Puth = path, IsMain = true });

            var news = new News
            {
                NewsGuid = Guid.NewGuid(),
                Images = ls,
                Title = model.Title,
                Content = model.Content,
                Description = model.Description
            };
            db.News.Add(news);
            await db.SaveChangesAsync();

            return RedirectToAction("NewsDetails", new { guid = news.NewsGuid.ToString()});
        }
        #endregion
        #region User region
        public async Task<IActionResult> NewsDetails(string guid)
        {
            var news = await db.News.Include(x=>x.Images).FirstOrDefaultAsync(x => x.NewsGuid == Guid.Parse(guid));
            var viewModel = new NewsModel
            {
                Title = news.Title,
                Description = news.Description,
                MainImagePath = news.Images.Where(x => x.IsMain == true).FirstOrDefault().Puth,
                Content = news.Content
            };
            return View(viewModel);
        }
        #endregion
    }
}