using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.ResponseModels
{
    public class GetDepartmentResponseInfo
    {
        public GetDepartmentResponseInfo()
        {
            departments = new HashSet<Department>();
        }

        /// <summary>
        /// Departments collection from databse, to return to the UI.
        /// </summary>
        public ICollection<Department> departments { get; set; }
    }
}
