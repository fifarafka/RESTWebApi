using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.ObjectManager.Model;

namespace Lib.ObjectManager.Interfaces
{
    public interface IAuthorRepository
    {
        List<Author> GetAll();
        int Add(Author author);
        Author Get(int id);
        Author Update(Author author);
        bool Delete(int id);
    }
}
