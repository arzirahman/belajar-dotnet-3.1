using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coba_Net.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The car name is required.")]
        [MaxLength(50, ErrorMessage = "The maximum length for the name is 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The car brand is required.")]
        [MaxLength(50, ErrorMessage = "The maximum length for the brand is 50 characters.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "The car color is required.")]
        [MaxLength(20, ErrorMessage = "The maximum length for the color is 20 characters.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "The car price is required.")]
        [Range(0, float.MaxValue, ErrorMessage = "The car price must be greater than or equal to 0.")]
        public float Price { get; set; }
    }
}
