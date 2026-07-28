using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("anexo_menus_tipos")]
    public class DAnexoMenuTipo
    {
        [Key]
        public int id_anexo_menu_tipo { get; set; }
        public string menu_tipo { get; set; }
        public int orden { get; set; }
        public bool vigente { get; set; }
                
    }
}
