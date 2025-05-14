using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class CustomerModel
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Full name can't exceed 50 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter a valid 10-digit mobile number.")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address can't exceed 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City name can't exceed 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [StringLength(50, ErrorMessage = "State name can't exceed 50 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; }

       
    }
}
