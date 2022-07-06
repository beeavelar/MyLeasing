using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IRepository _repository;

        //private readonly DataContext _context;

        //public OwnersController(DataContext context)
        //{
        //    _context = context;
        //}

        public OwnersController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Owners
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Owners.ToListAsync()); 
            //Tarefa -> Vai na propriedade Owners da DataContext (_context é a variável global criada para o DataContext)
            //e envia a lista de Owners para a view Index
            return View(_repository.GetOwners()); //Buscar o repositório o método para buscar todos os produtos
        }

        // GET: Owners/Details/5
        // public async Task<IActionResult> Details(int? id)
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var owner = await _context.Owners
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var owner = _repository.GetOwner(id.Value); //.Value para aceitar tbm valores nulos (int? id --> id é opcional,
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
                //_context.Add(owner);
                //await _context.SaveChangesAsync();
                _repository.AddOwner(owner); //Adiciona o owner
                await _repository.SaveAllAsync();//Mandar gravar --> await pq o método SaveAllAsync é async

                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }

        // GET: Owners/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var owner = await _context.Owners.FindAsync(id);
            var owner = _repository.GetOwner(id.Value);
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
                    //_context.Update(owner);
                    //await _context.SaveChangesAsync();
                    _repository.UpdateOwner(owner); //Faz o update dos dados do owner
                    await _repository.SaveAllAsync(); //Mandar gravar --> await pq o método SaveAllAsync é async
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!OwnerExists(owner.Id))
                    if (!_repository.OwnerExists(owner.Id)) //verifica se o owner existe, mas com o método do repositorio
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var owner = await _context.Owners
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var owner = _repository.GetOwner(id.Value);
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

            var owner = _repository.GetOwner(id); //não precisa do .Value pq o parametro id (int id) que é passado não e opcional nesse caso
            _repository.RemoveOwner(owner); //Remove o owner
            await _repository.SaveAllAsync(); //Mandar gravar --> await pq o método SaveAllAsync é async
            return RedirectToAction(nameof(Index));
        }

        //private bool OwnerExists(int id)
        //{
        //    return _context.Owners.Any(e => e.Id == id);
        //}
    }
}
