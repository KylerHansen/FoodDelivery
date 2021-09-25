using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Models
{
    public class MenuItem
    {     
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display (Name="Menu Item")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Price should be greater than $1")]
        public float Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int CategoryId { get; set; } //Also need to add it here. 

        public int FoodTypeId { get; set; }



        //To Connect Objects or Tables
        [ForeignKey("CategoryId")]  //this makes CategoryId a foreign key to the actually primary key in Category Model
        public virtual Category Category { get; set; }
        [ForeignKey("FoodTypeId")]
        public virtual FoodType FoodType { get; set; }

    }
}
