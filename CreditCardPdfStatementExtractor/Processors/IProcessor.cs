using System;
using System.Collections.Generic;

namespace CreditCardPdfStatementExtractor.Processors
{
    public interface IProcessor
    {
        public static List<string> ProcessTransactions()
        {
            throw new NotImplementedException(nameof(ProcessTransactions));
        }
    }
}
