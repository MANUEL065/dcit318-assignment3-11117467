using System;
using System.Collections.Generic;

namespace FinanceManagement
{
    // Interface
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }

    // Processors
    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[BankTransfer] Processing transfer of {transaction.Amount:C} for '{transaction.Category}' on {transaction.Date:d}.");
        }
    }

    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[MobileMoney] Processing mobile payment of {transaction.Amount:C} for '{transaction.Category}' on {transaction.Date:d}.");
        }
    }

    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[CryptoWallet] Processing crypto payment of {transaction.Amount:C} for '{transaction.Category}' on {transaction.Date:d}.");
        }
    }

    // Finance Application
    public class FinanceApp
    {
        private readonly List<Transaction> _transactions = new();

        public void Run()
        {
            var account = new SavingsAccount("SB-560096910001", 1000m);
            Console.WriteLine($"Created SavingsAccount {account.AccountNumber} with balance {account.Balance:C}\n");

            var t1 = new Transaction(1, DateTime.Now.Date, 150.75m, "Groceries" + " " + "Completed!");
            var t2 = new Transaction(2, DateTime.Now.Date, 250.00m, "Utilities" + " " + "Completed!");
            var t3 = new Transaction(3, DateTime.Now.Date, 900.00m, "Entertainment" + " " + "(Failed)"); // Will trigger insufficient funds

            ITransactionProcessor p1 = new MobileMoneyProcessor();
            ITransactionProcessor p2 = new BankTransferProcessor();
            ITransactionProcessor p3 = new CryptoWalletProcessor();

            p1.Process(t1);
            account.ApplyTransaction(t1);
            _transactions.Add(t1);
            Console.WriteLine();

            p2.Process(t2);
            account.ApplyTransaction(t2);
            _transactions.Add(t2);
            Console.WriteLine();

            p3.Process(t3);
            account.ApplyTransaction(t3);
            _transactions.Add(t3);
            Console.WriteLine();

            Console.WriteLine("Transaction history:");
            foreach (var tx in _transactions)
            {
                Console.WriteLine($"  Id:{tx.Id} Date:{tx.Date:d} Amount:{tx.Amount:C} Category:{tx.Category}");
            }

            Console.WriteLine($"\nFinal balance for account {account.AccountNumber}: {account.Balance:C}");
        }
    }
}
