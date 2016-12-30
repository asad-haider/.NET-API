using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.RequestModels
{
    public class AddUpdateDepartmentRequestDTO : BaseServiceRequest
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
        public string Address { get; set; }

    }
}
