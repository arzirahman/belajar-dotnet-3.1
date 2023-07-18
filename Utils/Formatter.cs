using System.Globalization;

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
    }
}