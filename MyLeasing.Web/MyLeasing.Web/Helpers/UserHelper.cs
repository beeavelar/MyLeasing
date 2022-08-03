using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Models;
using System.Threading.Tasks;

namespace MyLeasing.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager; //responsavel pelo user
        private readonly SignInManager<User> _signInManager; //responsãbvel pelo sign in
        private readonly RoleManager<IdentityRole> _roleManager; //responsável pelo papel de cada user

        public UserHelper(UserManager<User> userManager, 
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //Criar um novo user
        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        //Método que adiciona o user no role
        public async Task AddUserToRoleAsync(User user, string roleName)
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }


        //Método ChangePassword
        public async Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        //Método para verificar o role
        public async Task CheckRoleAsync(string roleName)
        {
            //verifica se o role existe e guarda na variavel
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            //se não existe, temos que criar
            if(!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
        }

        //Método GetUserByEmail
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);

        }

        //Método que verifica se o user tem um determinado role ou não
        public async Task<bool> IsUserInRoleAsync(User user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        //Método do Sign in
        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RemenberMe, false);
        }

        //Método do Sign out
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        //Método UpdateUser
        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user); //Recebe o user e faz o update
        }
    }
}
