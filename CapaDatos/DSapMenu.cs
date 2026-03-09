using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("sap_menus")]
    public class DSapMenu
    {
        [Key]
        public int id_sap_menu { get; set; }

        [Required]
        public int sap_id { get; set; }

        [Required]
        public int unidad_id { get; set; }

        [Required]
        public int tipo_menu_id { get; set; }

        public string detalle_vigente { get; set; }
        public bool vigente { get; set; }


        // Propiedad de navegación sap       
        public virtual DSap sap { get; set; }

        // Propiedad de navegación tipo menu     
        public virtual DTipoMenu tipo_menu { get; set; }        

        // Propiedad de navegación unidad    
        public virtual DUnidad unidad { get; set; }
    }
}
