using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Common.ResultModels
{
    public class AuthResponseModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string ResultMessage { get; set; }
        public IEnumerable<UserErrorModel> Errors { get; set; }
        public bool Succeeded { get; set; } = false;
    }
}
