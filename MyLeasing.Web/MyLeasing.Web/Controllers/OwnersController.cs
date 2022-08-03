using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyLeasing.Web.Controllers
{
    //[Authorize] //Com o Authorize, só entram users logados
    public class OwnersController : Controller
    {
        private readonly IOwnersRepository _ownerRepository;
        private readonly IUserHelper _userHelper;
        //private readonly IImageHelper _imageHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConveterHelper _converterHelper;

        //private readonly DataContext _context;

        //public OwnersController(DataContext context)
        //{
        //    _context = context;
        //}

        public OwnersController(IOwnersRepository ownerRepository,
            IUserHelper userHelper, IBlobHelper blobHelper,
            IConveterHelper converterHelper)
        {
            _ownerRepository = ownerRepository;
            _userHelper = userHelper;
            //_imageHelper = imageHelper;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }

        // GET: Owners
        public IActionResult Index()
        {
            //return View(await _context.Owners.ToListAsync()); 
            //Tarefa -> Vai na propriedade Owners da DataContext (_context é a variável global criada para o DataContext)
            //e envia a lista de Owners para a view Index
            return View(_ownerRepository.GetAll().OrderBy(o => o.FirstName)); //Buscar o repositório o método para buscar todos os produtos
        }

        // GET: Owners/Details/5
        // public async Task<IActionResult> Details(int? id)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var owner = await _context.Owners
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var owner = await _ownerRepository.GetByIdAsync(id.Value); //.Value para aceitar tbm valores nulos (int? id --> id é opcional,
                                                                       //aceita valor nulo) e não dar erro
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        [Authorize (Roles = "Admin")] //[Authorize (Roles = "Admin, Owner")] --> Se quiser dar permissão de create mais roles
        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerViewModel model)
        {
            if (ModelState.IsValid) //validar se o modelo é valido
            {
                //Depois de validar se o modelo é valido --> Carregar a imagem antes de colocar o owner no repositorio
                //var path = string.Empty;

                Guid imageId = Guid.Empty;

                if (model.ImageFile != null && model.ImageFile.Length > 0) //Se uma imagem for carregada --> lenght > 0 e model.ImageFile não é nulo
                {

                    //path = await _imageHelper.UploadImageAsync(model.ImageFile, "owners"); //Mandar guardar na pasta "owners"
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "owners");
                }

                var owner = _converterHelper.ToOwner(model, imageId, true); //Como é um create o isNew é true

                // To do: Mofidicar para o user que estiver logado
                //owner.User = await _userHelper.GetUserByEmailAsync("debora.avelar.21695@formandos.cinel.pt");
                owner.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                await _ownerRepository.CreateAsync(owner); //Adiciona o owner
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //Método retorna um Owner

        //private Owner ToOwner(OwnerViewModel model, string path)
        //{
        //    //Fazer a comversão do OwnerViewModel em um Owner
        //    return new Owner
        //    {
        //        Id = model.Id,
        //        Document = model.Document,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        FixedPhone = model.FixedPhone,
        //        CellPhone = model.CellPhone,
        //        Addrress = model.Addrress,
        //        ImageUrl = path,
        //        User = model.User
        //    };
        //}

        [Authorize(Roles = "Admin")]
        // GET: Owners/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        public async Task<IActionResult> Edit(int? id) //Recebe o id do owner a ser editado
        {
            //Verifica se o owner existe
            if (id == null)
            {
                return NotFound();
            }

            //var owner = await _context.Owners.FindAsync(id);
            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            //No Edit é ao contrário, converte o Owner em OwnerViewModel
            //var model = this.ToOwnerViewModel(owner);
            var model = _converterHelper.ToOwnerViewModel(owner);

            return View(model);
        }

        //private OwnerViewModel ToOwnerViewModel(Owner owner)
        //{
        //    return new OwnerViewModel
        //    {
        //        Id = owner.Id,
        //        Document = owner.Document,
        //        FirstName = owner.FirstName,
        //        LastName = owner.LastName,
        //        FixedPhone = owner.FixedPhone,
        //        CellPhone = owner.CellPhone,
        //        Addrress = owner.Addrress,
        //        ImageUrl = owner.ImageUrl,
        //        User = owner.User
        //    };
        //}

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OwnerViewModel model)
        {
            //Não é preciso ver se o Id existe
            //if (id != owner.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    //var path = model.ImageUrl;
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "owners");
                    }

                    var owner = _converterHelper.ToOwner(model, imageId, false);

                    owner.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    //_context.Update(owner);
                    //await _context.SaveChangesAsync();
                    await _ownerRepository.UpdateAsync(owner); //Faz o update dos dados do owner
                }

                catch (DbUpdateConcurrencyException)
                {
                    //if (!OwnerExists(owner.Id))
                    if (!await _ownerRepository.ExistAsync(model.Id)) //verifica se o owner existe, mas com o método do repositorio
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // GET: Owners/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var owner = await _context.Owners
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var owner = await _context.Owners.FindAsync(id);
            //_context.Owners.Remove(owner);
            //await _context.SaveChangesAsync();

            var owner = await _ownerRepository.GetByIdAsync(id); //não precisa do .Value pq o parametro id (int id) que é passado não e opcional nesse caso
            await _ownerRepository.DeleteAsync(owner); //Remove o owner
            return RedirectToAction(nameof(Index));
        }

        //private bool OwnerExists(int id)
        //{
        //    return _context.Owners.Any(e => e.Id == id);
        //}
    }
}
