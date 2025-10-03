using System;

// DRY
public class OrderService
{
    private void ProcessOrder(string action, string productName, int quantity, double price)
    {
        double totalPrice = quantity * price;
        Console.WriteLine($"Order for {productName} {action}. Total: {totalPrice}");
    }

    public void CreateOrder(string productName, int quantity, double price)
    {
        ProcessOrder("created", productName, quantity, price);
    }

    public void UpdateOrder(string productName, int quantity, double price)
    {
        ProcessOrder("updated", productName, quantity, price);
    }
}

public class Vehicle
{
    public virtual void Start()
    {
        Console.WriteLine($"{GetType().Name} is starting");
    }

    public virtual void Stop()
    {
        Console.WriteLine($"{GetType().Name} is stopping");
    }
}

public class Car : Vehicle { }
public class Truck : Vehicle { }


// KISS
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}


public class Service
{
    public void DoSomething()
    {
        Console.WriteLine("Doing something...");
    }
}

public class Client
{
    private Service _service = new Service();

    public void Execute()
    {
        _service.DoSomething();
    }
}



// YAGNI
public class Circle
{
    private double _radius;

    public Circle(double radius)
    {
        _radius = radius;
    }

    public double CalculateArea()
    {
        return Math.PI * _radius * _radius;
    }
}

public class MathOperations
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

public class Program
{
    public static void Main()
    {
        var orderService = new OrderService();
        orderService.CreateOrder("Laptop", 2, 500);
        orderService.UpdateOrder("Laptop", 3, 450);

        Vehicle car = new Car();
        Vehicle truck = new Truck();
        car.Start();
        car.Stop();
        truck.Start();
        truck.Stop();

        var calculator = new Calculator();
        Console.WriteLine($"Sum: {calculator.Add(5, 7)}");
        var client = new Client();
        client.Execute();

        var circle = new Circle(5);
        Console.WriteLine($"Circle area: {circle.CalculateArea()}");

        var mathOps = new MathOperations();
        Console.WriteLine($"Addition: {mathOps.Add(10, 20)}");
        
    }
}
