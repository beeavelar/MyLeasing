using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
    public class OwnerRepository : GenericRepository<Owner>, IOwnersRepository //Herda do GenericRepository e utiliza o interface IOwnersRepository
    {
        public OwnerRepository(DataContext context) : base(context) //Passa para o pai (herança) que é o GenericRepository
        {
        }
    }
}
