using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.ResponseModels
{
    public class GetStudentResponseInfo
    {
        public GetStudentResponseInfo()
        {
            studentsInfo = new HashSet<StudentsInfo>();
        }

        /// <summary>
        /// Hosts collection from databse, to return to the UI.
        /// </summary>
        public ICollection<StudentsInfo> studentsInfo { get; set; }
    }
}
