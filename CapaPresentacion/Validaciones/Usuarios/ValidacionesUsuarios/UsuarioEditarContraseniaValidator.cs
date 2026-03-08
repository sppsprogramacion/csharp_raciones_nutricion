using CapaPresentacion.Validaciones.Usuarios.Datos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Validaciones.Usuarios.ValidacionesUsuarios
{
    public class UsuarioEditarContraseniaValidator: AbstractValidator<UsuarioDatos>
    {
        public UsuarioEditarContraseniaValidator()
        {

            RuleFor(x => x.txtPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .Length(1, 50).WithMessage("La contraseña debe tener maximo 50 caracteres.");
        }

    }
}
