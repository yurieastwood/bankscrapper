using System;
using System.Collections.Generic;

namespace CreditCardStatementScrapper.Processors
{
    public interface IProcessor
    {
        public abstract List<string> ProcessTransactions(string pageText);
    }
}
