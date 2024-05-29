using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace _03.Telephony
{
    public class Smartphone : IBrowsable, ICallable
    {
        public string Browse(string url)
        {
            if(UrlIsValid(url))
            {
                return $"Browsing: {url}!";
            }
            return "Invalid URL!";
        }
        public string Call(string phoneNumber)
        {
            if (PhoneNumberIsValid(phoneNumber))
            {
                return $"Calling... {phoneNumber}";
            }
            return "Invalid number!";
        }

        public bool PhoneNumberIsValid(string number) => number.All(x => Char.IsDigit(x));

        public bool UrlIsValid(string url) => url.All(x => !Char.IsDigit(x));
    }
}
