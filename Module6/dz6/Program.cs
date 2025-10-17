// 1 Задание:
using System;


public interface IPaymentStrategy
{
    void Pay(decimal amount);
}



public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Оплата {amount} KZT произведена банковской картой.");
    }
}

public class PayPalPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Оплата {amount} KZT произведена через PayPal.");
    }
}

public class CryptoPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Оплата {amount} KZT произведена с помощью криптовалюты (BTC).");
    }
}


public class PaymentContext
{
    private IPaymentStrategy _paymentStrategy;

    public void SetPaymentStrategy(IPaymentStrategy strategy)
    {
        _paymentStrategy = strategy;
    }

    public void ProcessPayment(decimal amount)
    {
        if (_paymentStrategy == null)
        {
            Console.WriteLine("Ошибка: стратегия оплаты не выбрана!");
            return;
        }

        _paymentStrategy.Pay(amount);
    }
}


public class Program
{
    public static void Main()
    {
        PaymentContext context = new PaymentContext();

        Console.WriteLine("Выберите способ оплаты:");
        Console.WriteLine("1 - Банковская карта");
        Console.WriteLine("2 - PayPal");
        Console.WriteLine("3 - Криптовалюта");

        string choice = Console.ReadLine();
        decimal amount = 12500.75m;

        switch (choice)
        {
            case "1":
                context.SetPaymentStrategy(new CreditCardPayment());
                break;
            case "2":
                context.SetPaymentStrategy(new PayPalPayment());
                break;
            case "3":
                context.SetPaymentStrategy(new CryptoPayment());
                break;
            default:
                Console.WriteLine("Неверный выбор. Оплата не произведена.");
                return;
        }

        context.ProcessPayment(amount);
    }
}


// 2 Задание:
using System;
using System.Collections.Generic;


public interface IObserver
{
    void Update(string currency, decimal rate);
}


public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify();
}


public class CurrencyExchange : ISubject
{
    private List<IObserver> _observers = new List<IObserver>();
    private string _currency;
    private decimal _rate;

    public string Currency => _currency;
    public decimal Rate => _rate;

    public void SetRate(string currency, decimal rate)
    {
        _currency = currency;
        _rate = rate;
        Notify(); 
    }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
        Console.WriteLine($"{observer.GetType().Name} подписан на обновления.");
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
        Console.WriteLine($"{observer.GetType().Name} отписан от обновлений.");
    }

    public void Notify()
    {
        Console.WriteLine("\nУведомление наблюдателей о новом курсе...");
        foreach (var observer in _observers)
        {
            observer.Update(_currency, _rate);
        }
    }
}


public class MobileAppObserver : IObserver
{
    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"📱 [Мобильное приложение] Новый курс {currency}: {rate}");
    }
}

public class WebSiteObserver : IObserver
{
    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"🌐 [Веб-сайт] Обновление: {currency} теперь стоит {rate}");
    }
}

public class TradingBotObserver : IObserver
{
    public void Update(string currency, decimal rate)
    {
        if (rate < 440m)
            Console.WriteLine($"🤖 [Трейдинг-бот] Курс {currency} упал до {rate}! Покупаем!");
        else
            Console.WriteLine($"🤖 [Трейдинг-бот] Курс {currency} = {rate}. Ожидаем.");
    }
}


public class Program
{
    public static void Main()
    {
        CurrencyExchange exchange = new CurrencyExchange();

        IObserver mobile = new MobileAppObserver();
        IObserver web = new WebSiteObserver();
        IObserver bot = new TradingBotObserver();

        exchange.Attach(mobile);
        exchange.Attach(web);
        exchange.Attach(bot);

        exchange.SetRate("USD/KZT", 443.5m);
        exchange.SetRate("USD/KZT", 438.2m);

        Console.WriteLine("\nОтписываем веб-сайт от уведомлений...\n");
        exchange.Detach(web);

        exchange.SetRate("USD/KZT", 445.1m);
    }
}
