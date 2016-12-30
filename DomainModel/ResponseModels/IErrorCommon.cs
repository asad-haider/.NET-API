
using System;
using System.Collections.Generic;

namespace DomainModel.ResponseModels
{
    public interface IErrorCommon
    {
        bool Success { get; set; }
        List<String> ErrorList { get; set; }
    }
}
