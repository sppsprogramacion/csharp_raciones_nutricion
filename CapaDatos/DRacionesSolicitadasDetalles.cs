using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("raciones_solicitadas_detalles")]
    public class DRacionesSolicitadasDetalles
    {
        [Key]
        public int id_racion_solicitada_detalle { get; set; }

        [Required]
        public int racion_solicitada_id { get; set; }

        [Required]
        public int sap_id { get; set; }

        [Required]
        public int unidad_id { get; set; }

        [Required]
        public int tipo_menu_id { get; set; }

        public int almuerzo { get; set; }
        public int cena { get; set; }
        public DateTime? fecha_carga { get; set; }
        public TimeSpan? hora_carga { get; set; }

        [Required]
        public int usuario_id { get; set; }
        

        //// Propiedad de navegación racion solicitada       
        public virtual DRacionSolicitada racion_solicitada { get; set; }

        //// Propiedad de navegación sap       
        public virtual DSap sap { get; set; }

        //// Propiedad de navegación tipo menu     
        public virtual DTipoMenu tipo_menu { get; set; }        

        //// Propiedad de navegación unidad    
        public virtual DUnidad unidad { get; set; }

        //// Propiedad de navegación sap       
        public virtual DUsuario usuario { get; set; }
    }
}
