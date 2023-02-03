namespace Domain.TransactionArea
{
    public class TransactionManager
    {
        private readonly IJsonManager _jsonManager;
        public TransactionManager() { }
        public TransactionManager(IJsonManager jsonManager)
        {
            _jsonManager = jsonManager;
        }

        public Transaction CreateTransaction(string id, string transactionDate, string amount)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException($"Parameter {nameof(id)} is null or empty, please try again");
            }
            if (string.IsNullOrEmpty(transactionDate))
            {
                throw new ArgumentNullException($"Parameter {nameof(transactionDate)} is null or empty, please try again");
            }
            if (string.IsNullOrEmpty(amount))
            {
                throw new ArgumentNullException($"Parameter {nameof(amount)} is null or empty, please try again");
            }

            int idInt;
            try
            {
                idInt = Convert.ToInt32(id);
            }
            catch (OverflowException ex)
            {
                throw new ArgumentException($"{id} is greater than {Int32.MaxValue} or less than {Int32.MinValue}, this is no allowed, try again");
            }
            catch (FormatException ex)
            {
                throw new ArgumentException($"{nameof(id)} - '{id}' is not a number format, this is not allowed, try again");
            }

            if (idInt < 0)
            {
                throw new ArgumentException($"{nameof(id)} - '{id}' should be greater or equal 0, try again");
            }

            DateTime transDate;
            if (!DateTime.TryParse(transactionDate, out transDate))
            {
                throw new ArgumentException($"{nameof(transactionDate)} - '{transactionDate}' is not a Date format, this is not allowed, try again");
            }

            decimal amountDec;
            try
            {
                amount = amount.Replace('.', ',');
                amountDec = Convert.ToDecimal(amount);
            }
            catch (OverflowException ex)
            {
                throw new ArgumentException($"{amount} is greater than {Decimal.MaxValue} or less than {Decimal.MinValue}, this is no allowed, try again");
            }
            catch (FormatException ex)
            {
                throw new ArgumentException($"{nameof(amount)} - '{amount}' is not a number format, this is not allowed, try again");
            }

            var transaction = new Transaction
            {
                Id = idInt,
                TransactionDate = transDate,
                Amount = amountDec
            };

            _jsonManager.SaveTransaction(transaction);

            return transaction;
        }
    }
}
