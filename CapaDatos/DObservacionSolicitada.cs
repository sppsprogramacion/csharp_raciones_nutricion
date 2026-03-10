using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("observaciones_solicitada")]
    public class DObservacionSolicitada
    {
        [Key]
        public int id_observacion_solicitada { get; set; }
        public string observacion { get; set; }
        public string historial { get; set; }
        public bool vigente { get; set; }

        [Required]
        public int racion_solicitada_id { get; set; }

        [Required]
        public int usuario_id { get; set; }


        //// Propiedad de navegación racion solicitada       
        public virtual DRacionSolicitada racion_solicitada { get; set; }

        //// Propiedad de navegación sap       
        public virtual DUsuario usuario { get; set; }
    }
}
