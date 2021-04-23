using FluentValidation;
using SolucionIoT.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.COMMON.Validadores
{
    public abstract class GenericValidator<T>:AbstractValidator<T> where T:BaseDTO
    {
        public GenericValidator()
        {
            RuleFor(e => e.Id).NotEmpty().NotNull();
            RuleFor(e => e.FechaHora).NotEmpty().NotNull();
            
        }
    }
}
