using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using FoodDelivery.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApplicationCore.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Infrastructure.Services;

namespace FoodDelivery.Pages.Customer.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;
        public IndexModel(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public OrderDetailsCartVM OrderDetailsCart { get; set; }

        public void OnGet()
        {
            OrderDetailsCart = new OrderDetailsCartVM()
            {
                OrderHeader = new OrderHeader(),
                ListCart = new List<ShoppingCart>()
            };

            OrderDetailsCart.OrderHeader.OrderTotal = 0;

            //get the current User Id
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim != null)
            {
                //get the shopping cart items for the user. 
                IEnumerable<ShoppingCart> cart = _unitofWork.ShoppingCart.List(c => c.ApplicationUserId == claim.Value);

                if(cart != null)
                {
                    OrderDetailsCart.ListCart = cart.ToList();
                }

                foreach (var cartList in OrderDetailsCart.ListCart)
                {
                    cartList.MenuItem = _unitofWork.MenuItem.Get(n => n.Id == cartList.MenuItemId);
                    OrderDetailsCart.OrderHeader.OrderTotal += (cartList.MenuItem.Price * cartList.Count); //calculate the total.
                }

            }
        }

        public IActionResult OnPostMinus(int cartId)
        {
            var cart = _unitofWork.ShoppingCart.Get(c => c.Id == cartId);
            if(cart.Count == 1)
            {
                _unitofWork.ShoppingCart.Delete(cart);
            }
            else
            {
                cart.Count -= 1;
                _unitofWork.ShoppingCart.Update(cart);
            }           

            _unitofWork.Commit();

            var cnt = _unitofWork.ShoppingCart.List(u => u.ApplicationUserId == cart.ApplicationUserId).Count();
            HttpContext.Session.SetInt32(SD.ShoppingCart, cnt);

            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostPlus(int cartId)
        {
            var cart = _unitofWork.ShoppingCart.Get(c => c.Id == cartId);
           
            cart.Count += 1;
            _unitofWork.ShoppingCart.Update(cart);
        

            _unitofWork.Commit();       

            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _unitofWork.ShoppingCart.Get(c => c.Id == cartId);
            _unitofWork.ShoppingCart.Delete(cart);

            _unitofWork.Commit();

            var cnt = _unitofWork.ShoppingCart.List(u => u.ApplicationUserId == cart.ApplicationUserId).Count();
            HttpContext.Session.SetInt32(SD.ShoppingCart, cnt);

            return RedirectToPage("/Customer/Cart/Index");
        }


    }
}
