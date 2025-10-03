using System;

public class Order
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
}

public class PriceCalculator
{
    public double CalculateTotalPrice(Order order)
    {
        return order.Quantity * order.Price * 0.9;
    }
}

public class PaymentProcessor
{
    public void ProcessPayment(string paymentDetails)
    {
        Console.WriteLine("Payment processed using: " + paymentDetails);
    }
}

public class NotificationServiceSRP
{
    public void SendConfirmationEmail(string email)
    {
        Console.WriteLine("Confirmation email sent to: " + email);
    }
}

public abstract class Employee
{
    public string Name { get; set; }
    public double BaseSalary { get; set; }
    public abstract double CalculateSalary();
}

public class PermanentEmployee : Employee
{
    public override double CalculateSalary()
    {
        return BaseSalary * 1.2;
    }
}

public class ContractEmployee : Employee
{
    public override double CalculateSalary()
    {
        return BaseSalary * 1.1;
    }
}

public class Intern : Employee
{
    public override double CalculateSalary()
    {
        return BaseSalary * 0.8;
    }
}

public class Freelancer : Employee
{
    public override double CalculateSalary()
    {
        return BaseSalary * 1.05;
    }
}

public interface IPrint
{
    void Print(string content);
}

public interface IScan
{
    void Scan(string content);
}

public interface IFax
{
    void Fax(string content);
}

public class AllInOnePrinter : IPrint, IScan, IFax
{
    public void Print(string content)
    {
        Console.WriteLine("Printing: " + content);
    }

    public void Scan(string content)
    {
        Console.WriteLine("Scanning: " + content);
    }

    public void Fax(string content)
    {
        Console.WriteLine("Faxing: " + content);
    }
}

public class BasicPrinter : IPrint
{
    public void Print(string content)
    {
        Console.WriteLine("Printing: " + content);
    }
}

public interface IMessageSender
{
    void Send(string message);
}

public class EmailSender : IMessageSender
{
    public void Send(string message)
    {
        Console.WriteLine("Email sent: " + message);
    }
}

public class SmsSender : IMessageSender
{
    public void Send(string message)
    {
        Console.WriteLine("SMS sent: " + message);
    }
}

public class NotificationService
{
    private readonly IMessageSender messageSender;

    public NotificationService(IMessageSender sender)
    {
        messageSender = sender;
    }

    public void SendNotification(string message)
    {
        messageSender.Send(message);
    }
}

public class Program
{
    public static void Main()
    {
        Order order = new Order { ProductName = "Laptop", Quantity = 2, Price = 1000 };
        PriceCalculator calculator = new PriceCalculator();
        Console.WriteLine("Total price: " + calculator.CalculateTotalPrice(order));

        PaymentProcessor payment = new PaymentProcessor();
        payment.ProcessPayment("Credit Card");

        NotificationServiceSRP notifier = new NotificationServiceSRP();
        notifier.SendConfirmationEmail("user@example.com");

        Employee emp1 = new PermanentEmployee { Name = "John", BaseSalary = 1000 };
        Console.WriteLine(emp1.Name + " Salary: " + emp1.CalculateSalary());

        IPrint printer = new BasicPrinter();
        printer.Print("Test Document");

        NotificationService emailNotification = new NotificationService(new EmailSender());
        emailNotification.SendNotification("Hello via Email");

        NotificationService smsNotification = new NotificationService(new SmsSender());
        smsNotification.SendNotification("Hello via SMS");
    }
}

