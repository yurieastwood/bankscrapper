using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CreditCardPdfStatementExtractor.Processors;
using UglyToad.PdfPig;

namespace CreditCardPdfStatementExtractor
{
    //TODO: Add README to explain the solution
    //TODO: Make solution available for community in in Github!
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Get this configs from program input, text file or hard coded
            var root = @"/Users/yurieastwood/Downloads";
            var folders = new List<string>{ "cartao-nubank", "cartao-itau-5827", "cartao-itau-6788" };

            foreach (var folder in folders)
            {
                var files = Directory.GetFileSystemEntries(Path.Combine(root, folder), "*.pdf", SearchOption.TopDirectoryOnly);

                foreach (var file in files)
                {
                    using var document = PdfDocument.Open(file);
                    Console.WriteLine($"{file.Split('/').Last()}");

                    foreach (var page in document.GetPages())
                    {
                        //Console.WriteLine($"Page {page.Number}: {pageText}");

                        //TODO: Create other cards processors
                        //TODO: Create a Factory to deal with processors creation
                        NubankProcessor.Process(page.Text);
                    }
                }

                return;
            }
        }
    }
}