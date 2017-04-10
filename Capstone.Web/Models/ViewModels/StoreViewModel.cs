using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class StoreViewModel
    {
        public List<ItemModel> Hats { get; set; }
        public List<ItemModel> Backgrounds { get; set; }
        public MascotModel Mascot { get; set; }
        public int ItemId { get; set; }


    }
}