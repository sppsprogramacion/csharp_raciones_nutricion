using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    
    [Table("unidades_grupo")]
    public class DUnidadGrupo
    {
        [Key]
        public int id_unidad_grupo { get; set; }
        public string unidad_grupo { get; set; }
        public int orden { get; set; }
        public bool vigente { get; set; }
    }
    
}
