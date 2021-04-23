using SolucionIoT.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace SolucionIoT.COMMON.Validadores
{
   public class AccionValidator: GenericValidator<Accion>
    {
        public AccionValidator()
        {
            RuleFor(a => a.Actuador).NotEmpty().NotNull();
            RuleFor(a => a.IdDispositivo).NotEmpty().NotNull();
            RuleFor(a => a.Estado).NotEmpty().NotNull();
        }
    }
}
