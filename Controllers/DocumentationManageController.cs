using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using javora.Models;
using javora.Models.Database;
using javora.Models.View;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace javora.Controllers
{
    public class DocumentationManageController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly JavoraContext db;
        public DocumentationManageController(
            IWebHostEnvironment environment,
            JavoraContext db)
        {
            _environment = environment;
            this.db = db; 
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Admin
        [Route("DocumentList")]
        public async Task<ActionResult> DocumentAdminList()
        {
            List<DocumentModel> docs = //new List<DocumentModel>();
            await db.Documents
            .Select(x => new DocumentModel
            {
                Name = x.Name,
                Path = x.Path,
                Extension = x.Extension
            }).ToListAsync();

            return View(docs);
        }
        [Route("AddDocument")]
        public IActionResult AddDocument()
        {
            return View(new DocumentModel());
        }
        [HttpPost]
        public async Task<IActionResult> SaveDocument(DocumentModel model)
        {
            //List<Image> ls = new List<Image>();
            string fileName = Guid.NewGuid() + Path.GetExtension(model.File.FileName);

            string path = Path.Combine("Files", "Documentation", fileName);
            using (var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, path), FileMode.Create))
            {
                await model.File.CopyToAsync(fileStream);
            }
            //ls.Add(new Image { Name = imgName, Puth = path, IsMain = true });

            var doc = new Document
            {
                CreateDate = DateTime.Now,
                Name = model.Name,
                Extension = Path.GetExtension(model.File.FileName),
                Guid = Guid.NewGuid(),
                Path = path,
                FileType = DocumentType.UNDEFINED
            };
            db.Documents.Add(doc);
            await db.SaveChangesAsync();

            return RedirectToAction("DocumentList");
        }
        #endregion
    }
}