using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.RequestModels
{
    public class AddUpdateStudentRequestDTO : BaseServiceRequest
    {
        public int Id { get; set; }
        [Required]
        [StringLength(5)]
        public string Name { get; set; }
        [Required]
        [StringLength(5)]
        public string Class { get; set; }
        [Required]
        [StringLength(5)]
        public string Department { get; set; }

    }
}
