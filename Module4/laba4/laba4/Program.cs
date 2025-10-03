using System;

public interface ITransport
{
    void Move();
    void FuelUp();
    string Model { get; set; }
    int Speed { get; set; }
}

public class Car : ITransport
{
    public string Model { get; set; }
    public int Speed { get; set; }

    public void Move()
    {
        Console.WriteLine($"Автомобиль {Model} едет со скоростью {Speed} км/ч.");
    }

    public void FuelUp()
    {
        Console.WriteLine($"Автомобиль {Model} заправляется бензином.");
    }
}

public class Motorcycle : ITransport
{
    public string Model { get; set; }
    public int Speed { get; set; }

    public void Move()
    {
        Console.WriteLine($"Мотоцикл {Model} мчится со скоростью {Speed} км/ч.");
    }

    public void FuelUp()
    {
        Console.WriteLine($"Мотоцикл {Model} заправляется бензином.");
    }
}

public class Plane : ITransport
{
    public string Model { get; set; }
    public int Speed { get; set; }

    public void Move()
    {
        Console.WriteLine($"Самолет {Model} летит со скоростью {Speed} км/ч.");
    }

    public void FuelUp()
    {
        Console.WriteLine($"Самолет {Model} заправляется авиационным керосином.");
    }
}

public class Bicycle : ITransport
{
    public string Model { get; set; }
    public int Speed { get; set; }

    public void Move()
    {
        Console.WriteLine($"Велосипед {Model} едет со скоростью {Speed} км/ч.");
    }

    public void FuelUp()
    {
        Console.WriteLine($"Велосипед {Model} не требует топлива — нужна только сила ног!");
    }
}

public abstract class TransportFactory
{
    public abstract ITransport CreateTransport(string model, int speed);
}

public class CarFactory : TransportFactory
{
    public override ITransport CreateTransport(string model, int speed)
    {
        return new Car { Model = model, Speed = speed };
    }
}

public class MotorcycleFactory : TransportFactory
{
    public override ITransport CreateTransport(string model, int speed)
    {
        return new Motorcycle { Model = model, Speed = speed };
    }
}

public class PlaneFactory : TransportFactory
{
    public override ITransport CreateTransport(string model, int speed)
    {
        return new Plane { Model = model, Speed = speed };
    }
}

public class BicycleFactory : TransportFactory
{
    public override ITransport CreateTransport(string model, int speed)
    {
        return new Bicycle { Model = model, Speed = speed };
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите транспорт (car, motorcycle, plane, bicycle):");
        string choice = Console.ReadLine()?.ToLower();

        Console.WriteLine("Введите модель:");
        string model = Console.ReadLine();

        Console.WriteLine("Введите скорость:");
        int speed = int.Parse(Console.ReadLine() ?? "0");

        TransportFactory factory = choice switch
        {
            "car" => new CarFactory(),
            "motorcycle" => new MotorcycleFactory(),
            "plane" => new PlaneFactory(),
            "bicycle" => new BicycleFactory(),
            _ => throw new ArgumentException("Неизвестный транспорт")
        };

        ITransport transport = factory.CreateTransport(model, speed);

        transport.Move();
        transport.FuelUp();
    }
}
