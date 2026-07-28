using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("anexos")]
    public class DAnexo
    {
        [Key]
        public int id_anexo { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime? fecha_carga { get; set; }

        [Required]
        public int usuario_id { get; set; }


        // Propiedad de navegación usuario       
        public virtual DUsuario usuario { get; set; }

        //propiedad de navegacion detalles
        public virtual List<DAnexoDetalle> anexo_detalles { get; set; }

        //propiedad de navegacion observaciones
        public virtual ICollection<DAnexoObservacion> anexo_observaciones { get; set; }
    }
}
