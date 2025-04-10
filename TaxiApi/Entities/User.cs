﻿using System.Diagnostics.Contracts;

namespace TaxiApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public virtual Driver Driver { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
