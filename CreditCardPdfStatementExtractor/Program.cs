using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;

namespace CreditCardPdfStatementExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = @"/Users/yurieastwood/Downloads";
            var folders = new List<string>{ "cartao-nubank", "cartao-itau-5827", "cartao-itau-6788" };

            foreach (var folder in folders)
            {
                var files = Directory.GetFileSystemEntries(Path.Combine(root, folder));

                foreach (var file in files)
                {
                    using var document = PdfDocument.Open(file);
                    foreach (var page in document.GetPages())
                    {
                        var pageText = page.Text;
                        //Console.WriteLine($"Page {page.Number}: {pageText}");

                        if (pageText.Contains("TRANSAÇÕES"))
                        {
                            Console.WriteLine($"{file.Split('/').Last()}");

                            var text = pageText.Split("VALORES EM R$");
                            Console.WriteLine($"{text[1]}");
                            var regex = new Regex(@"(\d{2}\s{1}(?:JAN|FEV|MAR|ABR|MAI|JUN|JUL|AGO|SET|OUT|NOV|DEZ))(.*?)(?<!(?:USD|BRL|R\$)\s.?)((?:\d+\.)*?\d+,\d{2})");
                            foreach (Match match in regex.Matches(text[1]))
                                Console.WriteLine($"{match.Groups[1].Value};{match.Groups[2].Value.Trim()};{match.Groups[3].Value}");
                        }

                    }
                }

                return;
            }
        }
    }
}