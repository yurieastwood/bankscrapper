using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CreditCardPdfStatementExtractor.Processors;
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
                    Console.WriteLine($"{file.Split('/').Last()}");

                    foreach (var page in document.GetPages())
                    {
                        //Console.WriteLine($"Page {page.Number}: {pageText}");
                        NubankProcessor.Process(page.Text);
                    }
                }

                return;
            }
        }
    }
}