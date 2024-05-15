using DocHub.Common.DTO.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public string Token { get; set; }
        public RoleEnumDto Role { get; set; }

    }

}
