using Domain.TransactionArea;
using Infrastructure;

Dictionary<int, string> transactions = new Dictionary<int, string>();
JsonManager jsonManager = new JsonManager(transactionsFolderPath: "./transactions/");
TransactionManager transactionManager = new TransactionManager(jsonManager);
Console.WriteLine("Operations allowed:\nAdd - insert transaction data\nGet - get transaction by id\nExit - exit from application");

string input = "start";

while (input.ToLower() != "exit")
{
    Console.WriteLine("Input your command: ");
    input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
        continue;

    switch (input.ToLower())
    {
        case "add":
            Console.Write("Type Id: ");
            string id = Console.ReadLine();

            Console.Write("Type Transaction date: ");
            string date = Console.ReadLine();

            Console.Write("Type Amount: ");
            string amount = Console.ReadLine();

            Transaction transaction;
            try
            {
                transaction = transactionManager.CreateTransaction(id, date, amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                break;
            }

            if (transactions.Keys.Contains(transaction.Id))
            {
                Console.WriteLine($"A transaction with id: {id} already exists, try again");
                break;
            }

            Console.WriteLine($"Transaction with id: {id} was successfully added");
            break;

        case "get":
            Console.Write("Type Id: ");
            id = Console.ReadLine();
            int idInt;
            string transactionJsonString;

            if (!int.TryParse(id, out idInt))
            {
                Console.WriteLine($"Id - '{id}' has incorrect format, try again");
                break;
            }

            if (!transactions.ContainsKey(idInt))
            {
                try
                {
                    transactionJsonString = jsonManager.GetTransactionStr(idInt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                transactions[idInt] = transactionJsonString;
                Console.WriteLine(transactionJsonString);
            }
            else
            {
                Console.WriteLine(transactions[idInt]);
            }
            break;

        default:
            break;
    }
}
