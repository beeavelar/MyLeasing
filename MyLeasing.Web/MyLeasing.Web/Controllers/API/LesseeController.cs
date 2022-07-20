using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data;

namespace MyLeasing.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LesseeController : Controller
    {
        private readonly ILesseesRepository _lesseeRepository;

        public LesseeController(ILesseesRepository lesseeRepository)
        {
            _lesseeRepository = lesseeRepository;
        }

        [HttpGet]
        public IActionResult GetLessees()
        {
            return Ok(_lesseeRepository.GetAllWithUsers()); //Busca todos os owners no repositorio
        }
    }
}
