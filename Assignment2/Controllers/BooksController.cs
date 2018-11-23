using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment1.Models;
using Assignment2.Models;

namespace Assignment1.Controllers
{
    public class BooksController : Controller
    {
        // Creates Database
        //private MovieBookModel db = new MovieBookModel();
        private IBookMock db;

        public BooksController()
        {
            // if nothing passed to constructor, connect to the db (this is the default)
            this.db = new EFBooks();
        }

        public BooksController(IBookMock bookMock)
        {
            // if we pass a mock object to the constructor, we are unit testing so no db
            this.db = bookMock;
        }

        // GET: Books
        public ActionResult Index()
        {
            //return View(db.Books.ToList());
            var books = db.Books.OrderBy(b => b.Author).ThenBy(b => b.BookTitle).ToList();
            return View("Index", books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //original
            //Book book = db.Books.Find(id);

            //new code for mock and database
            Book book = db.Books.SingleOrDefault(b => b.BookId == id);

            if (book == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Details", book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,BookTitle,Author,Genre")] Book book)
        {
            if (ModelState.IsValid)
            {
                //db.Books.Add(book);
                //db.SaveChanges();
                db.Save(book);
                return RedirectToAction("Index");
            }
            return View("Create", book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //original
            //Book book = db.Books.Find(id);

            //new code for mock and database
            Book book = db.Books.SingleOrDefault(b => b.BookId == id);

            if (book == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Edit", book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,BookTitle,Author,Genre")] Book book)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(book).State = EntityState.Modified;
                //db.SaveChanges();
                db.Save(book);
                return RedirectToAction("Index");
            }
            return View("Edit", book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Book book = db.Books.Find(id);
            Book book = db.Books.SingleOrDefault(b => b.BookId == id);
            if (book == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Delete", book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            //Book book = db.Books.Find(id);
            //db.Books.Remove(book);
            //db.SaveChanges();
            if (id == null)
            {
                return View("Error");
            }

            Book book = db.Books.SingleOrDefault(b => b.BookId == id);

            if (book == null)
            {
                return View("Error");
            }

            db.Delete(book);

            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
