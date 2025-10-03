using System;
using System.Collections.Generic;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int Copies { get; set; }

    public Book(string title, string author, string isbn, int copies)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        Copies = copies;
    }

    public override string ToString()
    {
        return $"{Title} by {Author} (ISBN: {ISBN}, Copies: {Copies})";
    }
}

public class Reader
{
    public string Name { get; set; }
    public int ReaderId { get; set; }

    public Reader(string name, int readerId)
    {
        Name = name;
        ReaderId = readerId;
    }

    public override string ToString()
    {
        return $"Reader: {Name} (ID: {ReaderId})";
    }
}

public class Library
{
    private List<Book> books = new List<Book>();
    private List<Reader> readers = new List<Reader>();
    private Dictionary<int, List<string>> borrowedBooks = new Dictionary<int, List<string>>();

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"Added book: {book.Title}");
    }

    public void RemoveBook(string isbn)
    {
        var book = books.Find(b => b.ISBN == isbn);
        if (book != null)
        {
            books.Remove(book);
            Console.WriteLine($"Removed book: {book.Title}");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    public void RegisterReader(Reader reader)
    {
        readers.Add(reader);
        borrowedBooks[reader.ReaderId] = new List<string>();
        Console.WriteLine($"Registered reader: {reader.Name}");
    }

    public void RemoveReader(int readerId)
    {
        var reader = readers.Find(r => r.ReaderId == readerId);
        if (reader != null)
        {
            readers.Remove(reader);
            borrowedBooks.Remove(readerId);
            Console.WriteLine($"Removed reader: {reader.Name}");
        }
        else
        {
            Console.WriteLine("Reader not found.");
        }
    }

    public void BorrowBook(string isbn, int readerId)
    {
        var book = books.Find(b => b.ISBN == isbn);
        var reader = readers.Find(r => r.ReaderId == readerId);

        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        if (reader == null)
        {
            Console.WriteLine("Reader not found.");
            return;
        }

        if (book.Copies > 0)
        {
            book.Copies--;
            borrowedBooks[readerId].Add(isbn);
            Console.WriteLine($"{reader.Name} borrowed {book.Title}");
        }
        else
        {
            Console.WriteLine("No copies available.");
        }
    }

    public void ReturnBook(string isbn, int readerId)
    {
        var book = books.Find(b => b.ISBN == isbn);
        var reader = readers.Find(r => r.ReaderId == readerId);

        if (book == null || reader == null || !borrowedBooks[readerId].Contains(isbn))
        {
            Console.WriteLine("Invalid return attempt.");
            return;
        }

        book.Copies++;
        borrowedBooks[readerId].Remove(isbn);
        Console.WriteLine($"{reader.Name} returned {book.Title}");
    }

    public void ShowBooks()
    {
        Console.WriteLine("\nBooks in library:");
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }

    public void ShowReaders()
    {
        Console.WriteLine("\nReaders in library:");
        foreach (var reader in readers)
        {
            Console.WriteLine(reader);
        }
    }
}

public class Program
{
    static void Main()
    {
        Library library = new Library();


        library.AddBook(new Book("War and Peace", "Leo Tolstoy", "12345", 3));
        library.AddBook(new Book("Crime and Punishment", "Fyodor Dostoevsky", "67890", 2));


        library.RegisterReader(new Reader("Ivan", 1));
        library.RegisterReader(new Reader("Anna", 2));

        library.ShowBooks();
        library.ShowReaders();


        library.BorrowBook("12345", 1);
        library.BorrowBook("12345", 2);
        library.BorrowBook("12345", 2); // нет копий


        library.ReturnBook("12345", 1);


        library.RemoveBook("67890");

        library.ShowBooks();
        library.ShowReaders();
    }
}
