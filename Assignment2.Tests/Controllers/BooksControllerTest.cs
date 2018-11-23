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

        //Index test
        [TestMethod]
        public void IndexReturnsView()
        {
            // act
            ViewResult result = controller.Index() as ViewResult;

            // assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void IndexReturnsBooks()
        {
            // act - does the viewresults Model equal a list of albums?
            var actual = (List<Book>)((ViewResult)controller.Index()).Model;

            // assert
            CollectionAssert.AreEqual(books.OrderBy(b => b.Author).ThenBy(b => b.BookTitle).ToList(), actual);
        }

        //Details tests
        #region
        [TestMethod]
        public void DetailsNoId()
        {
            // act
            var result = (ViewResult)controller.Details(null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            // act
            var result = (ViewResult)controller.Details(67830);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidId()
        {
            // act - cast the model as an Album object
            Book actual = (Book)((ViewResult)controller.Details(200)).Model;

            // assert - is this the first mock album in our array?
            Assert.AreEqual(books[1], actual);
        }

        [TestMethod]
        public void DetailsViewLoads()
        {
            // act
            ViewResult result = (ViewResult)controller.Details(200);

            // assert
            Assert.AreEqual("Details", result.ViewName);
        }
        #endregion

        //GET: Create test
        #region

        [TestMethod]
        public void CreateViewLoads()
        {
            // act
            var result = (ViewResult)controller.Create();

            // assert
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        //GET: Edit test
        #region
        [TestMethod]
        public void EditNoId()
        {
            // arrange
            int? id = null;

            // act 
            var result = (ViewResult)controller.Edit(id);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void EditInvalidId()
        {
            // act
            var result = (ViewResult)controller.Edit(8983);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void EditViewLoads()
        {
            // act
            ViewResult actual = (ViewResult)controller.Edit(300);

            // assert
            Assert.AreEqual("Edit", actual.ViewName);
        }

        [TestMethod]
        public void EditLoadsBook()
        {
            // act
            Book actual = (Book)((ViewResult)controller.Edit(300)).Model;

            // assert
            Assert.AreEqual(books[2], actual);
        }
        #endregion

        //GET: Delete test
        #region

        [TestMethod]
        public void DeleteNoId()
        {
            // act
            var result = (ViewResult)controller.Delete(null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidId()
        {
            // act
            var result = (ViewResult)controller.Delete(5098);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Delete(100);

            // assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsBooks()
        {
            // act
            Book result = (Book)((ViewResult)controller.Delete(100)).Model;

            // assert
            Assert.AreEqual(books[0], result);
        }

        #endregion

        //POST: Create test
        #region
        [TestMethod]
        public void CreateValidBook()
        {
            // arrange
            Book newBook = new Book
            {
                BookId = 354,
                BookTitle = "Fifty",
                Author = "Ned",
                Genre = "Horror"
            };

            // act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Create(newBook);

            // assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateInvalidBook()
        {
            // arrange
            Book invalid = new Book();

            // act
            controller.ModelState.AddModelError("Cannot create", "create exception");
            ViewResult result = (ViewResult)controller.Create(invalid);

            // assert
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        //POST: Edit test
        #region

        [TestMethod]
        public void EditPostLoadsIndex()
        {
            // act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Edit(books[0]);

            // assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void EditPostInvalidLoadsView()
        {
            // arrange
            Book invalid = new Book { BookId = 35 };
            controller.ModelState.AddModelError("Error", "Won't Save");

            // act
            ViewResult result = (ViewResult)controller.Edit(invalid);

            // assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditPostInvalidLoadsAlbum()
        {
            // arrange
            Book invalid = new Book { BookId = 50 };
            controller.ModelState.AddModelError("Error", "Won't Save");

            // act
            Book result = (Book)((ViewResult)controller.Edit(invalid)).Model;

            // assert
            Assert.AreEqual(invalid, result);
        }

        #endregion

        //POST: Delete test
        #region
        [TestMethod]
        public void DeleteConfirmedNoId()
        {
            // act
            ViewResult result = (ViewResult)controller.DeleteConfirmed(null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidId()
        {
            // act
            ViewResult result = (ViewResult)controller.DeleteConfirmed(1234);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedValidId()
        {
            // act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(300);

            // assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        #endregion
    }
}
