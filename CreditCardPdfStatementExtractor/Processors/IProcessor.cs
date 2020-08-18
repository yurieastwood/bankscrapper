using System;
using System.Collections.Generic;

namespace CreditCardPdfStatementExtractor.Processors
{
    public interface IProcessor
    {
        public abstract List<string> ProcessTransactions(string pageText);
    }
}
