using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class ChooseMascotModel
    {
        [Required(ErrorMessage = "Please select a mascot.")]
        public string Mascot_Image { get; set; }
    }
}