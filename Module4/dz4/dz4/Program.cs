using System;

public interface IVehicle
{
    void Drive();
    void Refuel();
}

public class Car : IVehicle
{
    private string brand;
    private string model;
    private string fuelType;

    public Car(string brand, string model, string fuelType)
    {
        this.brand = brand;
        this.model = model;
        this.fuelType = fuelType;
    }

    public void Drive()
    {
        Console.WriteLine($"{brand} {model} is driving.");
    }

    public void Refuel()
    {
        Console.WriteLine($"{brand} {model} refueled with {fuelType}.");
    }
}

public class Motorcycle : IVehicle
{
    private string type;
    private int engineVolume;

    public Motorcycle(string type, int engineVolume)
    {
        this.type = type;
        this.engineVolume = engineVolume;
    }

    public void Drive()
    {
        Console.WriteLine($"{type} motorcycle with {engineVolume}cc engine is driving.");
    }

    public void Refuel()
    {
        Console.WriteLine($"{type} motorcycle refueled.");
    }
}

public class Truck : IVehicle
{
    private double capacity;
    private int axles;

    public Truck(double capacity, int axles)
    {
        this.capacity = capacity;
        this.axles = axles;
    }

    public void Drive()
    {
        Console.WriteLine($"Truck with {axles} axles and {capacity} tons capacity is driving.");
    }

    public void Refuel()
    {
        Console.WriteLine($"Truck with {axles} axles refueled.");
    }
}

public class Bus : IVehicle
{
    private int seats;
    private string route;

    public Bus(int seats, string route)
    {
        this.seats = seats;
        this.route = route;
    }

    public void Drive()
    {
        Console.WriteLine($"Bus with {seats} seats is driving on route {route}.");
    }

    public void Refuel()
    {
        Console.WriteLine($"Bus on route {route} refueled.");
    }
}

public class Scooter : IVehicle
{
    private string model;
    private bool electric;

    public Scooter(string model, bool electric)
    {
        this.model = model;
        this.electric = electric;
    }

    public void Drive()
    {
        Console.WriteLine($"{(electric ? "Electric" : "Gas")} scooter {model} is driving.");
    }

    public void Refuel()
    {
        Console.WriteLine($"{(electric ? "Electric scooter" : "Gas scooter")} {model} charged/refueled.");
    }
}

public abstract class VehicleFactory
{
    public abstract IVehicle CreateVehicle();
}

public class CarFactory : VehicleFactory
{
    private string brand;
    private string model;
    private string fuelType;

    public CarFactory(string brand, string model, string fuelType)
    {
        this.brand = brand;
        this.model = model;
        this.fuelType = fuelType;
    }

    public override IVehicle CreateVehicle()
    {
        return new Car(brand, model, fuelType);
    }
}

public class MotorcycleFactory : VehicleFactory
{
    private string type;
    private int engineVolume;

    public MotorcycleFactory(string type, int engineVolume)
    {
        this.type = type;
        this.engineVolume = engineVolume;
    }

    public override IVehicle CreateVehicle()
    {
        return new Motorcycle(type, engineVolume);
    }
}

public class TruckFactory : VehicleFactory
{
    private double capacity;
    private int axles;

    public TruckFactory(double capacity, int axles)
    {
        this.capacity = capacity;
        this.axles = axles;
    }

    public override IVehicle CreateVehicle()
    {
        return new Truck(capacity, axles);
    }
}

public class BusFactory : VehicleFactory
{
    private int seats;
    private string route;

    public BusFactory(int seats, string route)
    {
        this.seats = seats;
        this.route = route;
    }

    public override IVehicle CreateVehicle()
    {
        return new Bus(seats, route);
    }
}

public class ScooterFactory : VehicleFactory
{
    private string model;
    private bool electric;

    public ScooterFactory(string model, bool electric)
    {
        this.model = model;
        this.electric = electric;
    }

    public override IVehicle CreateVehicle()
    {
        return new Scooter(model, electric);
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Choose vehicle type: Car, Motorcycle, Truck, Bus, Scooter");
        string choice = Console.ReadLine();

        VehicleFactory factory = null;

        switch (choice.ToLower())
        {
            case "car":
                Console.WriteLine("Enter brand:");
                string brand = Console.ReadLine();
                Console.WriteLine("Enter model:");
                string model = Console.ReadLine();
                Console.WriteLine("Enter fuel type:");
                string fuel = Console.ReadLine();
                factory = new CarFactory(brand, model, fuel);
                break;
            case "motorcycle":
                Console.WriteLine("Enter type (sport, touring, etc):");
                string type = Console.ReadLine();
                Console.WriteLine("Enter engine volume:");
                int volume = int.Parse(Console.ReadLine());
                factory = new MotorcycleFactory(type, volume);
                break;
            case "truck":
                Console.WriteLine("Enter capacity (tons):");
                double capacity = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter axles:");
                int axles = int.Parse(Console.ReadLine());
                factory = new TruckFactory(capacity, axles);
                break;
            case "bus":
                Console.WriteLine("Enter seats:");
                int seats = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter route:");
                string route = Console.ReadLine();
                factory = new BusFactory(seats, route);
                break;
            case "scooter":
                Console.WriteLine("Enter model:");
                string smodel = Console.ReadLine();
                Console.WriteLine("Is it electric? (yes/no):");
                bool electric = Console.ReadLine().ToLower() == "yes";
                factory = new ScooterFactory(smodel, electric);
                break;
            default:
                Console.WriteLine("Unknown vehicle type");
                return;
        }

        IVehicle vehicle = factory.CreateVehicle();
        vehicle.Drive();
        vehicle.Refuel();
    }
}
