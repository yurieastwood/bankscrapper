using System;
using System.IO;
using System.Linq;
using CreditCardStatementScrapper.Processors;
using CreditCardStatementScrapper.Enums;
using UglyToad.PdfPig;
using System.Text;

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
            var fileContent = new StringBuilder();

            Console.WriteLine($"Beggining to process statements...");
            Console.WriteLine("");

            foreach (var file in files)
            {
                using var document = PdfDocument.Open(file);
                var fileName = file.Split('/').Last();
                var bankName = fileName.Split('-').First();
                fileContent.AppendLine($"{fileName}");

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
                                fileContent.AppendLine(entry);
                        }
                    } else
                    {
                        Console.WriteLine("Problem parsing bank name.");
                    }
                }

                fileContent.AppendLine("");
            }

            File.WriteAllText(Path.Combine(root, "_ProcessedFiles.txt"), fileContent.ToString());
            Console.Write(fileContent);

            Console.WriteLine($"End of processing!");
        }
    }
}