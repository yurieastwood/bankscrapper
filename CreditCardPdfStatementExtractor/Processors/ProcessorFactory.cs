namespace CreditCardPdfStatementExtractor.Processors
{
    public static class ProcessorFactory
    {

        public static IProcessor CreateNew(string bank)
        {
            IProcessor processor = new DefaultProcessor();

            switch (bank)
            {
                case "itau":
                    processor = new ItauProcessor();
                    break;

                case "nubank":
                default:
                    processor = new NubankProcessor();
                    break;
            }

            return processor;
        }
    }
}
