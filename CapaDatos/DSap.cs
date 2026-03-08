using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("sap")]
    public class DSap
    {
        [Key]
        public int id_sap { get; set; }
        public string sap { get; set; }
        public int orden { get; set; }
        public bool vigente { get; set; }
    }
}
