using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpertAI.RequestAllocator.Models
{
    public class MakeRequestFormModel
    {
        [Required]
        public string Request { get; set; }

        [Required]
        public string Area { get; set; }
    }
}