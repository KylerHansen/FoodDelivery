using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public String Name { get; set; }

        [Display(Name="Display Order")]
        public int DisplayOrder { get; set; }
    }
}
