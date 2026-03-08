using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    [Table("usuarios")]
    public class DUsuario
    {
        [Key]
        public int id_usuario { get; set; }
        public string apellido { get; set; }
        public string nombre { get; set; }
        public int dni { get; set; }
        public int legajo { get; set; }
        public string contrasenia { get; set; }
        public bool is_activo { get; set; }

        [Required]
        public string rol_id { get; set; }

        // Propiedad de navegación        
        public virtual DRol rol { get; set; }
    }
}
