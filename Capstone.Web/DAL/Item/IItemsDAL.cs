using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DAL.Item
{
    public interface IItemsDAL
    {
        List<ItemModel> GetHats();
        List<ItemModel> GetBackgrounds();

    }
}
