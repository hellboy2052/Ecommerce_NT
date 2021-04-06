using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Models
{
    public class User : IdentityUser
    {
        public User() : base()
        {

        }
        public User(string userName) : base(userName)
        {

        }
        [PersonalData]
        public string fullName { get; set; }
    }
}
