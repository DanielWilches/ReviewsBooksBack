using System;
using System.ComponentModel.DataAnnotations;

namespace Books.EnterpriseBusiness.Layer.Entitys
{
    public class CustomUserProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IdentityUserId { get; set; } // Relación con UserEntity/Identity

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime CreatedDate { get; set; }

        // Puedes agregar más campos personalizados aquí
    }
}