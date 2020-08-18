using CreditCardStatementScrapper.Enums;

namespace CreditCardStatementScrapper.Processors
{
    public static class ProcessorFactory
    {

        public static IProcessor CreateNew(Bank bank)
        {
            IProcessor processor;

            switch (bank)
            {
                case Bank.Itau:
                    processor = new ItauProcessor();
                    break;

                case Bank.Nubank:
                default:
                    processor = new NubankProcessor();
                    break;
            }

            return processor;
        }
    }
}
