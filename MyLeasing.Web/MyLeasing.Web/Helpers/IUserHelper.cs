using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;

namespace MyLeasing.Web.Helpers
{
    //Gestão do utilizador
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserSync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model); //Faz o login

        Task LogoutAsync(); //Faz o logout
    }
}
