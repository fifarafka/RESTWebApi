using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.ObjectManager.Interfaces;
using Lib.ObjectManager.Model;
using Lib.LiteDB.Model;
using LiteDB;

namespace Lib.LiteDB
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly string _authorsConnection = DatabaseConnections.AuthorConnection;

        public List<Author> GetAll()
        {
            using (var db = new LiteDatabase(this._authorsConnection))
            {
                var repository = db.GetCollection<AuthorDB>("authors");
                var results = repository.FindAll();

                return results.Select(x => Map(x)).ToList();
            }
        }

        public int Add(Author author)
        {
            using (var db = new LiteDatabase(this._authorsConnection))
            {
                var dbObject = InverseMap(author);


                var repository = db.GetCollection<AuthorDB>("authors");
                repository.Insert(dbObject);

                return dbObject.Id;
            }
        }

        public Author Get(int id)
        {
            using (var db = new LiteDatabase(this._authorsConnection))
            {
                var repository = db.GetCollection<AuthorDB>("authors");
                var result = repository.FindById(id);
                return Map(result);
            }
        }

        public Author Update(Author author)
        {
            using (var db = new LiteDatabase(this._authorsConnection))
            {
                var dbObject = InverseMap(author);

                var repository = db.GetCollection<AuthorDB>("authors");
                if (repository.Update(dbObject))
                    return Map(dbObject);
                else
                    return null;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(this._authorsConnection))
            {
                var repository = db.GetCollection<AuthorDB>("authors");
                return repository.Delete(id);
            }
        }

        private Author Map(AuthorDB dbAuthor)
        {
            if (dbAuthor == null)
                return null;

            return new Author() { Id = dbAuthor.Id, AuthorName = dbAuthor.AuthorName, AuthorSurname = dbAuthor.AuthorSurname };
        }

        private AuthorDB InverseMap(Author author)
        {
            if (author == null)
                return null;
            return new AuthorDB()
            {
                Id = author.Id,
                AuthorName = author.AuthorName,
                AuthorSurname = author.AuthorSurname
            };
        }
    }
}
