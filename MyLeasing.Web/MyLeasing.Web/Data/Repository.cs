using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        //CONSTRUÇÃO DO CRUD DOS DADOS

        //Buscar todos os owners
        public IEnumerable<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(o => o.FirstName); //Retorna a lista de todos Owners ordenada pelo FirstName
        }

        //Buscar um owner específico pelo id
        public Owner GetOwner(int id)
        {
            return _context.Owners.Find(id); //Retorna o owner do id que foi passado por parâmetro
        }

        //Adicionar o owner --> apenas em memória, ainda não vai pada a bd
        public void AddOwner(Owner owner) //Recebe por paramêtro o owner que será adicionado
        {
            _context.Owners.Add(owner); //Manda adicionar o owner
        }

        //Update
        public void UpdateOwner(Owner owner) //Recebe por paramêtro o owner que será feito o update
        {
            _context.Owners.Update(owner); //Manda fazer o update do owner
        }

        //Remove
        public void RemoveOwner(Owner owner) //Recebe por paramêtro o owner que será removido
        {
            _context.Owners.Remove(owner); //Manda remover o owner
        }

        //Método para gravar na Bd
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
            //SaveChangesAsync --> Grava tudo que está pendente "assincronicamente" --> Se for maior que 0 é pq gravou
            //pelo menos uma coisa (retorna true), se for 0 é pq não gravou nenhum coisa (retorn false)
        }

        //Verificar se o owner existe
        public bool OwnerExists(int id)
        {
            return _context.Owners.Any(o => o.Id == id); //Procra se existe um owner que tem o id igual ao id passado por parâmetor
        }
    }
}
