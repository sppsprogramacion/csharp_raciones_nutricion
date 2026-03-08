using CapaPresentacion.Validaciones.Usuarios.Datos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Validaciones.Usuarios.ValidacionesUsuarios
{
    public class UsuarioEditarValidator : AbstractValidator<UsuarioDatos>
    {
        public UsuarioEditarValidator()
        {

            RuleFor(x => x.txtApellido)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El detalle es obligatorio.")
                .Length(1, 100).WithMessage("El detalle debe tener maximo 100 caracteres.");
            RuleFor(x => x.txtNombre)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("El detalle es obligatorio.")
                .Length(1, 100).WithMessage("El detalle debe tener maximo 100 caracteres.");
            RuleFor(x => x.txtLegajo)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe ingresar un valor para legajo.")
                .Must(BeAnInteger).WithMessage("El legajo debe ser un numero entero.");
            RuleFor(x => x.txtDNI)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe ingresar un valor para dni.")
                .Must(BeAnInteger).WithMessage("El dni debe ser un numero entero.");
            RuleFor(x => x.cmbRol)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Debe ingresar un valor para rol.")
                .NotEmpty().WithMessage("Debe ingresar un valor para rol.")
                .Length(1, 20).WithMessage("El identificador de rol debe tener entre 1 y 20 caracteres.");

        }

        private bool BeAnInteger(string numero)
        {
            int numerox;
            try
            {
                numerox = int.Parse(numero);
            }
            catch
            {
                return false;
            }

            return numerox % 1 == 0;
        }
    }
}
