using System;

namespace BankingSystem
{
    public interface IAccount
    {
        string OwnerName { get; set; }
        int Balance { get; set; }
        void Withdraw(int amount);
    }

    public interface ITransfer
    {
        int DailyLimit { get; set; }
        void TransferTo(IAccount target, int amount);
    }

    public class SavingsAccount : IAccount
    {
        private string ownerName;
        private int balance;

        public string OwnerName
        {
            get { return ownerName; }
            set { ownerName = value; }
        }

        public int Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public SavingsAccount(string ownerName, int initialBalance)
        {
            OwnerName = ownerName;
            Balance = initialBalance;
        }

        public void Withdraw(int amount)
        {
            if (amount > Balance)
            {
                Console.WriteLine($"[{OwnerName}] Попытка снять {amount}, но доступно только {Balance}. Списываем всё.");
                Balance = 0;
            }
            else
            {
                Balance -= amount;
                Console.WriteLine($"[{OwnerName}] Снято {amount}. Остаток: {Balance}");
            }
        }

        public override string ToString()
        {
            return $"SavingsAccount: Владелец = {OwnerName}, Баланс = {Balance}";
        }
    }

    public class PremiumAccount : IAccount, ITransfer
    {
        private string ownerName;
        private int balance;
        private int dailyLimit;

        public string OwnerName
        {
            get { return ownerName; }
            set { ownerName = value; }
        }

        public int Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public int DailyLimit
        {
            get { return dailyLimit; }
            set { dailyLimit = value; }
        }

        public PremiumAccount(string ownerName, int initialBalance, int dailyLimit)
        {
            OwnerName = ownerName;
            Balance = initialBalance;
            DailyLimit = dailyLimit;
        }

        public void Withdraw(int amount)
        {
            if (amount > DailyLimit)
            {
                Console.WriteLine($"[{OwnerName}] Попытка снять {amount}, но дневной лимит = {DailyLimit}. Списываем только лимит.");
                amount = DailyLimit;
            }

            if (amount > Balance)
            {
                Console.WriteLine($"[{OwnerName}] Попытка снять {amount}, но доступно только {Balance}. Списываем всё.");
                Balance = 0;
            }
            else
            {
                Balance -= amount;
                Console.WriteLine($"[{OwnerName}] Снято {amount}. Остаток: {Balance}");
            }
        }

        public void TransferTo(IAccount target, int amount)
        {
            Console.WriteLine($"\n[ПЕРЕВОД] {OwnerName} -> {target.OwnerName}, сумма: {amount}");

            int balanceBefore = Balance;

            Withdraw(amount);

            int actuallyWithdrawn = balanceBefore - Balance;

            if (actuallyWithdrawn > 0)
            {
                target.Balance += actuallyWithdrawn;
                Console.WriteLine($"[{target.OwnerName}] Получено {actuallyWithdrawn}. Новый баланс: {target.Balance}");
            }
            else
            {
                Console.WriteLine($"[ПЕРЕВОД ОТМЕНЁН] Не удалось списать средства со счёта {OwnerName}");
            }
        }

        public override string ToString()
        {
            return $"PremiumAccount: Владелец = {OwnerName}, Баланс = {Balance}, Дневной лимит = {DailyLimit}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== СОЗДАНИЕ СЧЕТОВ ===\n");

            SavingsAccount savingsAccount = new SavingsAccount("Иван Иванов", 5000);
            Console.WriteLine(savingsAccount);

            PremiumAccount premiumAccount = new PremiumAccount("Мария Петрова", 10000, 3000);
            Console.WriteLine(premiumAccount);

            Console.WriteLine("\n=== ТЕСТ 1: Обычное снятие с SavingsAccount ===\n");
            savingsAccount.Withdraw(2000);
            Console.WriteLine(savingsAccount);

            Console.WriteLine("\n=== ТЕСТ 2: Снятие с превышением баланса (SavingsAccount) ===\n");
            savingsAccount.Withdraw(5000);
            Console.WriteLine(savingsAccount);

            Console.WriteLine("\n=== ТЕСТ 3: Пополнение счетов ===\n");
            savingsAccount.Balance = 8000;
            premiumAccount.Balance = 10000;
            Console.WriteLine("Счета пополнены:");
            Console.WriteLine(savingsAccount);
            Console.WriteLine(premiumAccount);

            Console.WriteLine("\n=== ТЕСТ 4: Снятие в пределах лимита (PremiumAccount) ===\n");
            premiumAccount.Withdraw(2000);
            Console.WriteLine(premiumAccount);

            Console.WriteLine("\n=== ТЕСТ 5: Снятие с превышением лимита (PremiumAccount) ===\n");
            premiumAccount.Withdraw(5000);
            Console.WriteLine(premiumAccount);

            Console.WriteLine("\n=== ТЕСТ 6: Восстановление балансов ===\n");
            savingsAccount.Balance = 5000;
            premiumAccount.Balance = 10000;
            Console.WriteLine(savingsAccount);
            Console.WriteLine(premiumAccount);

            Console.WriteLine("\n=== ТЕСТ 7: Перевод в пределах лимита ===\n");
            premiumAccount.TransferTo(savingsAccount, 2500);
            Console.WriteLine("\nРезультат перевода:");
            Console.WriteLine(savingsAccount);
            Console.WriteLine(premiumAccount);

            Console.WriteLine("\n=== ТЕСТ 8: Перевод с превышением лимита ===\n");
            premiumAccount.TransferTo(savingsAccount, 5000);
            Console.WriteLine("\nРезультат перевода:");
            Console.WriteLine(savingsAccount);
            Console.WriteLine(premiumAccount);

            Console.WriteLine("\n=== ТЕСТ 9: Перевод с превышением баланса ===\n");
            premiumAccount.Balance = 1000;
            premiumAccount.TransferTo(savingsAccount, 2000);
            Console.WriteLine("\nРезультат перевода:");
            Console.WriteLine(savingsAccount);
            Console.WriteLine(premiumAccount);

            Console.WriteLine("\n=== ТЕСТ 10: Цепочка переводов ===\n");
            PremiumAccount premiumAccount2 = new PremiumAccount("Пётр Сидоров", 15000, 4000);
            Console.WriteLine("Создан третий счёт:");
            Console.WriteLine(premiumAccount2);

            Console.WriteLine("\nЦепочка переводов:");
            premiumAccount2.TransferTo(premiumAccount, 3500);
            premiumAccount.TransferTo(savingsAccount, 2000);

            Console.WriteLine("\nИтоговые балансы:");
            Console.WriteLine(savingsAccount);
            Console.WriteLine(premiumAccount);
            Console.WriteLine(premiumAccount2);

            Console.ReadKey();
        }
    }
}