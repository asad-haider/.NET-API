using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.RequestModels
{
    public class AddUpdateStudentRequestDTO : BaseServiceRequest
    {
        /// <summary>
        /// HostId, to update the values in the database.
        /// </summary>
        public short id { get; set; }

        /// <summary>
        ///Name of the Student.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Name of the Class.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Name of the department.
        /// </summary>
        public string Department { get; set; }

    }
}
