using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using javora.Models;
using Microsoft.AspNetCore.Mvc;
using javora.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace javora.Controllers
{
    public class InfoPageController : Controller
    {
        private readonly JavoraContext db;
        public InfoPageController(JavoraContext db)
        {
            this.db = db;
        }
        #region Admin
        public async Task<IActionResult> ChangeRailwayInfo()
        {
            var items = await db.InfoDatas.Where(x => x.InfoType == InfoType.RAILWAY_SCHEDULE).ToListAsync();
            var item = items.LastOrDefault();
            if(item == null)
            {
                return View();
            }
            else
            {
                return View("ChangeRailwayInfo", item.HtmlData);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangeRailwayInfo(string htmlData)
        {
            var items = await db.InfoDatas.Where(x => x.InfoType == InfoType.RAILWAY_SCHEDULE).ToListAsync();
            var item = items.LastOrDefault();
            if (item != null)
            {
                item.ChangeData = DateTime.Now;
                item.HtmlData = htmlData;
                await db.SaveChangesAsync();
            }
            else
            {
                var newItem = new InfoData
                {
                    InfoType = InfoType.RAILWAY_SCHEDULE,
                    ChangeData = DateTime.Now,
                    HtmlData = htmlData
                };
                db.InfoDatas.Add(newItem);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("RailwayInfo");
        }
        #endregion
        #region User
        [Route("RailwayInfo")]
        public async Task<IActionResult> RailwayInfo()
        {
            var items = await db.InfoDatas.Where(x => x.InfoType == InfoType.RAILWAY_SCHEDULE).ToListAsync();
            var item = items.LastOrDefault();
            if (item == null)
            {
                return View("RailwayInfo", string.Empty);
            }
            else
            {
                return View("RailwayInfo",item.HtmlData);
            }
        }
        #endregion
    }
}