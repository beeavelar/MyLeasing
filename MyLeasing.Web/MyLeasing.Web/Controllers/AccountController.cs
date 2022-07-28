using Microsoft.AspNetCore.Identity;
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
            this.ModelState.AddModelError(string.Empty, "Failed to login");
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

    }
}
