using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public interface IGenericRepository<T> where T : class //T --> quer dizer que é alguma coisa generica --> Dizer que T é uma classe (<T> where T : class)
    {
        //FAZER O CRUD QUE DÊ PARA TODOS AO MESMO TEMPO (OWNERS,PRODUTOS, etc)

        IQueryable<T> GetAll(); //Método que devolve uma lista da entidade que estiver sendo utilizada, nesse caso devolve uma lista de Owners

        Task<T> GetByIdAsync(int id);

        Task CreateAsync(T entity); //Como parameto recece uma entidade T

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(int id);

    }
}
