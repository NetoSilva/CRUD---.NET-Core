using Evento_Domain_Core.Models;
using Eventos_Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos_Data.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity<T>
    {
        private DbContext DbContext;
        private DbSet<T> DbSet;
        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }
        public void Create(T entity)
        {
            if (entity.EhValido())
            {
                DbSet.Add(entity);
            }
        }

        public void Delete(Guid id)
        {
            var entity = DbSet.AsNoTracking().FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                DbSet.Remove(entity);
            }
        }

        public Task<T> Read(Guid id)
        {
            if (id != null)
            {
                return DbSet.FirstOrDefaultAsync(e => e.Id == id);
            }

            return null;
        }

        public Task<List<T>> ReadAll()
        {
            return DbSet.ToListAsync();
        }

        public void Update(T entity)
        {
            var Entity = DbSet.AsNoTracking().FirstOrDefault(e => e.Id == entity.Id);
            if (Entity != null && entity.EhValido())
            {
                Entity = entity;
                DbSet.Update(Entity);
            }
        }
    }
}
