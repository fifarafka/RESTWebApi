using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.ObjectManager.Interfaces;
using LiteDB;
using Lib.LiteDB.Model;
using Lib.ObjectManager.Model;

namespace Lib.LiteDB
{
    public class BookRepository : IBookRepository
    {
        private readonly string _booksConnection = DatabaseConnections.BookConnection;

        public List<Book> GetAll()
        {
            using (var db = new LiteDatabase(this._booksConnection))
            {
                var repository = db.GetCollection<BookDB>("Books");
                var results = repository.FindAll();

                return results.Select(x => Map(x)).ToList();
            }
        }

        public int Add(Book book)
        {
            using (var db = new LiteDatabase(this._booksConnection))
            {
                var dbObject = InverseMap(book);


                var repository = db.GetCollection<BookDB>("Books");
                repository.Insert(dbObject);

                return dbObject.Id;
            }
        }

        public Book Get(int id)
        {
            using (var db = new LiteDatabase(this._booksConnection))
            {
                var repository = db.GetCollection<BookDB>("Books");
                var result = repository.FindById(id);
                return Map(result);
            }
        }

        public Book Update(Book book)
        {
            using (var db = new LiteDatabase(this._booksConnection))
            {
                var dbObject = InverseMap(book);

                var repository = db.GetCollection<BookDB>("Books");
                if (repository.Update(dbObject))
                    return Map(dbObject);
                else
                    return null;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(this._booksConnection))
            {
                var repository = db.GetCollection<BookDB>("Books");
                return repository.Delete(id);
            }
        }

        private Book Map(BookDB dbBook)
        {
            if (dbBook == null)
                return null;

            return new Book() { Id = dbBook.Id, BookTitle = dbBook.BookTitle, ISBN = dbBook.ISBN };
        }

        private BookDB InverseMap(Book book)
        {
            if (book == null)
                return null;
            return new BookDB()
            {
                Id = book.Id,
                BookTitle = book.BookTitle,
                ISBN = book.ISBN
            };
        }
    }
}
