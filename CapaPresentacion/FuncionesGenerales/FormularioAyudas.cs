using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.FuncionesGenerales
{
    public static class FormularioAyudas
    {
        public static void AjustarFormulario(Form form)
        {
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;

            if (form.Height > screenHeight)
                form.Height = screenHeight;

            if (form.Width > screenWidth)
                form.Width = screenWidth;

            form.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
