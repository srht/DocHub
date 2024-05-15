using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Common.DTO.Users
{
    public class AuthRequestDto:IDataTransfer
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
