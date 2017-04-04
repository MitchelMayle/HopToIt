using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ChildModel
    {
        public int Child_Id { get; set; }
        public int Parent_Id { get; set; }

        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password is required.")]   
        public string Password { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string First_Name { get; set; }

        public int Steps { get; set; }
        public int Active_Minutes { get; set; }
    }
}