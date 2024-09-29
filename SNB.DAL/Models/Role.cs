﻿using Microsoft.AspNetCore.Identity;

namespace SNB.DAL.Models
{
    public class Role : IdentityRole
    {
        public string? Description { get; set; }

        public List<User> Users { get; set; } = new();
    }
}
