using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eventos_Data.UoW.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
        void Dispose();
    }
}
