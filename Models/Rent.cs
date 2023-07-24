using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Coba_Net.Utils;
using System.Linq;
using System.Reflection;

namespace Coba_Net.Models
{
    public class Rent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The rental start date is required.")]
        [DateNotInPast(ErrorMessage = "The rental start date cannot be in the past.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The rental end date is required.")]
        [FlowCheck(ErrorMessage = "The rental end date cannot be before the rental start date.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "The rented car is required.")]
        public Guid CarId { get; set; }

        [ForeignKey("CarId")]
        public Car Car { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public string FormUrl { get; set; }

        public RentalStatus Status { get; set; } = RentalStatus.Pending;

        public string GetStatusColor()
        {
            if (Status.ToString() == "Pending") return "FFD700";
            else if (Status.ToString() == "Active") return "54f14c";
            else if (Status.ToString() == "Completed") return "1E90FF";
            else if (Status.ToString() == "Canceled") return "FE0000";
            else return null;
        }
    }

    public enum RentalStatus
    {
        Pending,
        
        Active,

        Completed,

        Canceled
    }
}