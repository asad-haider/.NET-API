using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.ResponseModels
{
    public class BaseServiceResponse
    {
        public DateTime RequestTimeStamp { get; set; }
        public DateTime ResponseTimeStamp { get; set; }
        public Boolean Success { get; set; }

        public BaseServiceResponse()
        {
            ResponseTimeStamp = DateTime.Now;
        }
    }
}
