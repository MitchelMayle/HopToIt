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
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }
    }
}