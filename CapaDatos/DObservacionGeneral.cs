using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("observaciones_generales")]
    public class DObservacionGeneral
    {
        [Key]
        public int id_observacion_general { get; set; }
        public string observacion { get; set; }
        public string historial { get; set; }
        public bool vigente { get; set; }

        [Required]
        public int usuario_id { get; set; }


        //// Propiedad de navegación sap       
        public virtual DUsuario usuario { get; set; }
    }
}
