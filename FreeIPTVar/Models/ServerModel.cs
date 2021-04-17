using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeIPTVar.Models
{
    public class ServerModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int ServerID { get; set; }

        [Required(ErrorMessage = "Fill Title")]
        public string ServerTitle { get; set; }

       
        public string ServerLink { get; set; }

        [Display(Name = "Image")]
        public string ServerImage { get; set; }

        [Required(ErrorMessage = "Fill Description")]
        public string ServerDescription { get; set; }

        [Required(ErrorMessage = "Fill CategoryID")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public List<Category> Categories { get; set; }
    }
}