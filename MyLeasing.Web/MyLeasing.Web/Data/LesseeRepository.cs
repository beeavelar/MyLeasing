using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;
using System.Linq;

namespace MyLeasing.Web.Data
{
    public class LesseeRepository : GenericRepository<Lessee>, ILesseesRepository //Herda do GenericRepository e utiliza o interface ILesseesRepository
    {
        private readonly DataContext _context;

        public LesseeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Owners.Include(o => o.User);
            //É como fazer um INNER JOIN --> Da os owners com inner join com os user

        }
    }
}
