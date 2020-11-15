using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CreditCardStatementScrapper.Processors
{
    //TODO: Create interface for all processors
    public class NubankProcessor : IProcessor
    {
        public List<string> ProcessTransactions(string pageText)
        {
            var result = new List<string>();

            if (pageText.Contains("TRANSAÇÕES"))
            {
                // Removing useless beggining and ending information from transactions page
                var text = pageText.Split("VALORES EM R$")[1].Split("YURI TAVARES DOS SANTOS EASTWOOD");

                var regex = new Regex(@"(\d{2}\s(?:JAN|FEV|MAR|ABR|MAI|JUN|JUL|AGO|SET|OUT|NOV|DEZ))(.*?(?:\d+,\d{2})?)((?:\d+\.)*?\d+,\d{2})(?=$|\s*\d{2}\s(?:JAN|FEV|MAR|ABR|MAI|JUN|JUL|AGO|SET|OUT|NOV|DEZ))");

                foreach (Match match in regex.Matches(text[0]))
                {
                    var entry = match.Groups[2].Value.Trim();
                    var value = match.Groups[3].Value;

                    if (!entry.Contains("Pagamento em "))
                        result.Add($"{match.Groups[1].Value};{entry};{value}");
                }
            }

            return result;
        }
    }
}
