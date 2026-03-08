using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("roles")]
    public class DRol
    {
        [Key]
        public string id_rol { get; set; }
        public string rol { get; set; }


    }
}
