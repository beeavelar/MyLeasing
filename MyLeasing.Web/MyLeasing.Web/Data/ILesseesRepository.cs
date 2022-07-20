using MyLeasing.Web.Data.Entities;
using System.Linq;

namespace MyLeasing.Web.Data
{
    public interface ILesseesRepository : IGenericRepository<Lessee> //Herda do IGenericRepository e dizer que nesse caso o IGenericRepository vai ser um Lessee
    {
        public IQueryable GetAllWithUsers();
    }
}
