using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.ResponseModels
{
    public class SingleModelResponse<TModel> : BaseServiceResponse
    {
        public TModel Model { get; set; }
    }
}
