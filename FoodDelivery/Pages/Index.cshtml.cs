﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Pages
{
    public class IndexModel : PageModel
    { 
        public IActionResult OnGet()
        {
            return RedirectToPage("/Customer/Home/Index");
        }
    }
}
