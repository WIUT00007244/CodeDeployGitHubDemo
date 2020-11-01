using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSCC_CW1_7244_2.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Display(Name = "Department name")]
        public string Name { get; set; }
    }
}