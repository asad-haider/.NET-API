using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModel.ResponseModels
{
    public interface IListModelResponse<TModel> : IResponse
    {
        Int32 PageSize { get; set; }
        Int32 PageNumber { get; set; }
        Int32 TotalRecords { get; set; }
        IEnumerable<TModel> Model { get; set; }
    }
}
