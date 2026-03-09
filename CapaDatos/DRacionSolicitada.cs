using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("raciones_solicitadas")]
    public class DRacionSolicitada
    {
        [Key]
        public int id_racion_solicitada { get; set; }                
        public string convenio { get; set; }
        public DateTime fecha_solicitada { get; set; }
        public string detalles { get; set; }
        public DateTime? fecha_carga { get; set; }

        [Required]
        public int usuario_id { get; set; }

        // Propiedad de navegación usuario       
        public virtual DUsuario usuario { get; set; }

        //propiedad de navegacion detalles
        public virtual ICollection<DRacionesSolicitadasDetalles> raciones_solicitadas_detalles { get; set; }
    }
}
