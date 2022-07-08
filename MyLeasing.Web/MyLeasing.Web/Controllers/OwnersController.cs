using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;

namespace MyLeasing.Web.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnersRepository _ownerRepository;
        private readonly IUserHelper _userHelper;

        //private readonly DataContext _context;

        //public OwnersController(DataContext context)
        //{
        //    _context = context;
        //}

        public OwnersController(IOwnersRepository ownerRepository, IUserHelper userHelper)
        {
            _ownerRepository = ownerRepository;
            _userHelper = userHelper;
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
        public async Task<IActionResult> Create([Bind("Id,Document,FirstName,LastName,FixedPhone,CellPhone,Addrress")] Owner owner)
        {
            if (ModelState.IsValid)
            {
                // To do: Mofidicar para o user que estiver logado
                owner.User = await _userHelper.GetUserByEmailAsync("debora.avelar.21695@formandos.cinel.pt");
                //_context.Add(owner);
                //await _context.SaveChangesAsync();
                await _ownerRepository.CreateAsync(owner); //Adiciona o owner
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }

        // GET: Owners/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        public async Task<IActionResult> Edit(int? id)
        {
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
            return View(owner);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Document,FirstName,LastName,FixedPhone,CellPhone,Addrress")] Owner owner)
        {
            if (id != owner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    owner.User = await _userHelper.GetUserByEmailAsync("debora.avelar.21695@formandos.cinel.pt");
                    //_context.Update(owner);
                    //await _context.SaveChangesAsync();
                    await _ownerRepository.UpdateAsync(owner); //Faz o update dos dados do owner
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!OwnerExists(owner.Id))
                    if (! await _ownerRepository.ExistAsync(owner.Id)) //verifica se o owner existe, mas com o método do repositorio
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
            return View(owner);
        }

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
