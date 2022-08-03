using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        //private readonly UserManager<User> _userManager;

        public SeedDb(DataContext context, IUserHelper userHelper) // Contrutor busca o dataContext e o userHelper
        {
            _context = context;
            _userHelper = userHelper;
            //_userManager = userManager;
        }

        //Método publico asincrono que retorna uma Task 
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync(); //Cria a base de dados, se nao estiver criada, ele cria, se já tiver, segue o baile

            //Verifica se existe os roles Admin, Owner e Lessee
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Owner");
            await _userHelper.CheckRoleAsync("Lessee");

            var user = await _userHelper.GetUserByEmailAsync("debora.avelar.21695@formandos.cinel.pt"); //Verificar se já existe o o user debora.avelar.21695@formandos.cinel.pt
            if (user == null) //Se o user não existir, então criar o user
            {
                user = new User //Cria os dados do user
                {
                    Document = "1",
                    FirstName = "Débora",
                    LastName = "Avelar",
                    Email = "debora.avelar.21695@formandos.cinel.pt",
                    UserName = "debora.avelar.21695@formandos.cinel.pt",
                    PhoneNumber = "932829382"
                };

                var result = await _userHelper.AddUserAsync(user, "123456"); //Cria o user
                if (result != IdentityResult.Success) //Se o user não for criado corretamente, mostra um erro
                    throw new InvalidOperationException("Could not create the user in seeder.");

                //Adicionar o role ao user
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            //Verifica se o user tem o role que quero verificar --> No caso o "AdmiN"
            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");

            //Se o user não tem a permissão Admin, adiciona ele no Admin
            if(!isInRole)
                await _userHelper.AddUserToRoleAsync(user, "Admin");

            if (!_context.Owners.Any()) //Se nao existir Owners na Bd --> criar 
            {
                AddOwner("Miguel", user); //Como só vai ter um utilizador criado, ele é quem cria todos os Owners por enquanto
                AddOwner("Pedro", user);
                AddOwner("Karen", user);
                AddOwner("Maria", user);
                AddOwner("Thiago", user);
                AddOwner("Heitor", user);
                AddOwner("Rodrigue", user);
                AddOwner("Gabriel", user);
                AddOwner("Renata", user);
                AddOwner("Amélie", user);
                await _context.SaveChangesAsync();
            }

            if (!_context.Lessee.Any()) //Se nao existir Owners na Bd --> criar 
            {
                AddLessee("Miguel", "Guerreiro", user); //Como só vai ter um utilizador criado, ele é quem cria todos os Owners por enquanto,
                AddLessee("Pedro", "Rodrigues", user);
                AddLessee("Karen", "Borges", user);
                AddLessee("Maria", "Lopes", user);
                AddLessee("Thiago", "Abravael",  user);
                await _context.SaveChangesAsync();
            }

        }
        private void AddOwner(string name, User user)
        {
            _context.Owners.Add(new Owner
            {
                FirstName = name,
                User = user
            });
        }

        private void AddLessee(string firstName, string lastName, User user)
        {
            _context.Lessee.Add(new Lessee
            {
                FirstName = firstName,
                LastName = lastName,
                User = user
            });
        }
    }
}
