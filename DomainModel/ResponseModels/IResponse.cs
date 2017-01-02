using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.ResponseModels
{
    public interface IResponse
    {
        Boolean Success { get; set; }
    }
}
