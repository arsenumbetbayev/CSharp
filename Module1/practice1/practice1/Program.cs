using System;
using System.Collections.Generic;

class Vehicle
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    public Vehicle(string brand, string model, int year)
    {
        Brand = brand;
        Model = model;
        Year = year;
    }

    public virtual void StartEngine()
    {
        Console.WriteLine($"{Brand} {Model} engine started");
    }

    public virtual void StopEngine()
    {
        Console.WriteLine($"{Brand} {Model} engine stopped");
    }
}

class Car : Vehicle
{
    public int Doors { get; set; }
    public string Transmission { get; set; }

    public Car(string brand, string model, int year, int doors, string transmission)
        : base(brand, model, year)
    {
        Doors = doors;
        Transmission = transmission;
    }
}

class Motorcycle : Vehicle
{
    public string BodyType { get; set; }
    public bool HasBox { get; set; }

    public Motorcycle(string brand, string model, int year, string bodyType, bool hasBox)
        : base(brand, model, year)
    {
        BodyType = bodyType;
        HasBox = hasBox;
    }
}

class Garage
{
    public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

    public void AddVehicle(Vehicle vehicle)
    {
        Vehicles.Add(vehicle);
    }

    public void RemoveVehicle(Vehicle vehicle)
    {
        Vehicles.Remove(vehicle);
    }
}

class Fleet
{
    public List<Garage> Garages { get; set; } = new List<Garage>();

    public void AddGarage(Garage garage)
    {
        Garages.Add(garage);
    }

    public void RemoveGarage(Garage garage)
    {
        Garages.Remove(garage);
    }

    public List<Vehicle> FindVehicles(string brand)
    {
        List<Vehicle> result = new List<Vehicle>();
        foreach (var garage in Garages)
        {
            foreach (var vehicle in garage.Vehicles)
            {
                if (vehicle.Brand == brand)
                    result.Add(vehicle);
            }
        }
        return result;
    }
}

class Program
{
    static void Main()
    {
        Car car1 = new Car("Toyota", "Camry", 2020, 4, "Automatic");
        Car car2 = new Car("BMW", "X5", 2021, 4, "Manual");
        Motorcycle moto1 = new Motorcycle("Yamaha", "R1", 2019, "Sport", false);
        Motorcycle moto2 = new Motorcycle("Honda", "Goldwing", 2022, "Touring", true);

        Garage garage1 = new Garage();
        Garage garage2 = new Garage();

        garage1.AddVehicle(car1);
        garage1.AddVehicle(moto1);
        garage2.AddVehicle(car2);
        garage2.AddVehicle(moto2);

        Fleet fleet = new Fleet();
        fleet.AddGarage(garage1);
        fleet.AddGarage(garage2);

        car1.StartEngine();
        moto1.StartEngine();
        car1.StopEngine();
        moto1.StopEngine();

        var found = fleet.FindVehicles("BMW");
        foreach (var v in found)
            Console.WriteLine($"Found: {v.Brand} {v.Model}");

        garage1.RemoveVehicle(moto1);
        fleet.RemoveGarage(garage2);
    }
}
