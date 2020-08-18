using System.Collections.Generic;

namespace CreditCardStatementScrapper.Processors
{
    internal abstract class DefaultProcessor : IProcessor
    {
        public List<string> ProcessTransactions(string pageText)
        {
            throw new System.NotImplementedException();
        }
    }
}