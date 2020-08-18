using System;
using System.IO;
using System.Linq;
using CreditCardStatementScrapper.Processors;
using CreditCardStatementScrapper.Enums;
using UglyToad.PdfPig;

namespace CreditCardStatementScrapper
{
    //TODO: Add README to explain the solution
    //TODO: Make solution available for community in in Github!
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Get this configs from program input, text file or hard coded
            var root = @"/Users/yurieastwood/Documents/CreditCardStatements";
            var files = Directory.GetFileSystemEntries(root, "*.pdf", SearchOption.TopDirectoryOnly).OrderBy(_ => _);

            Console.WriteLine($"Beggining to process statements...");
            Console.WriteLine("");

            foreach (var file in files)
            {
                using var document = PdfDocument.Open(file);
                var fileName = file.Split('/').Last();
                var bankName = fileName.Split('-').First();
                Console.WriteLine($"{fileName}");

                foreach (var page in document.GetPages())
                {
                    var success = Enum.TryParse<Bank>(bankName, true, out var bank);
                    var processor = ProcessorFactory.CreateNew(bank);
                        
                    //Console.WriteLine($"Page {page.Number}: {pageText}");
                    if (success)
                    {
                        var output = processor.ProcessTransactions(page.Text);

                        if (output.Count > 0)
                        {
                            foreach (var entry in output)
                                Console.WriteLine(entry);
                        }
                    } else
                    {
                        Console.WriteLine("Problem parsing bank name.");
                    }
                }

                Console.WriteLine("");
            }
        }
    }
}