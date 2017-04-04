using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class ParentModel
    {
        public int Parent_ID { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression("^(?=.[A-Z].[A-Z])(?=.[!@#$&])(?=.[0-9].[0-9])(?=.[a-z].[a-z].*[a-z]).{8}$", ErrorMessage ="Password does not meet requirements.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage ="Password do not match")]
        public string ConfirmPassword { get; set; }
       
    }
}