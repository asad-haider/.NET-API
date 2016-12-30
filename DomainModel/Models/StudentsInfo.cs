using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel.Models
{
    [Table("studentsInfo")]
    public partial class StudentsInfo
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Class { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Department { get; set; }
    }
}
