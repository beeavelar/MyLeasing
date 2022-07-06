using System;
using System.Linq;
using System.Threading.Tasks;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context) // Contrutor busca o o dataContext e o random
        {
            _context = context;
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
                FirstName = name,
            });
        }
    }
}
