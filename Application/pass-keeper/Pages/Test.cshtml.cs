﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pass_keeper.Pages
{
    public class TestModel : PageModel
    {        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Your test page.";
        }
    }
}