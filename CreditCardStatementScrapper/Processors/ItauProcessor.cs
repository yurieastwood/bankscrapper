using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CreditCardStatementScrapper.Processors
{
    public class ItauProcessor : IProcessor
    {
        public List<string> ProcessTransactions(string pageText)
        {
            var result = new List<string>();

            if (pageText.Contains("Lançamentos"))
            {
                var text = pageText;

                var regex = new Regex(@"(?:[^(])((?:0[1-9]|[1-2][0-9]|3[0-1])/(?:0[1-9]|[1][0-2]))((?:[^/)\d]).+?(?:\d{2}/\d{2}|LJ\s\d{1,4}|W\s\d{1,8}|[*]\d{1,10}|FL\s\d{1,3}|TELEFONE\s\d{1,8}|ENERGIA\s\d{1,8}|BR\s\d{1,8}|\sL\d{1,3}|BETHAVILLE\s\d{1}|\sGUARUJA\s\d{1,3}|\sSANTOS\s\d{1,3}|\sBZ\s\d{1,2}|[*]Saraiva\d{1,3})?)((?:-\s)?\d+,\d{2})");

                foreach (Match match in regex.Matches(text))
                {
                    var entry = match.Groups[2].Value.Trim();
                    var value = match.Groups[3].Value;

                    result.Add($"{match.Groups[1].Value};{entry};{value}");
                }
            }

            return result;
        }
    }
}
