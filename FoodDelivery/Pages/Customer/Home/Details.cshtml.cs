using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.Pages.Customer.Home
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;
        public DetailsModel(IUnitofWork unitofWork) => _unitofWork = unitofWork;

        [BindProperty] //ONly one model is allowed to be bound. 
        public ShoppingCart ShoppingCartObj { get; set; }       

        public async Task OnGet(int id) //Id is not optional 
        {

            ShoppingCartObj = new ShoppingCart()
            {
                MenuItem = await _unitofWork.MenuItem.GetAsync(m => m.Id == id, false, "Category,FoodType")
            };
               //TODO: GETTING A NULL REFERENCE ERROR. 
            ShoppingCartObj.MenuItemId = id; //hidden html object required to pass it back. 
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //get the applicationUserId from aspnetusers table
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                ShoppingCartObj.ApplicationUserId = claim.Value;

                //if there's already a cart for this user, retrieve it
                ShoppingCart cartFromDb = _unitofWork
                    .ShoppingCart.Get(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId
                    && c.MenuItemId == ShoppingCartObj.MenuItemId);

                if(cartFromDb == null)
                {
                    _unitofWork.ShoppingCart.Add(ShoppingCartObj);
                }
                else
                {
                    cartFromDb.Count += ShoppingCartObj.Count;
                    _unitofWork.ShoppingCart.Update(cartFromDb);
                }
                _unitofWork.Commit();

                //this is for the icon on the shared layout menu
                var count = _unitofWork.ShoppingCart.List(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId).Count();
                HttpContext.Session.SetInt32(SD.ShoppingCart, count); //stores it to the local session. 
                //The session variable gets created under the session Name ShoppingCart. That variable continues from page to page
                //until the session ends. 
                return RedirectToPage("Index");
            }
            else
            {
                ShoppingCartObj.MenuItem = _unitofWork.MenuItem.Get(m => m.Id == ShoppingCartObj.MenuItemId, false, "Category,FoodType");
            }
            return Page();
        }
    }
}
