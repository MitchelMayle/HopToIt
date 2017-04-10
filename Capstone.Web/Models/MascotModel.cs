using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class MascotModel
    {
        public int Mascot_Id { get; set; }
        public string Mascot_Image { get; set; }
        public string CurrentHat { get; set; }
        public string CurrentBackground { get; set; }
        public int Child_Id { get; set; }
        public bool BaseballHat { get; set; }
        public bool Beanie { get; set; }
        public bool Bonnet { get; set; }
        public bool BucketHat { get; set; }
        public bool Crown { get; set; }
        public bool Flower { get; set; }
        public bool PropellerHat { get; set; }
        public bool Sombrero { get; set; }
        public bool TopHat { get; set; }
    }
}