using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DYMobileFirst.Models;
using System.IO;
using System.Globalization;

namespace DYMobileFirst.Controllers
{
    //[Authorize]
    public class BookController : Controller
    {
        private BookDBContext db = new BookDBContext();
     
        // GET: /Book/
        public async Task<ActionResult> Index(string authors, string searchString, string start, string end, int? pi, int? ps)
        {
            var authorLst = db.Authors.Select(a => a.Name).ToList().Distinct();
            ViewData["authors"] = new SelectList(authorLst);

            var books = db.Books.OrderBy(b=>b.Id).Include(b => b.Author);

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString) || s.Genre.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(authors))
            {
                books = books.Where(x => x.Author.Name == authors);
            }

            DateTime dstart;
            DateTime dend;
            if (DateTime.TryParseExact(start, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dstart)
                && DateTime.TryParseExact(end, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dend))
            {
                if (dstart < dend) 
                { 
                    books = books.Where(x => x.ReleaseDate >= dstart && x.ReleaseDate <= dend);
                }
            }

            if (!pi.HasValue || !ps.HasValue)
            {
                return View(await books.ToListAsync());
            }

            ViewBag.ps = ps.Value;
            ViewBag.pi = pi.Value;
            ViewBag.tp = (books.Count() + ps - 1) / ps;
            int startRow = (pi.Value - 1) * ps.Value;
            return View(await books.Skip(startRow).Take(ps.Value).ToListAsync());
        }

        [HttpGet]
        public JsonResult CheckTitleExists(string Title)
        {
            return Json(!db.Books.Any(b => b.Title == Title), JsonRequestBehavior.AllowGet);
        }  

        [HttpPost]
        public JsonResult GetCustomersNames()
        {
            var allItems = db.Authors.Select(c => c.Name);
            return Json(new { Items = allItems });
        }

        // GET: /Book/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            db.Entry(book).Reference(x => x.Author).Load();
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: /Book/Create
        public ActionResult Create()
        {
            Book book = new Book();
            book.Title = "书名";
            book.Price = 1;
            book.pub = true;
            book.ReleaseDate = DateTime.Now;
            book.staute = em_staute.普通;
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name");
            return View(book);
        }

        // POST: /Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,ReleaseDate,Genre,Price,staute,pub,AuthorId")] Book book, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                if (files.Count() > 0)
                {
                    List<string> imgs = new List<string>();
                    foreach (var file in files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var ext = Path.GetExtension(file.FileName);
                            var fileName = Guid.NewGuid().ToString("n") + ext;
                            var path = Path.Combine(Server.MapPath("~/uploads"), fileName);
                            file.SaveAs(path);
                            imgs.Add(fileName);
                        }
                    }
                    book.imgs = Newtonsoft.Json.JsonConvert.SerializeObject(imgs);
                }

                db.Books.Add(book);
                await db.SaveChangesAsync();

                return RedirectToAction("Index", new { pi = 1, ps = 15 });
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name", book.AuthorId);
            return View(book);
        }

        // GET: /Book/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            db.Entry(book).Reference(x => x.Author).Load();
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name", book.AuthorId);
            return View(book);
        }

        // POST: /Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Title,ReleaseDate,Genre,Price,staute,pub,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { pi = 1, ps = 15 });
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name", book.AuthorId);
            return View(book);
        }

        // GET: /Book/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await db.Books.FindAsync(id);
            db.Entry(book).Reference(x => x.Author).Load();
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: /Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await db.Books.FindAsync(id);

            if (!string.IsNullOrEmpty(book.imgs))
            {
                var imgs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(book.imgs);
                foreach (var item in imgs)
                {
                    var path = Path.Combine(Server.MapPath("~/uploads"), item);
                    System.IO.File.Delete(path);
                }
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
            catch { }
        }
    }

}
