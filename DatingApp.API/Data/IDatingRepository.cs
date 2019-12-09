using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
        // Die zwei nachfolgenden Methoden sollen generische Parameter
        // entgegen nehmen können. Es wird nur bestimmt, dass diese Parameter von
        // einer Klasse abzuleiten sind.
         void Add<T>(T entity) where T: class ;
         void Delete<T>(T entity) where T: class ;

         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
    }
}