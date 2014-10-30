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
            if (DateTime.TryParseExact(start, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dstart)
                && DateTime.TryParseExact(end, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dend))
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
            var meth = Request.UrlReferrer.AbsolutePath.ToLower();
            var book = db.Books.FirstOrDefault(b => b.Title == Title);
            if (meth.Contains("create"))
            {
                return Json(book == null ? true : false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchTitles(string searchTerm)
        {
            var result = db.Books.Where(b => b.Title.Contains(searchTerm) || b.Genre.Contains(searchTerm)).Include(b => b.Author).Select(a => new 
            {
                id = a.Id, 
                text = a.Title,
                price=a.Price,
                pub=a.pub.ToString(),
                date=a.ReleaseDate,
                em =a.staute.ToString(),
                genre = a.Genre,
                author = a.Author.Id
            });
            return Json(result, JsonRequestBehavior.AllowGet);
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
            book.pub =  em_bool.否;
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
                    foreach (var file in files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            if (!HttpPostedFileBaseExtensions.IsImage(file))
                            {
                                ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name", book.AuthorId);
                                return View(book);
                            }
                        }
                    }

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
        public async Task<ActionResult> Edit([Bind(Include="Id,Title,ReleaseDate,Genre,Price,staute,pub,AuthorId,imgs")] Book book, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                if (files.Count() > 0)
                {
                    foreach (var file in files)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            if (!HttpPostedFileBaseExtensions.IsImage(file))
                            {
                                ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name", book.AuthorId);
                                return View(book);
                            }
                        }
                    }

                    var oldImgs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(book.imgs);
                    var fs = files.ToList();
                    for (int i = 0; i < fs.Count; i++)
                    {
                        if (fs[i] != null && fs[i].ContentLength > 0)
                        {
                            if (oldImgs.Count > i)
                            {
                                var delpath = Path.Combine(Server.MapPath("~/uploads"), oldImgs[i]);
                                System.IO.File.Delete(delpath);
                                var ext = Path.GetExtension(fs[i].FileName);
                                var fileName = Guid.NewGuid().ToString("n") + ext;
                                var path = Path.Combine(Server.MapPath("~/uploads"), fileName);
                                fs[i].SaveAs(path);
                                oldImgs[i] = fileName;
                            }
                            else
                            {
                                var ext = Path.GetExtension(fs[i].FileName);
                                var fileName = Guid.NewGuid().ToString("n") + ext;
                                var path = Path.Combine(Server.MapPath("~/uploads"), fileName);
                                fs[i].SaveAs(path);
                                oldImgs.Add(fileName);
                            }
                        }
                    }
                    book.imgs = Newtonsoft.Json.JsonConvert.SerializeObject(oldImgs);
                }

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
