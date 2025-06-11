using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Books.EnterpriseBusiness.Layer.Entitys
{
    public class UserEntity:IdentityUser<int>

    {
        // Los campos básicos ya están heredados de IdentityUser:
        // Id, Email, UserName, etc.

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }

    
}


