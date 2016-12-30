using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel.Models
{
    [Table("department")]
    public partial class Department
    {
        [Column("pkDepartmentId")]
        public int PkDepartmentId { get; set; }
        [Required]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Required]
        [Column("address", TypeName = "varchar(100)")]
        public string Address { get; set; }
    }
}
