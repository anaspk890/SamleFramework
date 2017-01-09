using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FrameworkWebClient.Models;

namespace FrameworkWebClient.ViewModels
{
    [DisplayName("Employees")]
    public class EmployeeInsertVM : EmployeeBase
    {
        //data anotations go here, if any
        [DisplayName("Department Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "This is required field")]
        [StringLength(50, MinimumLength = 1)]
        public override string DepName { get; set; }

        [CustomStringLength(1, 50)]
        public override string EmpName { get; set; }

        //other operation specific properties
    }

    public class CustomStringLength : StringLengthAttribute
    {
        public CustomStringLength(int minLength, int MaxLength):base(MaxLength)
        {
            base.MinimumLength = minLength;
        }
    }
}
