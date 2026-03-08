using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("unidades")]
    public class DUnidad
    {
        [Key]
        public int id_unidad { get; set; }
        public string unidad { get; set; }
        public int orden { get; set; }
        public bool vigente { get; set; }
    }
}
