using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
    public class OwnerRepository : GenericRepository<Owner>, IOwnersRepository //Herda do GenericRepository e utiliza o interface IOwnersRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context) : base(context) //Passa para o pai (herança) que é o GenericRepository
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers() //Trazer tudo com o respectivo user
        {
            return _context.Owners.Include(o => o.User); 
            //É como fazer um INNER JOIN --> Da os owners com inner join com os user
        }
    }
}
