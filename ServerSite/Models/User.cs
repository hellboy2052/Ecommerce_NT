﻿using Microsoft.AspNetCore.Identity;

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
