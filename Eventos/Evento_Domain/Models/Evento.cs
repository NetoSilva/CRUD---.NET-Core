using Evento_Domain_Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Evento_Domain.Models
{
    public class Evento : Entity<Evento>
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }

        public Evento(string nome, string descricao, DateTime dataInicio, DateTime dataFim)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
        }

        #region Validacoes

        public override bool EhValido()
        {
            ValidarNome();
            ValidarDescricao();
            ValidarDataInicio();
            ValidarDataFim();

            validationResult = Validate(this);
            return validationResult.IsValid;
        }

        private void ValidarDataFim()
        {
            RuleFor(e => e.DataFim)
            .GreaterThan(DataInicio);
        }

        private void ValidarDataInicio()
        {
            RuleFor(e => e.DataInicio)
                .GreaterThan(DateTime.Now);
        }

        private void ValidarDescricao()
        {
            RuleFor(e => e.Descricao)
                  .NotNull()
                  .NotEmpty()
                  .Length(3, 50);
        }

        private void ValidarNome()
        {
            RuleFor(e => e.Nome)
                 .NotNull()
                 .NotEmpty()
                 .Length(3, 50);
        }

        #endregion
    }
}
