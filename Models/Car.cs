using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

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

        public string PicUrl { set; get; }

        [Required(ErrorMessage = "The car price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The car price must be greater than or equal to 0.")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class CarListView
    {
        public List<Car> Cars { set; get; }
        public Pagination Pagination { set; get; }
    }

    public class CarFormatModel
    {
        public string Key { set; get; }
        public Dictionary<string, object> Dictionary { set; get; }
    }
}
