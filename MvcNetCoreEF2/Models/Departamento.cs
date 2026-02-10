using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNetCoreEF2.Models
{
    // Mediante decoraciones de mapping vamos indicando a que tabla/view apunta la clase

    [Table("DEPT")]
    public class Departamento
    {
        [Key]
        [Column("DEPT_NO")]
        public int Dept { get; set; }

        [Column("DNOMBRE")]
        public string Nombre { get; set; }

        [Column("LOC")]
        public string Localizacion { get; set; }

    }
}
