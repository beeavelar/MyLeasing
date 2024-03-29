﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;

        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        //Actions:

        //Login --> Esse só serve para mostrar a página do login
        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated) //Se autenticar, ir para a view index do home
                return RedirectToAction("Index", "Home");
            
            return View(); //Se nao autenticas, fica na propria view
        }

        [HttpPost] //Dizer que esse método vai ser um post
        public async Task<IActionResult> Login(LoginViewModel model) //Recebe um LoginViewModel e faz o login (entra no sistema)
        {
            if(ModelState.IsValid) //Se o modelo for válido
            {
                var result = await _userHelper.LoginAsync(model); //Vai tentar logar
                
                if(result.Succeeded) //se conseguiu fazer login e entrou através de uma URL de retorno
                {
                    if(this.Request.Query.Keys.Contains("ReturnUrl")) //Se na query aparecer um return url
                    {
                        return Redirect(this.Request.Query["ReturnUrl"].First()); //Redireciona para a url de retorno
                    }
                    return this.RedirectToAction("Index", "Home"); //Caso contrário redirecionar para a página principal
                }
            }
            //Se não conseguiu logar, recebe mensagem que o login falhou
            this.ModelState.AddModelError(string.Empty, "Failed to login!");
            return View(model); //Fica na mesma página e não limpa os campos do formulario
        }

        //Logout
        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username); //Verificar se o user já existe ou não
                if(user == null) //Se nao existe
                {
                    user = new User //Criar o novo objeto
                    {
                        Document = model.Document,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username
                    };

                    //Adicionar o user
                    var result = await _userHelper.AddUserAsync(user, model.Password);

                    //Se o user nao for criado
                    if(result != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "The user couldn´t be created."); //mostrar mensagem de erro
                        return View(model); //Manter as caixas de texto com dados
                    }

                    //Se conseguir adicionar o novo user
                    var loginViewModel = new LoginViewModel //Controi o loginviewmodel
                    {
                        Password = model.Password,
                        RemenberMe = false,
                        Username = model.Username
                    };

                    //tenta logar
                    var result2 = await _userHelper.LoginAsync(loginViewModel);

                    //Se conseguir logar, vai para a página index do home
                    if(result2.Succeeded)
                        return RedirectToAction("Index", "Home");

                    //Se nao conseguir logar, mostra mensagem de erro
                    ModelState.AddModelError(string.Empty, "The user couldn´t be logged."); //mostrar mensagem de erro
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ChangeUser()
        {
            //Para mdoficar o user, a primeira coisa é buscar o email dele
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name); //Buscar o user (no caso é pelo email)
            var model = new ChangeUserViewModel(); //Instanciar o modelo do ChangeUserViewModel

            if(user != null) //Verificar se o email do user existe
            {
                //Se o user for diferente de nulo
                model.FirstName = user.FirstName; //Dizer que o firstname do modelo vai ser igual ao firstname do user
                model.LastName = user.LastName;
            }

            return View(model); //Fazer return da View (model)
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model) //Recebe o model
        {
            if(ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name); //Buscar o user (no caso é pelo email)
                
                if (user != null) //Verificar se o email do user existe
                {
                    //Se o user for diferente de nulo
                    user.FirstName = model.FirstName; //Dizer que o firstname do user vai ser igual ao firstname do modelo
                    user.LastName = model.LastName;

                    var response = await _userHelper.UpdateUserAsync(user); //Manda fazer o update

                    if (response.Succeeded) //Se conseguir fazer o update, manda mensagem confirmando
                        ViewBag.UserMessage = "User updated!";

                    else //Se não conseguir, manda mensagem de erro
                        ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                }
            }
            return View(model); //Fazer return da View (model)
        }

        public IActionResult ChangePassword() //Esse método só retorna a View, ou seja, só mostra o formulário para mudar a Password
        {
            return View(); //Retorna apenas a View, sem nada preenchido pq se trata de dados sensíveis (password)
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model) //Passa o mdoelo
        {
            if(ModelState.IsValid) //Se o modelo for valido
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name); //Buscar o user (no caso é pelo email)

                if (user != null) //Se o user existe
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword); 

                    if (result.Succeeded)
                        return this.RedirectToAction("ChangeUser");
                    else
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);    
                }
            }
            return View(model);
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }
    }
}
