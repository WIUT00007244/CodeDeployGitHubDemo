using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSCC_CW1_7244_2.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }


        
        public Department EmployeeDepartment { get; set; }

        public string Position { get; set; }
        public decimal Salary { get; set; }
        
    }
}