using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Assignment1.Controllers;
using Assignment1.Models;
using Assignment2.Controllers;
using Assignment2.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assignment2.Tests.Controllers
{
    [TestClass]
    public class BooksControllerTest
    {
        Mock<IBookMock> mock;
        List<Book> books;
        BooksController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            // arrange mock data for all unit tests
            mock = new Mock<IBookMock>();

            books = new List<Book>
            {
                new Book { BookId = 100, BookTitle = "Red", Author = "Hans", Genre = "Comedy" },
                new Book { BookId = 200, BookTitle = "Blue", Author = "Jeb", Genre = "Action" },
                new Book { BookId = 300, BookTitle = "Purple", Author = "Richie", Genre = "Drama" },
            };

            // populate interface from mock data
            mock.Setup(m => m.Books).Returns(books.AsQueryable());

            controller = new BooksController(mock.Object);
        }

        [TestMethod]
        public void IndexReturnsView()
        {
            // act
            ViewResult result = controller.Index() as ViewResult;

            // assert
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
