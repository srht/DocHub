using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Common.Extensions
{
    public static class AuthProcessExtension
    {
        public static IApplicationBuilder UseAuthProcess(this IApplicationBuilder builder)
        {
            return builder;
        }
    }
}
