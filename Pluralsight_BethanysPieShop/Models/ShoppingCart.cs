using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pluralsight_BethanysPieShop.Models
{
    public class ShoppingCart
    {
        //stores a local copy of the appDbContext passed in on the constructor.
        //  Used to get at the ShoppingCartItems in the Database.
        private readonly AppDbContext _appDbContext;

        //stores the shopping cart id for the current shopping cart 
        //  note: not stored in the Database.
        public string ShoppingCartId { get; set; }

        //list of the current items in the shopping cart from the database
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }


        //constructor for the class and set up local appDbContext varible
        public ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //create a session for the shopping cart
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            //local varible to store a session varible for the cart
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //get a current context to be passed to the created new ShoppingCart at end of function.
            var context = services.GetService<AppDbContext>();

            //create the new cartid string
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            //set the cart id
            session.SetString("CartId", cartId);


            //use this function to create a new object of the ShoppingCart class.  Hints that why this 
            //  method is static.  It will set the public property in the new class of ShoppingCartId to the 
            //  newly created cartId local to this function.
            return new ShoppingCart(context) { ShoppingCartId = cartId };

        }
        
        public void AddToCart(Pie pie, int amount)
        {

            //study more.  very confusing
            // uses lambda expression (parameters) => funcion
            // used to query the database with the WHERE s(db reference).PieId == pie(parameter).PieId and s(db reference).ShoppingCartId == (local property of this class)ShoppingCartId 
            var shoppingCartItem =
                   _appDbContext.ShoppingCartItems.SingleOrDefault(
                       s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);
            
            //if the above statement comes back null then there is no items currently in the shopping cart so we need to add them.
            if(shoppingCartItem == null)
            {

                //create a new instance of the shoppingCartItem 
                //   funky short had way of creating a new object and setting parameters.  
                //   need to learn and get familiar with doing it this way
                shoppingCartItem = new ShoppingCartItem  //make a new instance of shopping cart and save it to the null reference.
                {
                    //set the properties of the new ShoppingCartItem object
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,  //link the foreign key of the pie to the ShoppingCartItem
                    Amount = 1  //I don't see why this is set to one but it will do.
                };

                //add the newly created item to the database
                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            //else there was already a shopping cart item matching the id and the pie so just increment the amount
            else
            {
                //flat increment the amount of the previous shopping cart item 
                //  note this can only go up by one at a time.
                shoppingCartItem.Amount++;  
            }

            //save the changes of ether making a new entry into the ShoppingCartItem table or 
            //  incrementing the amount by one.
            _appDbContext.SaveChanges();
        }
        
        /**
         * Removes a Pie from the cart and return 
         * 
         */
        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }


    }
}
