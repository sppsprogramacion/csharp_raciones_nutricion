using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("tipos_menu")]
    public class DTipoMenu
    {
        [Key]
        public int id_tipo_menu { get; set; }
        public string tipo_menu { get; set; }
        public string detalle { get; set; }
        public int orden { get; set; }
        public bool vigente { get; set; }

        [Required]
        public int menu_id { get; set; }

        // Propiedad de navegación        
        public virtual DMenu menu { get; set; }
    }
}
