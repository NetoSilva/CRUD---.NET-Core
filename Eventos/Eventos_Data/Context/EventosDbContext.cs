using Evento_Domain.Models;
using Evento_Domain_Core.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Eventos_Data.Context
{
    public class EventosDbContext: DbContext
    {
        public EventosDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("LocalDb"));

                base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FluentAPI
            #region Evento
            modelBuilder.Entity<Evento>().Ignore(e => e.CascadeMode);

            modelBuilder.Entity<Evento>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Evento>()
                .Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Evento>()
               .Property(e => e.Descricao)
               .IsRequired()
               .HasMaxLength(150);

            modelBuilder.Entity<Evento>()
                .Property(e => e.DataInicio)
                .HasColumnType("datetime");

            modelBuilder.Entity<Evento>()
            .Property(e => e.DataFim)
            .HasColumnType("datetime");
            #endregion

            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
