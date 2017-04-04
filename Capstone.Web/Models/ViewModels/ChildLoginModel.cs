using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class ChildLoginModel
    {
            [Required(ErrorMessage = "*")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "*")]
            public string Password { get; set; }
        }
    }