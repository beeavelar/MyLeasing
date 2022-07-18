using System.Linq;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
    public interface IOwnersRepository : IGenericRepository<Owner> //Herda do IGenericRepository e dizer que nesse caso o IGenericRepository vai ser um Owner
    {
        public IQueryable GetAllWithUsers();
    }

    //Basta construir isso nesse interface pq ele vai utilizar tudo do IGenericRepository
}
