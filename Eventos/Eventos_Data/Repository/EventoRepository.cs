using Evento_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos_Data.Repository
{
    public class EventoRepository : Repository<Evento>
    {
        public EventoRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
