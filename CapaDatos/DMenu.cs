using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("menus")]
    public class DMenu
    {
        [Key]
        public int id_menu { get; set; }
        public string menu_descripcion { get; set; }
        public decimal factor_desayuno { get; set; }
        public decimal factor_almuerzo { get; set; }
        public decimal factor_merienda { get; set; }
        public decimal factor_cena { get; set; }
        public string detalle { get; set; }
        public int orden { get; set; }
        public bool vigente { get; set; }
    }
}
