using System;
using System.Text.RegularExpressions;

namespace CreditCardPdfStatementExtractor.Processors
{
    //TODO: Create interface for all processors
    public static class NubankProcessor
    {
        public static void Process(string pageText)
        {
            if (pageText.Contains("TRANSAÇÕES"))
            {
                var text = pageText.Split("VALORES EM R$");
                //Console.WriteLine($"{text[1]}");
                var regex = new Regex(@"(\d{2}\s{1}(?:JAN|FEV|MAR|ABR|MAI|JUN|JUL|AGO|SET|OUT|NOV|DEZ))(.*?)(?<!(?:USD|BRL|R\$)\s.?)((?:\d+\.)*?\d+,\d{2})");
                foreach (Match match in regex.Matches(text[1]))
                {
                    var entry = match.Groups[2].Value.Trim();
                    var value = match.Groups[3].Value;
                    var index = match.Groups[2].Value.Trim().LastIndexOf(',');
                    if (match.Groups[2].Value.Trim().Length == index + 1)
                    {
                        entry = string.Concat(entry, value.Substring(0, 2));
                        value = value.Substring(2);
                    }

                    //TODO: Remove Console dependencies
                    Console.WriteLine($"{match.Groups[1].Value};{entry};{value}");
                    //Console.WriteLine($"{match.Groups[1].Value};{match.Groups[2].Value.Trim()};{match.Groups[3].Value}");
                }

                Console.WriteLine();
            }
        }
    }
}
