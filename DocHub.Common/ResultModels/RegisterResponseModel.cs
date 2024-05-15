using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Common.ResultModels
{
    public class RegisterResponseModel
    {
        public string Email { get; set; }
        public string Role { get; set; }

        public string ResultMessage { get; set; }

        public IEnumerable<UserErrorModel> Errors { get; set; }
        public bool Succeeded { get; set; } = false;
    }
}
