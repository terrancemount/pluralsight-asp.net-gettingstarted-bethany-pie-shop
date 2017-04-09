using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



//create a table with items in a shopping cart.  This is a line item for pies only.  Not for any
// other merchandise.
namespace Pluralsight_BethanysPieShop.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }  //id for the item only
        public Pie Pie { get; set; }            //which pie is it referencing (foreign Key)
        public int Amount { get; set; }         //How many pies ordered
        public string ShoppingCartId { get; set; }  //foriegn key to the whole cart.  I don't know why this is not int.
    }
}
