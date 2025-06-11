using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.EnterpriseBusiness.Layer.Entitys
{
    public  class BookEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        [Column("author")]
        public string Author { get; set; }

        [Required]
        [StringLength(50)]
        [Column("category")]
        public string Category { get; set; }

        [Column("summary")]
        public string Summary { get; set; }

        [Required]
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("modified_date")]
        public DateTime? ModifiedDate { get; set; }

    }
}
