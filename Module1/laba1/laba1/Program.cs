using System;
using System.Collections.Generic;

public abstract class Employee
{
    public string Name { get; set; }
    public int Id { get; set; }
    public string Position { get; set; }

    public Employee(string name, int id, string position)
    {
        Name = name;
        Id = id;
        Position = position;
    }

    public abstract int CalculateSalary();
}

public class Worker : Employee
{
    public int HourlyRate { get; set; }
    public int HoursWorked { get; set; }

    public Worker(string name, int id, string position, int hourlyRate, int hoursWorked)
        : base(name, id, position)
    {
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
    }

    public override int CalculateSalary()
    {
        return HourlyRate * HoursWorked;
    }
}

public class Manager : Employee
{
    public int Salary { get; set; }
    public int Premium { get; set; }

    public Manager(string name, int id, string position, int salary, int premium)
        : base(name, id, position)
    {
        Salary = salary;
        Premium = premium;
    }

    public override int CalculateSalary()
    {
        return Salary + Premium;
    }
}

public class Program
{
    public static void Main()
    {
        List<Employee> employees = new List<Employee>
        {
            new Worker("Ivan", 1, "Worker", 500, 160),
            new Worker("Anna", 2, "Worker", 600, 120),
            new Manager("Oleg", 3, "Manager", 50000, 10000)
        };

        foreach (var emp in employees)
        {
            Console.WriteLine($"{emp.Position} {emp.Name} (ID: {emp.Id}) - Salary: {emp.CalculateSalary()}");
        }
    }
}
