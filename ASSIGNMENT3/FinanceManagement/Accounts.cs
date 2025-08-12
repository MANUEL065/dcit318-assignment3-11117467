using System;

namespace FinanceManagement
{
    // Record for transactions
    public record Transaction(int Id, DateTime Date, decimal Amount, string Category);

    // Base Account class
    public class Account
    {
        public string AccountNumber { get; }
        public decimal Balance { get; protected set; }

        public Account(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public virtual void ApplyTransaction(Transaction transaction)
        {
            Balance -= transaction.Amount;
            Console.WriteLine($"[Account] Applied transaction {transaction.Id}: -{transaction.Amount:C}. New balance: {Balance:C}");
        }
    }

    // Sealed SavingsAccount class
    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance) { }

        public override void ApplyTransaction(Transaction transaction)
        {
            if (transaction.Amount > Balance)
            {
                Console.WriteLine($"[SavingsAccount] Insufficient funds for transaction {transaction.Id} " +
                                  $"(amount: {transaction.Amount:C}, balance: {Balance:C}).");
                return;
            }

            Balance -= transaction.Amount;
            Console.WriteLine($"[SavingsAccount] Transaction {transaction.Id} applied: -{transaction.Amount:C}. Updated balance: {Balance:C}");
        }
    }
}

