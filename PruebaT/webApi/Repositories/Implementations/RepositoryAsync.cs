using Microsoft.EntityFrameworkCore;
using webApi.Models;
using webApi.Repositories.Interfaces;

namespace webApi.Repositories.Implementations
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class,new()
    {
        private readonly PruebaTecnicaContext _context;
        public RepositoryAsync(PruebaTecnicaContext context)
        {
            this._context = context;
        }
        protected DbSet<T> EntitySet
        {
            get
            {
                return _context.Set<T>();
            }
        }
        private bool disposed=false;
        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed &&  disposing)
            {
                _context.Dispose();
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }

        public async Task<List<T>> GetAll()
        {
            return await EntitySet.ToListAsync();
        }

        public async Task<T?> GetByID(int? id)
        {
            return await EntitySet.FindAsync(id);
        }

        public async Task<T> Insert(T entity)
        {
            EntitySet.Add(entity);
            await Save();
            return entity;
        }

        private async Task Save()
        {
            await (_context as DbContext).SaveChangesAsync();
        }

        public async Task<T> Delete(int id)
        {
            T? entity=await EntitySet.FindAsync(id);
            if (entity==null)
            {
                return new T();
            }
            EntitySet.Remove(entity);
            await Save();
            return entity;
        }

        public Task Update(T entity)
        {
            _context.Entry(entity).State=EntityState.Modified;
            return Save();
        }
    }
}
