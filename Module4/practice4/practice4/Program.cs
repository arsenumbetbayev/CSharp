using System;

abstract class Document
{
    public abstract void Open();
}

class Report : Document
{
    public override void Open()
    {
        Console.WriteLine("Открыт отчет");
    }
}

class Resume : Document
{
    public override void Open()
    {
        Console.WriteLine("Открыто резюме");
    }
}

class Letter : Document
{
    public override void Open()
    {
        Console.WriteLine("Открыто письмо");
    }
}

class Invoice : Document
{
    public override void Open()
    {
        Console.WriteLine("Открыт счет-фактура");
    }
}

abstract class DocumentCreator
{
    public abstract Document CreateDocument();
}

class ReportCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Report();
    }
}

class ResumeCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Resume();
    }
}

class LetterCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Letter();
    }
}

class InvoiceCreator : DocumentCreator
{
    public override Document CreateDocument()
    {
        return new Invoice();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите тип документа (report, resume, letter, invoice):");
        string input = Console.ReadLine()?.ToLower();

        DocumentCreator creator = null;

        switch (input)
        {
            case "report":
                creator = new ReportCreator();
                break;
            case "resume":
                creator = new ResumeCreator();
                break;
            case "letter":
                creator = new LetterCreator();
                break;
            case "invoice":
                creator = new InvoiceCreator();
                break;
            default:
                Console.WriteLine("Неизвестный тип документа");
                return;
        }

        Document doc = creator.CreateDocument();
        doc.Open();
    }
}
