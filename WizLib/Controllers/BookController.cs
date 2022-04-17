using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;
using WizLib_Model.ViewModels;

namespace WizLib.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _db;

        public BookController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            /*
            //EXPLICIT LOADING
            List<Book> objList = _db.Books.ToList();
            foreach (var obj in objList)
            {
                //Least Effecient //due to many db calls
                //obj.Publisher = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == obj.Publisher_Id);

                //or Explicit Loading More Efficient // this reduces the calls to only distinct publishers
                _db.Entry(obj).Reference(u => u.Publisher).Load();// 1 call for books and call for each distict publisher. n+1 query executions
                
                //to do all in one query execution, use eger loading using Include() method

                _db.Entry(obj).Reference(u => u.BookAuthors).Load();
                foreach (var bookAuth in obj.BookAuthors)
                {
                    _db.Entry(bookAuth).Reference(u => u.Author).Load(); //n+1 queries problem
                }
            }
           */

            //EAGER LOADING (single query execution using joins)
            List<Book> objList = _db.Books.Include(u => u.Publisher)
                                    .Include(u => u.BookAuthors).ThenInclude(u => u.Author).ToList();
            //ThenInclude > specifies prop is inside bookAuthors not inside books

            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            BookVM obj = new BookVM();
            obj.PublisherList = _db.Publishers.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Publisher_Id.ToString()
            });
            if (id == null)
            {
                return View(obj);
            }
            //this for edit
            obj.Book = _db.Books.FirstOrDefault(u => u.Book_Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM obj)
        {
            if (obj.Book.Book_Id == 0)
            {
                //this is create
                _db.Books.Add(obj.Book);
            }
            else
            {
                //this is an update
                _db.Books.Update(obj.Book);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var objFromDb = _db.Books.FirstOrDefault(u => u.Book_Id == id);
            _db.Books.Remove(objFromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Details button
        public IActionResult Details(int? id)
        {
            BookVM obj = new BookVM();

            if (id == null)
            {
                return View(obj);
            }
            //this for edit
            obj.Book = _db.Books.Include(u => u.BookDetail).FirstOrDefault(u => u.Book_Id == id);

            //less efficient
            //obj.Book = _db.Books.FirstOrDefault(u => u.Book_Id == id);
            //obj.Book.BookDetail = _db.BookDetails.FirstOrDefault(u => u.BookDetail_Id == obj.Book.BookDetail_Id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookVM obj)
        {
            if (obj.Book.BookDetail.BookDetail_Id == 0)
            {
                //this is create
                _db.BookDetails.Add(obj.Book.BookDetail);
                _db.SaveChanges();

                //if you add book detail there should be a corresponding book to it. so update that also
                var BookFromDb = _db.Books.FirstOrDefault(u => u.Book_Id == obj.Book.Book_Id);
                BookFromDb.BookDetail_Id = obj.Book.BookDetail.BookDetail_Id;
                _db.SaveChanges();
            }
            else
            {
                //this is an update
                _db.BookDetails.Update(obj.Book.BookDetail);
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ManageAuthors(int id)
        {
            //BOOKS:AUTHORS  >>many :many reltion
            BookAuthorVM obj = new BookAuthorVM
            {
                //eager load
                BookAuthorList = _db.BookAuthors.Include(u => u.Author).Include(u => u.Book)
                                    .Where(u => u.Book_Id == id).ToList(),
                BookAuthor = new BookAuthor()
                {
                    Book_Id = id
                },

                //this book object is needed eventhough you include above because for the first time if you add new author or assigning author to book. that time bookAuthor list will be empty
                Book = _db.Books.FirstOrDefault(u => u.Book_Id == id)
            };
            List<int> tempListOfAssignedAuthors = obj.BookAuthorList.Select(u => u.Author_Id).ToList();
            //NOT IN Clause in LINQ
            //get all the authors whos id is not in tempListOfAssignedAuthors
            var tempList = _db.Authors.Where(u => !tempListOfAssignedAuthors.Contains(u.Author_Id)).ToList();

            obj.AuthorList = tempList.Select(i => new SelectListItem
            {
                Text = i.FullName,
                Value = i.Author_Id.ToString()
            });

            return View(obj);

        }

        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if (bookAuthorVM.BookAuthor.Book_Id != 0 && bookAuthorVM.BookAuthor.Author_Id != 0)
            {
                _db.BookAuthors.Add(bookAuthorVM.BookAuthor);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.Book_Id });
        }

        [HttpPost]
        public IActionResult RemoveAuthors(int authorId, BookAuthorVM bookAuthorVM)
        {
            int bookId = bookAuthorVM.Book.Book_Id;
            BookAuthor bookAuthor = _db.BookAuthors.FirstOrDefault(
                u => u.Author_Id == authorId && u.Book_Id == bookId);

            _db.BookAuthors.Remove(bookAuthor);
            _db.SaveChanges();
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });
        }



        public IActionResult PlayGround()
        {
            //-----------------Deferred execution----------------------------------------------------
            //var bookTemp = _db.Books.FirstOrDefault();  //QUERY executes here
            //bookTemp.Price = 100;

            //var bookCollection = _db.Books; //QUERY not executes here
            //double totalPrice = 0;

            //foreach (var book in bookCollection) //QUERY executes here
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = _db.Books.ToList();  //QUERY executes here
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _db.Books;  //QUERY not executes here
            //var bookCount1 = bookCollection2.Count();  //QUERY executes here

            //var bookCount2 = _db.Books.Count();  //QUERY executes here

            //-------------------------IEnumerable vs IQueryable------------------------------------------------
            //client side filteration
            IEnumerable<Book> BookList1 = _db.Books;
            var FilteredBook1 = BookList1.Where(b => b.Price > 500).ToList();// this gets all books from db and filters here

            //db side filteration (efficicient)
            IQueryable<Book> BookList2 = _db.Books;
            var fileredBook2 = BookList2.Where(b => b.Price > 500).ToList(); // this gets filtered books from db
                                                                             //----------------------------------------------------------------------------



            //------------------------------Attach() vs Update()-----------------------------------s
            //Updating Related Data

            //Update() >> updates NumberOfChapters in bookDetail table and updates all columns in book table
            //var bookTemp1 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.Book_Id == 4);
            //bookTemp1.BookDetail.NumberOfChapters = 2222;
            //bookTemp1.Price = 12345;
            //_db.Books.Update(bookTemp1);
            //_db.SaveChanges();

            //Attach() >>updates Weight in bookDetail table only >> efficient
            //var bookTemp2 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.Book_Id == 4);
            //bookTemp2.BookDetail.Weight = 3333;
            //bookTemp2.Price = 123456;
            //_db.Books.Attach(bookTemp2);
            //_db.SaveChanges();

            //-----Manually set entity state to modified---------------
            //var category = _db.Categories.FirstOrDefault();
            //_db.Entry(category).State = EntityState.Modified;

            //_db.SaveChanges(); //this queries update eventhough values not changed but marked as modified


            //VIEWS
            //var viewList = _db.BookDetailsFromViews.ToList();
            //var viewList1 = _db.BookDetailsFromViews.FirstOrDefault();
            //var viewList2 = _db.BookDetailsFromViews.Where(u => u.Price > 500);

            //RAW SQL

            var bookRaw = _db.Books.FromSqlRaw("Select * from dbo.books").ToList();

            //SQL Injection attack prone
            int id = 2;
            var bookTemp1 = _db.Books.FromSqlInterpolated($"Select * from dbo.books where Book_Id={id}").ToList();

            var booksSproc = _db.Books.FromSqlInterpolated($" EXEC dbo.getAllBookDetails {id}").ToList();

            //.NET 5 only
            var BookFilter1 = _db.Books.Include(e => e.BookAuthors.Where(p => p.Author_Id == 5)).ToList();
            var BookFilter2 = _db.Books.Include(e => e.BookAuthors.OrderByDescending(p => p.Author_Id).Take(2)).ToList();


            return RedirectToAction(nameof(Index));
        }


    }
}
