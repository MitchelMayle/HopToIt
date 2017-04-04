using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class ParentRegistrationModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
