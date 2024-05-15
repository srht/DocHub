using DocHub.Common.DTO.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Common.DTO.UserService
{
    public class RegisterDto:IDataTransfer
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public RoleEnumDto Role { get; set; }
    }
}
