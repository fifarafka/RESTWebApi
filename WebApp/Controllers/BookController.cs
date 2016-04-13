using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lib.LiteDB;
using Lib.ObjectManager.Model;

namespace WebApp.Controllers
{
    public class BookController : ApiController
    {

        BookRepository bookRep { get; set; }

        public BookController()
        {
            bookRep = new BookRepository();
        }

        public IEnumerable<Book> Get()
        {
            return bookRep.GetAll();
        }

        public Book Get(int id)
        {
            return bookRep.Get(id);
        }

        public IEnumerable<Book> Get([FromUri] String search)
        {
            return bookRep.GetAll().Where(book => book.BookTitle.Contains(search));
        }

        public int Post([FromBody]Book book)
        {
            return bookRep.Add(book);
        }

        public void Put(int id, [FromBody]Book book)
        {
            book.Id = id;
            bookRep.Update(book);
        }

        public void Delete(int id)
        {
            bookRep.Delete(id);
        }
    }
}
