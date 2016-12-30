using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.ResponseModels
{
    public class BaseServiceResponse : ErrorCommon
    {
        public Guid SessionId { get; set; }
        public DateTime RequestTimeStamp { get; set; }
        public DateTime ResponseTimeStamp { get; set; }

        public BaseServiceResponse()
        {
            ResponseTimeStamp = DateTime.Now;
        }
    }
}
