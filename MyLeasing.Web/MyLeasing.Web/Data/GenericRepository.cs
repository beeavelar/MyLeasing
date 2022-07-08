using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data.Entities;

namespace MyLeasing.Web.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        //Buscar todos itens da entidade (do tipo IEntity) que foi requerida (no caso Owners)
        public IQueryable<T> GetAll() 
        {
            return _context.Set<T>().AsNoTracking();  //Vai na tabela e retorna os itens/valores que foram solicitados
            //AsNoTracking --> vai na tabela, busca o que tem que buscar e desliga a ligação com a tabela
            //Set<T> é uma tabela de Owner no caso
        }

        //Buscar por um Id específico da entidade
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id); //e = entidade --> Busca o id da entidade
        }

        //Criar = gravar os dados
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveAllAsync();
        }

        //Update
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await SaveAllAsync();
        }

        //Delete
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await SaveAllAsync();
        }

        //verificar se o item existe
        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Set<T>().AnyAsync(e => e.Id == id);
        }

        //Método salvar
        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() < 0;
            //SaveChangesAsync --> Grava tudo que está pendente "assincronicamente" --> Se for maior que 0 é pq gravou
            //pelo menos uma coisa (retorna true), se for 0 é pq não gravou nenhum coisa (retorn false)
        }
    }
}
