using System.Globalization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Coba_Net.Utils
{
    public class Formatter
    {
        public Formatter() {}

        public string FormatMoney(string numberString)
        {
            decimal number = decimal.Parse(numberString);
            CultureInfo culture = new CultureInfo("id-ID");
            string formattedNumber = string.Format("Rp. {0:N0}", number);
            return formattedNumber;
        }

        public Dictionary<string, object> ObjectToDictionary(object obj)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                dictionary[propertyInfo.Name] = propertyInfo.GetValue(obj);
            }
            return dictionary;
        }

        public bool IsEmailValid(string email)
        {
            string pattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
    }
}