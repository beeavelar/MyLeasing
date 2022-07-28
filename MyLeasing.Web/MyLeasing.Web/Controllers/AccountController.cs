using Microsoft.AspNetCore.Mvc;
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
    }
}
