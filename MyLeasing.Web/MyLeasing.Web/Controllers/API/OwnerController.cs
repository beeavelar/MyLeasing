using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data;

namespace MyLeasing.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnersRepository _ownerRepository;

        public OwnerController(IOwnersRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        [HttpGet]
        public IActionResult GetOwners()
        { 
            return Ok(_ownerRepository.GetAllWithUsers()); //Busca todos os owners no repositorio
        }


    }
}
