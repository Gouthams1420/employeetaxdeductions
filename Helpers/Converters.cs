using System;
using System.Collections.Generic;
using EmployeeWebApplication.Entites;
using EmployeeWebApplication.Models;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebApplication.Helpers
{
    public static class Converters
    {
        public static List<Entites.Person> ConvertEmployeeToPersonList(Employee employee)
        {
            var returnList = new List<Entites.Person>();

            returnList.Add(new Entites.Person() { Name = employee.Name, Type = Entites.PersonType.Employee });

            foreach (var dependent in employee.Dependents)
            {
                returnList.Add(new Entites.Person() { Name = dependent.Name, Type = ConvertDependentTypeToPersonType(dependent.Type) });
            }

            return returnList;
        }

        public static PersonType ConvertDependentTypeToPersonType(DependentType type)
        {
            switch (type)
            {
                case DependentType.Child:
                    return PersonType.Child;
                case DependentType.Spouse:
                    return PersonType.Spouse;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
