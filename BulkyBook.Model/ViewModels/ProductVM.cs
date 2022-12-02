using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BulkyBook.Model.ViewModels
{
    public class ProductVM
    {
        public Product product { get; set; }

        //Projection using select
        // fetch all category
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }


        //Projection using select
        // fetch all category
        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }

    }
}
