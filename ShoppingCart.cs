using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winui_cooler
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<MedicineShoppingCartView> View { get; set; }
    }
}
