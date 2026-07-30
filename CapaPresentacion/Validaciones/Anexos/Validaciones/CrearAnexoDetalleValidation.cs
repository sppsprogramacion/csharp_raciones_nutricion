using CapaPresentacion.Validaciones.Anexos.Datos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapaPresentacion.Validaciones.Anexos.Validaciones
{
    public class CrearAnexoDetalleValidation : AbstractValidator<AnexoDetalleDatos>
    {
        public CrearAnexoDetalleValidation()
        {
            RuleFor(x => x.cmbMenus)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Debe ingresar un valor para MENU.")
                .NotEmpty().WithMessage("Debe ingresar un valor para MENU.")
                .Must(BeAnInteger).WithMessage("El MENU seleccionado debe ser valido.");
            RuleFor(x => x.txtDetalle)
                .Cascade(CascadeMode.Stop)
                .MaximumLength(200).WithMessage("DETALLE debe tener maximo 200 caracteres.");
            RuleFor(x => x.txtCantidad)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe ingresar un valor para CANTIDAD.")
                .Must(BeAnInteger).WithMessage("CANTIDAD debe ser un numero entero.");
            RuleFor(x => x.txtFactor)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe ingresar un valor para FACTOR.")
                .Must(BeADecimal).WithMessage("FACTOR debe ser un numero decimal con hasta 2 decimales.");
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

        private bool BeADecimal(string value)
        {
            // Reemplaza coma por punto por si el usuario escribe coma
            value = value.Replace('.', ',');

            // Validar formato: número entero o decimal con 1 o 2 decimales
            return Regex.IsMatch(value, @"^\d+(\,\d{1,2})?$");
        }
    }
}
