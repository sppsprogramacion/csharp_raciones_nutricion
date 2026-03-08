using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("raciones_elaboradas")]
    public class DRacionElaborada
    {
        [Key]
        public int id_racion_elaborada { get; set; }
        public string convenio { get; set; }
        public DateTime fecha_elaborada { get; set; }
        public string detalles { get; set; }
        public DateTime? fecha_carga { get; set; }

        [Required]
        public int usuario_id { get; set; }
               

        // Propiedad de navegación sap       
        public virtual DUsuario usuario { get; set; }

        //propiedad de navegacion detalles
        public virtual List<DRacionElaboradaDetalles> raciones_elaboradas_detalles { get; set; }
    }
}
