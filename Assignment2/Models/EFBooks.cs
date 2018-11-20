using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Assignment1.Models;

namespace Assignment2.Models
{
    public class EFBooks : IBookMock
    {
        private MovieBookModel db = new MovieBookModel();
        public IQueryable<Book> Books { get { return db.Books; } }

        public void Delete(Book book)
        {
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book Save(Book book)
        {
            if (book.BookId == 0)
            {
                db.Books.Add(book);
            }
            else
            {
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
            return book;
        }
    }
}