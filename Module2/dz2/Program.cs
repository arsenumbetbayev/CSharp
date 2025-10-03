using System;

public class Logger
{
    public void Log(string level, string message)
    {
        Console.WriteLine($"{level}: {message}");
    }
}

public static class Config
{
    public static string ConnectionString = "Server=myServer;Database=myDb;User Id=myUser;Password=myPass;";
}

public class DatabaseService
{
    public void Connect()
    {
        Console.WriteLine($"Connecting: {Config.ConnectionString}");
    }
}

public class LoggingService
{
    public void Log(string message)
    {
        Console.WriteLine($"Log to DB ({Config.ConnectionString}): {message}");
    }
}

public class NumberProcessor
{
    public void ProcessNumbers(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0) return;
        foreach (var number in numbers)
            if (number > 0) Console.WriteLine(number);
    }

    public void PrintPositiveNumbers(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0) return;
        foreach (var number in numbers)
            if (number > 0) Console.WriteLine(number);
    }

    public int Divide(int a, int b)
    {
        if (b == 0) return 0;
        return a / b;
    }
}

public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class FileReader
{
    public string ReadFile(string filePath)
    {
        return "file content";
    }
}

public class ReportGenerator
{
    public void GenerateReport(string format)
    {
        Console.WriteLine($"{format} report generated");
    }
}

public class Program
{
    public static void Main()
    {
        var logger = new Logger();
        logger.Log("INFO", "Program started");

        var db = new DatabaseService();
        db.Connect();

        var logService = new LoggingService();
        logService.Log("Some message");

        var processor = new NumberProcessor();
        processor.ProcessNumbers(new[] { -1, 0, 2, 4 });
        processor.PrintPositiveNumbers(new[] { -3, 5, 6 });
        Console.WriteLine(processor.Divide(10, 2));
        Console.WriteLine(processor.Divide(5, 0));

        var fileReader = new FileReader();
        Console.WriteLine(fileReader.ReadFile("file.txt"));

        var generator = new ReportGenerator();
        generator.GenerateReport("PDF");
    }
}
