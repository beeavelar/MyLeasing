using MyLeasing.Web.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        private Random _random; //Criar orwers aleatorios

        public SeedDb(DataContext context) // Contrutor busca o o dataContext e o random
        {
            _context = context;
            _random = new Random(); //Instaciar
        }

        //Método publico asincrono que retorna uma Task 
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //Cria a base de dados, se nao estiver criada, ele cria, se já tiver, segue o baile

            if(!_context.Owners.Any()) //Se nao existir Owners na Bd --> criar os owners
            {
                AddOwner("Miguel");
                AddOwner("Pedro");
                AddOwner("Karen");
                AddOwner("Maria");
                AddOwner("Thiago");
                AddOwner("Heitor");
                AddOwner("Rodrigue");
                AddOwner("Gabriel");
                AddOwner("Renata");
                AddOwner("Amélie");
                await _context.SaveChangesAsync();
            }
        }

        private void AddOwner(string name)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(10000),
                FirstName = name,
            });
        }
    }
}
