using Eventos_Data.UoW.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eventos_Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext DbContext;
        public UnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<int> Commit()
        {
            return await DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
