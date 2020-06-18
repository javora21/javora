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
        public IActionResult NewsAdminList()
        {
            var news = db.News.Include(x => x.Images).AsEnumerable();
            List<NewsModel> model = new List<NewsModel>();
            foreach (var item in news)
            {
                model.Add(new NewsModel
                {
                    Id = item.Id,
                    MainImagePath = item.Images.Where(x=>x.IsMain).FirstOrDefault().Puth,
                    Title = item.Title,
                    Description = item.Description,
                    Guid = item.NewsGuid

                });
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveNews(NewsModel model)
        {
            if(model.Guid == Guid.Empty)//add save
            {
                List<Image> ls = new List<Image>();
                string imgName = Guid.NewGuid() + Path.GetExtension(model.MainImage.FileName);

                string path = Path.Combine("Images", "News", imgName);
                using (var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, path), FileMode.Create))
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
                    Description = model.Description,
                    CreateTime = DateTime.Now
                };
                db.News.Add(news);

                await db.SaveChangesAsync();

                return RedirectToAction("NewsDetails", new { guid = news.NewsGuid.ToString() });
            }
            else//edit save
            {
                List<Image> ls = new List<Image>();
                if (model.MainImage != null)//main image change
                {
                    string imgName = Guid.NewGuid() + Path.GetExtension(model.MainImage.FileName);

                    string path = Path.Combine("Images", "News", imgName);
                    using (var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, path), FileMode.Create))
                    {
                        await model.MainImage.CopyToAsync(fileStream);
                    }
                    ls.Add(new Image { Name = imgName, Puth = path, IsMain = true });
                }
                var target = await db.News.Include(x => x.Images).Where(x => x.NewsGuid == model.Guid).FirstOrDefaultAsync();

                target.Title = model.Title;
                target.Description = model.Description;
                target.Content = model.Content;
                if (model.MainImage != null)//main image change
                {
                    target.Images = ls;
                }

                await db.SaveChangesAsync();

                return RedirectToAction("NewsDetails", new { guid = target.NewsGuid.ToString() });
            }
        }

        public async Task<IActionResult> DeleteNews(Guid guid)
        {
            
            var target = await db.News.Where(x => x.NewsGuid == guid).FirstOrDefaultAsync();
            db.News.Remove(target);
            await db.SaveChangesAsync();
            return RedirectToAction("NewsAdminList");
        }
        public async Task<IActionResult> EditNews(Guid guid)
        {
            var target = await db.News.Include(x=>x.Images).Where(x => x.NewsGuid == guid).FirstOrDefaultAsync();
            //db.News.Remove(target);
            //await db.SaveChangesAsync();
            NewsModel model = new NewsModel
            {
                Guid = target.NewsGuid,
                Description = target.Description,
                Title = target.Title,
                Content = target.Content,
                MainImagePath = target.Images.Where(x => x.IsMain).FirstOrDefault().Puth
            };
            return View(model);
        }
        #endregion
        #region User region
        [Route("NewsList")]
        public async Task<IActionResult> NewsList(int page = 1)
        {
            int pagesize = 3;
            var query = db.News;
            int totalItems = await query.CountAsync();

            var totalpages = (int)Math.Ceiling((double)totalItems / (double)pagesize);

            var list = await query.OrderByDescending(x => x.Id).
                Skip((page - 1) * pagesize).
                Take(pagesize)
                .Select(x => new NewsModel
            {
                Description = x.Description,
                CreateDate = x.CreateTime,
                Title = x.Title,
                Guid = x.NewsGuid,
                MainImagePath = x.Images.FirstOrDefault(x=>x.IsMain).Puth
            }).ToListAsync();
            var pagination = new PaginationModel
            {
                PageNumber = page,
                TotalPages = totalpages
            };
            return View(new NewsListModel { Pagination = pagination, News = list });
        }
        [Route("News/{guid}")]
        public async Task<IActionResult> NewsDetails(string guid)
        {
            var news = await db.News.Include(x=>x.Images).FirstOrDefaultAsync(x => x.NewsGuid == Guid.Parse(guid));
            var viewModel = new NewsModel
            {
                Title = news.Title,
                Description = news.Description,
                MainImagePath = news.Images.Where(x => x.IsMain == true).FirstOrDefault().Puth,
                Content = news.Content, 
                CreateDate = news.CreateTime
            };
            return View(viewModel);
        }
        #endregion
    }
}