using System;

// Интерфейс для управления банковским счетом
public interface IBankAccount
{
    void Deposit(decimal amount);   // Внести деньги
    void Withdraw(decimal amount);  // Снять деньги
    decimal GetBalance();           // Получить баланс
}

// Делегат для обработки случаев превышения остатка средств на счете
public delegate void OverdraftEventHandler(decimal overdraftAmount);

// Делегат для обработки событий изменения баланса
public delegate void BalanceChangedEventHandler(decimal newBalance);

// Класс, реализующий интерфейс IBankAccount
public class BankAccount : IBankAccount
{
    private decimal balance;  // Баланс счета

    // Событие, возникающее при превышении остатка средств на счете
    public event OverdraftEventHandler Overdraft;

    // Событие, возникающее при изменении баланса счета
    public event BalanceChangedEventHandler BalanceChanged;

    public void Deposit(decimal amount)
    {
        balance += amount;
        BalanceChanged?.Invoke(balance);  // Вызов события BalanceChanged
    }

    public void Withdraw(decimal amount)
    {
        if (amount > balance)
        {
            decimal overdraft = amount - balance;
            Overdraft?.Invoke(overdraft);  // Вызов события Overdraft
            balance = 0;
        }
        else
        {
            balance -= amount;
        }
        BalanceChanged?.Invoke(balance);  // Вызов события BalanceChanged
    }

    public decimal GetBalance()
    {
        return balance;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        BankAccount account = new BankAccount();

        // Подписка на события Overdraft и BalanceChanged
        account.Overdraft += OverdraftHandler;
        account.BalanceChanged += BalanceChangedHandler;

        account.Deposit(1259);
        account.Withdraw(700);
        account.Withdraw(950);

        Console.ReadLine();
    }

    // Обработчик события Overdraft
    static void OverdraftHandler(decimal overdraftAmount)
    {
        Console.WriteLine($"Превышение остатка средств на счете: {overdraftAmount}");
    }

    // Обработчик события BalanceChanged
    static void BalanceChangedHandler(decimal newBalance)
    {
        Console.WriteLine($"Новый баланс счета: {newBalance}");
    }
}