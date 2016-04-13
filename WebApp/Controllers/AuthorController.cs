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
    public class AuthorController : ApiController
    {
        AuthorRepository authorRep { get; set; }

        public AuthorController()
        {
            authorRep = new AuthorRepository();
        }

        public IEnumerable<Author> Get()
        {
            return authorRep.GetAll();
        }

        public Author Get(int id)
        {
            return authorRep.Get(id);
        }

        public int Post([FromBody]Author author)
        {
            return authorRep.Add(author);
        }

        public void Put(int id, [FromBody]Author author)
        {
            author.Id = id;
            authorRep.Update(author);
        }

        public void Delete(int id)
        {
            authorRep.Delete(id);
        }
    }
}
