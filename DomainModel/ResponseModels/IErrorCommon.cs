
using System;
using System.Collections.Generic;

namespace DomainModel.ResponseModels
{
    public interface IErrorCommon
    {
        List<String> ErrorList { get; set; }
    }
}
