using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("anexo_menus")]
    public class DAnexoMenu
    {
        [Key]
        public int id_anexo_menu { get; set; }
        public string menu { get; set; }
        public int orden { get; set; }
        public bool vigente { get; set; }

        [Required]
        public int anexo_menu_tipo_id { get; set; }

        // Propiedad de navegación        
        public virtual DAnexoMenuTipo anexo_menu_tipo { get; set; }
    }
}
