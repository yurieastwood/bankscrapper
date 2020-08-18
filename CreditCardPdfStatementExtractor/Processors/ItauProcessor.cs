using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CreditCardPdfStatementExtractor.Processors
{
    public class ItauProcessor : IProcessor
    {
        public List<string> ProcessTransactions(string pageText)
        {
            var result = new List<string>();

            if (pageText.Contains("Lançamentos"))
            {
                var text = pageText;

                var regex = new Regex(@"(\d{2}\s(?:JAN|FEV|MAR|ABR|MAI|JUN|JUL|AGO|SET|OUT|NOV|DEZ))(.*?(?:\d+,\d{2})?)((?:\d+\.)*?\d+,\d{2})(?=$|\s*\d{2}\s(?:JAN|FEV|MAR|ABR|MAI|JUN|JUL|AGO|SET|OUT|NOV|DEZ))");

                foreach (Match match in regex.Matches(text))
                {
                    var entry = match.Groups[2].Value.Trim();
                    var value = match.Groups[3].Value;

                    //TODO: Remove Console dependencies
                    result.Add($"{match.Groups[1].Value};{entry};{value}");
                }
            }

            return result;
        }
    }
}
