﻿
using LightMessanger.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LightMessanger.DAL.Repositories
{
    public class AbstractRepository<T> : IRepository<T> where T : class, IEntityWithId
    {
        protected ApplicationDbContext _context;
        public AbstractRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public virtual async Task AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T item)
        {
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async  Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(p => p.Id == id);
        }

        public virtual async Task UpdateAsync(T item)
        {
            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
        }

    }
}
