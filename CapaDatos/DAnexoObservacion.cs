using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("anexos_observaciones")]
    public class DAnexoObservacion
    {

        [Key]
        public int id_anexo_observacion { get; set; }
        public string observacion { get; set; }
        public bool vigente { get; set; }

        [Required]
        public int anexo_id { get; set; }

        [Required]
        public int usuario_id { get; set; }


        //// Propiedad de navegación anexo       
        public virtual DAnexo anexo { get; set; }

        //// Propiedad de navegación sap       
        public virtual DUsuario usuario { get; set; }
    }
}
