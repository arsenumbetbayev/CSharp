using System;
using System.Collections.Generic;

// SRP
public class Item
{
    public string Name { get; set; }
    public double Price { get; set; }
}

public class Invoice
{
    public int Id { get; set; }
    public List<Item> Items { get; set; }
    public double TaxRate { get; set; }

    public Invoice(int id, List<Item> items, double taxRate)
    {
        Id = id;
        Items = items;
        TaxRate = taxRate;
    }
}

public class InvoiceCalculator
{
    public double CalculateTotal(Invoice invoice)
    {
        double subTotal = 0;
        foreach (var item in invoice.Items)
        {
            subTotal += item.Price;
        }
        return subTotal + (subTotal * invoice.TaxRate);
    }
}

public class InvoiceRepository
{
    public void SaveToDatabase(Invoice invoice)
    {
        Console.WriteLine($"Invoice {invoice.Id} saved to database.");
    }
}


// OCP
public interface IDiscountStrategy
{
    double ApplyDiscount(double amount);
}

public class RegularDiscount : IDiscountStrategy
{
    public double ApplyDiscount(double amount) => amount;
}

public class SilverDiscount : IDiscountStrategy
{
    public double ApplyDiscount(double amount) => amount * 0.9;
}

public class GoldDiscount : IDiscountStrategy
{
    public double ApplyDiscount(double amount) => amount * 0.8;
}

public class DiscountCalculator
{
    private readonly IDiscountStrategy _discountStrategy;

    public DiscountCalculator(IDiscountStrategy discountStrategy)
    {
        _discountStrategy = discountStrategy;
    }

    public double Calculate(double amount)
    {
        return _discountStrategy.ApplyDiscount(amount);
    }
}


// ISP
public interface IWorkable
{
    void Work();
}

public interface IEatable
{
    void Eat();
}

public interface ISleepable
{
    void Sleep();
}

public class HumanWorker : IWorkable, IEatable, ISleepable
{
    public void Work() { }
    public void Eat() { }
    public void Sleep() { }
}

public class RobotWorker : IWorkable
{
    public void Work() { }
}


// DIP
public interface IMessageService
{
    void SendMessage(string message);
}

public class EmailService : IMessageService
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Отправка Email: {message}");
    }
}

public class SmsService : IMessageService
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Отправка SMS: {message}");
    }
}

public class Notification
{
    private readonly IMessageService _messageService;

    public Notification(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public void Send(string message)
    {
        _messageService.SendMessage(message);
    }
}


public class Program
{
    public static void Main()
    {
        var items = new List<Item> { new Item { Name = "Book", Price = 100 } };
        var invoice = new Invoice(1, items, 0.2);
        var calculator = new InvoiceCalculator();
        Console.WriteLine($"Total: {calculator.CalculateTotal(invoice)}");
        var repo = new InvoiceRepository();
        repo.SaveToDatabase(invoice);

        var silverCalc = new DiscountCalculator(new SilverDiscount());
        Console.WriteLine($"Silver discount: {silverCalc.Calculate(1000)}");

        IWorkable human = new HumanWorker();
        human.Work();

        IWorkable robot = new RobotWorker();
        robot.Work();

        var notification = new Notification(new SmsService());
        notification.Send("Hello via SMS");
    }
}
