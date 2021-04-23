using FluentValidation;
using SolucionIoT.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.COMMON.Validadores
{
    public class UsuarioValidator: GenericValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(u => u.Correo).NotNull().EmailAddress();
            RuleFor(u => u.Password).NotEmpty().NotNull().Length(2, 50);
            RuleFor(u => u.Nombre).NotNull().NotEmpty().MinimumLength(10);

        }
    }
}
