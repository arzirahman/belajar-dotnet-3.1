using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Coba_Net.Utils;
using System.Linq;
using System.Collections.Generic;

namespace Coba_Net.Models
{
    public class Rent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The rental start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The rental end date is required.")]
        [FlowCheck(ErrorMessage = "The rental end date cannot be before the rental start date.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "The rented car is required.")]
        public Guid CarId { get; set; }

        public DateTime? ApprovedTime { set; get; }

        public DateTime? CancelledTime { set; get; }

        public DateTime CreatedAt { set; get; } = DateTime.Now;
        
        public bool IsCancelledByAdmin { set; get; } = false;

        [ForeignKey("CarId")]
        public Car Car { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public string FormUrl { get; set; }

        public string GetStatus()
        { 
            if (CancelledTime != null) return "Cancelled";
            else if (ApprovedTime == null) return "Pending";
            else if (DateTime.Now < StartDate) return "Approved";
            else if (DateTime.Now >= StartDate && DateTime.Now <= EndDate) return "Active";
            else if (DateTime.Now > EndDate) return "Complete";
            else return "";
        }

        public string[] GetProgress()
        {
            var status = GetStatus();
            var progress = new string[0];
            if (status == "Approved") progress = progress.Concat(new string[] { "Approved" }).ToArray();
            else if (status == "Active") progress = progress.Concat(new string[] { "Approved", "Active" }).ToArray();
            else if (status == "Complete") progress = progress.Concat(new string[] { "Approved", "Active", "Complete" }).ToArray();
            return progress;
        }

        public string GetStatusColor()
        {
            var Status = GetStatus();
            if (Status == "Pending") return "FFDF00";
            else if (Status == "Approved") return "EE82EE";
            else if (Status == "Active") return "0000FF";
            else if (Status == "Complete") return "00FF00";
            else if (Status == "Cancelled") return "FF0000";
            else return null;
        }
    }

    public class RentListView
    {
        public List<Rent> Rents { set; get; }
        public Pagination Pagination { set; get; }
    }
}