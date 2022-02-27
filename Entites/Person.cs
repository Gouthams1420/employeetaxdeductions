using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebApplication.Entites
{
   
    public class Person
    {
        public string Name { get; set; }
        public PersonType Type { get; set; }
    }
}
