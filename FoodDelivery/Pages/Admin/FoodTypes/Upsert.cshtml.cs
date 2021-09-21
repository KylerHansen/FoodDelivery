using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.Pages.Admin.FoodTypes
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitofWork _unitofWork;

        [BindProperty]
        public FoodType FoodTypeObj { get; set; }

        public UpsertModel(IUnitofWork unitofWork) => _unitofWork = unitofWork;

        public IActionResult OnGet(int? id)
        {
            FoodTypeObj = new FoodType();

            if (id != 0) //edit mode
            {
                FoodTypeObj = _unitofWork.FoodType.Get(u => u.Id == id);

                if (FoodTypeObj == null)
                {
                    return NotFound();
                }
            }
            return Page(); //assume insert new mode
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //If New
            if (FoodTypeObj.Id == 0)
            {
                _unitofWork.FoodType.Add(FoodTypeObj);
            }
            else  //existing record
            {
                _unitofWork.FoodType.Update(FoodTypeObj);
            }

            _unitofWork.Commit();

            return RedirectToPage("./Index");
        }   
    }
}
