
using System;
using System.Collections.Generic;

namespace DomainModel.ResponseModels
{
    public class ErrorCommon : IErrorCommon
    {
        public bool Success { get; set; }
        public List<String> ErrorList { get; set; }

        public ErrorCommon()
        {
            ErrorList = new List<String>();
        }
    }
}
