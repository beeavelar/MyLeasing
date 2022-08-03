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

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model); //Faz o login

        Task LogoutAsync(); //Faz o logout

        Task<IdentityResult> UpdateUserAsync (User user);

        Task<IdentityResult> ChangePasswordAsync (User user, string oldPassword, string newPassword);
        
        //Método que verifica se o user tem um determinado role
        Task CheckRoleAsync(string roleName);

        //Adicionao o role em um determinado user
        Task AddUserToRoleAsync(User user, string roleName);

        //Verifica se o user já tem esse role --> um user pode ter vários roles
        Task<bool> IsUserInRoleAsync(User user, string roleName);
    }
}
