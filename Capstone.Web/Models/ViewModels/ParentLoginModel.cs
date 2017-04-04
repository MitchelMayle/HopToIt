using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class ParentLoginModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "User Name:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }
}