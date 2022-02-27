using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeWebApplication.Entites;
using EmployeeWebApplication.Business_Logic.Interfaces;

namespace EmployeeWebApplication.Business_Logic
{
    public class DiscountByNameCalculator : IDiscountCalculator
    {
        public decimal GetDiscountRate(Person person)
        {
            if (person?.Name?.ToLower().StartsWith("a") ?? false)
                return Constants.TEN_PERCENT_DISCOUNT_RATE; // 10 percent discount rate
            else
                return Constants.ZERO_PERCENT_DISCOUNT_RATE; // no discount
        }
    }
}
