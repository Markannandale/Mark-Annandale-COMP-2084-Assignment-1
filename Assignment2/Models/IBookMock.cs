using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    public interface IBookMock
    {
        IQueryable<Book> Books { get; }
        Book Save(Book book);
        void Delete(Book book);
    }
}
