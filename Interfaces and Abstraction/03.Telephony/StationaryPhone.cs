using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Telephony
{
    public class StationaryPhone : ICallable
    {
        public string Call(string number)
        {
            if (PhoneNumberIsValid(number))
            {
                return $"Dialing... {number}";
            }
            return "Invalid number!";
        }
        public bool PhoneNumberIsValid(string number) => number.All(x => Char.IsDigit(x));

    }

}
