// 1 Задание:
// ShippingStrategyDemo.cs
using System;
using System.Globalization;

namespace ShippingStrategyDemo
{
    public interface IShippingStrategy
    {
        decimal CalculateShippingCost(decimal weight, decimal distance);
    }

    public class StandardShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingCost(decimal weight, decimal distance)
            => weight * 0.5m + distance * 0.1m;
    }

    public class ExpressShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingCost(decimal weight, decimal distance)
            => (weight * 0.75m + distance * 0.2m) + 10m;
    }

    public class InternationalShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingCost(decimal weight, decimal distance)
            => weight * 1.0m + distance * 0.5m + 15m;
    }

    // Ночная доставка — фиксированная надбавка за срочность
    public class NightShippingStrategy : IShippingStrategy
    {
        private readonly decimal _surcharge;
        public NightShippingStrategy(decimal surcharge = 20m) => _surcharge = surcharge;

        public decimal CalculateShippingCost(decimal weight, decimal distance)
        {
            // Базовая формула + ночная надбавка
            decimal baseCost = weight * 0.6m + distance * 0.12m;
            return baseCost + _surcharge;
        }
    }

    public class DeliveryContext
    {
        private IShippingStrategy _strategy;
        public void SetShippingStrategy(IShippingStrategy strategy) => _strategy = strategy;

        public decimal CalculateCost(decimal weight, decimal distance)
        {
            if (_strategy == null) throw new InvalidOperationException("Стратегия доставки не установлена.");
            if (weight < 0) throw new ArgumentException("Вес не может быть отрицательным.", nameof(weight));
            if (distance < 0) throw new ArgumentException("Расстояние не может быть отрицательным.", nameof(distance));
            return _strategy.CalculateShippingCost(weight, distance);
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Shipping Strategy Demo ===");
            var ctx = new DeliveryContext();

            Console.WriteLine("Выберите стратегию доставки:");
            Console.WriteLine("1 - Стандартная");
            Console.WriteLine("2 - Экспресс");
            Console.WriteLine("3 - Международная");
            Console.WriteLine("4 - Ночная");
            Console.Write("Выбор: ");
            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1": ctx.SetShippingStrategy(new StandardShippingStrategy()); break;
                    case "2": ctx.SetShippingStrategy(new ExpressShippingStrategy()); break;
                    case "3": ctx.SetShippingStrategy(new InternationalShippingStrategy()); break;
                    case "4":
                        Console.Write("Введите фиксированную надбавку за ночную доставку (Enter = 20): ");
                        var s = Console.ReadLine();
                        if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal surcharge))
                            ctx.SetShippingStrategy(new NightShippingStrategy(surcharge));
                        else
                            ctx.SetShippingStrategy(new NightShippingStrategy());
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Выход.");
                        return;
                }

                decimal weight = ReadPositiveDecimal("Вес посылки (кг): ");
                decimal distance = ReadPositiveDecimal("Расстояние доставки (км): ");

                decimal cost = ctx.CalculateCost(weight, distance);
                Console.WriteLine($"Стоимость доставки: {cost:C}");
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine($"Ошибка: {ae.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static decimal ReadPositiveDecimal(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal v))
                {
                    if (v < 0) { Console.WriteLine("Значение не может быть отрицательным."); continue; }
                    return v;
                }
                Console.WriteLine("Некорректный ввод. Попробуйте ещё раз.");
            }
        }
    }
}
// 2 Задание:
using System;
using System.Collections.Generic;
using System.Globalization;

namespace WeatherObserverDemo
{
    public interface IObserver
    {
        void Update(float temperature);
    }

    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }

    public class WeatherStation : ISubject
    {
        private readonly List<IObserver> _observers = new();
        private float _temperature;

        public void RegisterObserver(IObserver observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));
            if (_observers.Contains(observer))
            {
                Console.WriteLine("Наблюдатель уже зарегистрирован.");
                return;
            }
            _observers.Add(observer);
            Console.WriteLine($"{observer.GetType().Name} зарегистрирован.");
        }

        public void RemoveObserver(IObserver observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));
            if (_observers.Remove(observer))
                Console.WriteLine($"{observer.GetType().Name} удалён.");
            else
                Console.WriteLine("Попытка удалить несуществующего наблюдателя — игнорировано.");
        }

        public void NotifyObservers()
        {
            foreach (var obs in _observers)
            {
                try { obs.Update(_temperature); }
                catch (Exception ex) { Console.WriteLine($"Ошибка в наблюдателе {obs.GetType().Name}: {ex.Message}"); }
            }
        }

        public void SetTemperature(float newTemperature)
        {
            if (float.IsNaN(newTemperature) || float.IsInfinity(newTemperature))
            {
                Console.WriteLine("Некорректное значение температуры.");
                return;
            }
            if (newTemperature < -100f || newTemperature > 100f)
            {
                Console.WriteLine("Температура вне допустимого диапазона (-100..100). Изменение отменено.");
                return;
            }

            _temperature = newTemperature;
            Console.WriteLine($"\n[WeatherStation] Температура установлена: {_temperature}°C");
            NotifyObservers();
        }
    }

    public class WeatherDisplay : IObserver
    {
        private readonly string _name;
        public WeatherDisplay(string name) => _name = name;
        public void Update(float temperature) => Console.WriteLine($"[{_name}] Текущая температура: {temperature}°C");
    }

    public class EmailAlert : IObserver
    {
        private readonly string _email;
        public EmailAlert(string email) => _email = email;
        public void Update(float temperature) => Console.WriteLine($"[Email -> {_email}] Отправлено оповещение: {temperature}°C");
    }

    public class SoundAlert : IObserver
    {
        private readonly float _threshold;
        public SoundAlert(float threshold) => _threshold = threshold;
        public void Update(float temperature)
        {
            if (temperature >= _threshold)
                Console.WriteLine($"[SoundAlert] Порог {_threshold}°C достигнут — воспроизвести сигнал!");
            else
                Console.WriteLine($"[SoundAlert] Темп. {temperature}°C — ниже порога {_threshold}°C.");
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Weather Observer Demo ===");
            var station = new WeatherStation();

            var mobile = new WeatherDisplay("Мобильное приложение");
            var board = new WeatherDisplay("Электронное табло");
            var email = new EmailAlert("alerts@example.com");
            var sound = new SoundAlert(30f);

            // стартовая регистрация
            station.RegisterObserver(mobile);
            station.RegisterObserver(board);
            station.RegisterObserver(email);
            station.RegisterObserver(sound);

            Console.WriteLine("\nКоманды:");
            Console.WriteLine("set <value>          — установить температуру (пример: set 25)");
            Console.WriteLine("add display <name>   — добавить дисплей");
            Console.WriteLine("add email <addr>     — добавить email-оповещение");
            Console.WriteLine("add sound <threshold>- добавить звуковой оповещатель");
            Console.WriteLine("remove <type>        — удалить: mobile, board, email, sound (удаляет только заранее созданные объекты)");
            Console.WriteLine("exit                 — выход\n");

            while (true)
            {
                Console.Write("cmd> ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
                var cmd = parts[0].ToLowerInvariant();

                if (cmd == "exit") break;

                if (cmd == "set" && parts.Length >= 2)
                {
                    if (float.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out float t))
                        station.SetTemperature(t);
                    else
                        Console.WriteLine("Неверный формат температуры.");
                }
                else if (cmd == "add" && parts.Length >= 3)
                {
                    var type = parts[1].ToLowerInvariant();
                    var arg = parts[2];
                    if (type == "display") station.RegisterObserver(new WeatherDisplay(arg));
                    else if (type == "email") station.RegisterObserver(new EmailAlert(arg));
                    else if (type == "sound")
                    {
                        if (float.TryParse(arg, NumberStyles.Any, CultureInfo.InvariantCulture, out float thr))
                            station.RegisterObserver(new SoundAlert(thr));
                        else Console.WriteLine("Неверный порог для sound.");
                    }
                    else Console.WriteLine("Неизвестный тип наблюдателя (display|email|sound).");
                }
                else if (cmd == "remove" && parts.Length >= 2)
                {
                    var what = parts[1].ToLowerInvariant();
                    // Для демонстрации удаляем заранее созданные объекты по имени
                    if (what == "mobile") station.RemoveObserver(mobile);
                    else if (what == "board") station.RemoveObserver(board);
                    else if (what == "email") station.RemoveObserver(email);
                    else if (what == "sound") station.RemoveObserver(sound);
                    else Console.WriteLine("Неизвестный наблюдатель для удаления.");
                }
                else
                {
                    Console.WriteLine("Неизвестная команда.");
                }
            }
        }
    }
}
