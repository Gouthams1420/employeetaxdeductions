using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeWebApplication.Models
{
        public enum DependentType
        {
            Spouse,
            Child
        }

        [ExcludeFromCodeCoverage]
        public class EmployeeDependent : Person
        {
            [Required]
            public DependentType Type { get; set; }
       }
    
}
