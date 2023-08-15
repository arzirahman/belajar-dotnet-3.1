using System.Globalization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;

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

        public string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }

            return stringBuilder.ToString();
        }
    }
}