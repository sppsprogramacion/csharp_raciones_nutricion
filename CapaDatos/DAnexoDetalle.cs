using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("anexos_detalles")]
    public class DAnexoDetalle
    {
        [Key]
        public int id_anexo_detalle { get; set; }

        [Required]
        public int anexo_id { get; set; }

        [Required]
        public int anexo_menu_id { get; set; }        
        public string detalle { get; set; }
        public int cantidad { get; set; }
        public decimal factor { get; set; }
        public DateTime? fecha_carga { get; set; }
        public TimeSpan? hora_carga { get; set; }


        [Required]
        public int usuario_id { get; set; }


        //// Propiedad de navegación anexo       
        public virtual DAnexo anexo { get; set; }

        //// Propiedad de navegación anexoMenu       
        public virtual DAnexoMenu anexo_menu { get; set; }

        //// Propiedad de navegación usuario       
        public virtual DUsuario usuario { get; set; }


        
    }
}
