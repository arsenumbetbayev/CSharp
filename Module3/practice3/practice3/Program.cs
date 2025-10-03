using System;
using System.Collections.Generic;

// --- Абстракции ---

public interface IPayment
{
    void ProcessPayment(double amount);
}

public interface IDelivery
{
    void DeliverOrder(Order order);
}

public interface INotification
{
    void SendNotification(string message);
}

public interface IDiscount
{
    double ApplyDiscount(double totalAmount);
}

// --- Реализации оплаты ---

public class CreditCardPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Paid {amount} using Credit Card.");
    }
}

public class PayPalPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Paid {amount} using PayPal.");
    }
}

public class BankTransferPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Paid {amount} via Bank Transfer.");
    }
}

// --- Реализации доставки ---

public class CourierDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Order delivered by courier.");
    }
}

public class PostDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Order delivered by post.");
    }
}

public class PickUpPointDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Order delivered to pick-up point.");
    }
}

// --- Реализации уведомлений ---

public class EmailNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"Email notification sent: {message}");
    }
}

public class SmsNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"SMS notification sent: {message}");
    }
}

// --- Реализации скидок ---

public class PercentageDiscount : IDiscount
{
    private double percent;
    public PercentageDiscount(double percent)
    {
        this.percent = percent;
    }

    public double ApplyDiscount(double totalAmount)
    {
        return totalAmount - (totalAmount * percent / 100);
    }
}

public class FixedAmountDiscount : IDiscount
{
    private double discountAmount;
    public FixedAmountDiscount(double discountAmount)
    {
        this.discountAmount = discountAmount;
    }

    public double ApplyDiscount(double totalAmount)
    {
        return Math.Max(0, totalAmount - discountAmount);
    }
}

public class NoDiscount : IDiscount
{
    public double ApplyDiscount(double totalAmount) => totalAmount;
}

// --- Калькулятор скидок (OCP через стратегию) ---

public class DiscountCalculator
{
    private readonly IDiscount discount;
    public DiscountCalculator(IDiscount discount)
    {
        this.discount = discount;
    }

    public double Calculate(double totalAmount)
    {
        return discount.ApplyDiscount(totalAmount);
    }
}

// --- Класс товара ---

public class Product
{
    public string Name { get; }
    public double Price { get; }
    public int Quantity { get; }

    public Product(string name, double price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public double GetTotalPrice() => Price * Quantity;
}

// --- Класс заказа (SRP: хранение и работа с данными заказа) ---

public class Order
{
    private List<Product> products = new List<Product>();
    public IReadOnlyList<Product> Products => products.AsReadOnly();

    public IPayment PaymentMethod { get; set; }
    public IDelivery DeliveryMethod { get; set; }
    public INotification NotificationService { get; set; }
    public DiscountCalculator DiscountCalculator { get; set; }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public double GetTotalPrice()
    {
        double sum = 0;
        foreach (var p in products)
        {
            sum += p.GetTotalPrice();
        }
        return DiscountCalculator?.Calculate(sum) ?? sum;
    }

    public void ProcessOrder()
    {
        double total = GetTotalPrice();
        PaymentMethod?.ProcessPayment(total);
        DeliveryMethod?.DeliverOrder(this);
        NotificationService?.SendNotification($"Your order has been processed. Total: {total}");
    }
}

// --- Пример использования ---

public class Program
{
    public static void Main()
    {
        var order = new Order();
        order.AddProduct(new Product("Laptop", 1000, 1));
        order.AddProduct(new Product("Mouse", 50, 2));

        order.PaymentMethod = new CreditCardPayment();
        order.DeliveryMethod = new CourierDelivery();
        order.NotificationService = new EmailNotification();
        order.DiscountCalculator = new DiscountCalculator(new PercentageDiscount(10));

        order.ProcessOrder();
    }
}

