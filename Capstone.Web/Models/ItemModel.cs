using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ItemModel
    {
        public int Item_Id { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
    }
}