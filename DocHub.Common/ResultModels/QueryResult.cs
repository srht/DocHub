using DocHub.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Common.ResultModels
{
    public class QueryResult
    {
        public int StatusCode { get; set; }
        public string Detail { get; set; }
        public ResultTypes ResultType { get; set; }
    }
}
