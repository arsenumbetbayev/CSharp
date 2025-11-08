// 1 Задание
using System;

public class TV
{
    public void On() => Console.WriteLine("Телевизор включен.");
    public void Off() => Console.WriteLine("Телевизор выключен.");
    public void SetChannel(int channel) => Console.WriteLine($"Выбран канал №{channel}.");
}

public class AudioSystem
{
    private int volume = 10;

    public void On() => Console.WriteLine("Аудиосистема включена.");
    public void Off() => Console.WriteLine("Аудиосистема выключена.");
    public void SetVolume(int level)
    {
        volume = level;
        Console.WriteLine($"Громкость установлена на уровень {volume}.");
    }
}

public class DVDPlayer
{
    public void On() => Console.WriteLine("DVD-проигрыватель включен.");
    public void Off() => Console.WriteLine("DVD-проигрыватель выключен.");
    public void Play() => Console.WriteLine("Воспроизведение DVD начато.");
    public void Pause() => Console.WriteLine("Воспроизведение приостановлено.");
    public void Stop() => Console.WriteLine("Воспроизведение остановлено.");
}

public class GameConsole
{
    public void On() => Console.WriteLine("Игровая консоль включена.");
    public void Off() => Console.WriteLine("Игровая консоль выключена.");
    public void StartGame(string game) => Console.WriteLine($"Запуск игры: {game}.");
}
public class HomeTheaterFacade
{
    private TV tv;
    private AudioSystem audio;
    private DVDPlayer dvd;
    private GameConsole console;

    public HomeTheaterFacade(TV tv, AudioSystem audio, DVDPlayer dvd, GameConsole console)
    {
        this.tv = tv;
        this.audio = audio;
        this.dvd = dvd;
        this.console = console;
    }

    public void WatchMovie()
    {
        Console.WriteLine("\nРежим: Просмотр фильма");
        tv.On();
        audio.On();
        audio.SetVolume(15);
        dvd.On();
        dvd.Play();
    }

    public void StopMovie()
    {
        Console.WriteLine("\nОстановка фильма");
        dvd.Stop();
        dvd.Off();
        audio.Off();
        tv.Off();
    }

    public void PlayGame(string game)
    {
        Console.WriteLine("\nРежим: Игра");
        tv.On();
        audio.On();
        audio.SetVolume(20);
        console.On();
        console.StartGame(game);
    }

    public void StopGame()
    {
        Console.WriteLine("\nЗавершение игры");
        console.Off();
        audio.Off();
        tv.Off();
    }

    public void ListenMusic()
    {
        Console.WriteLine("\nРежим: Прослушивание музыки");
        tv.On();
        audio.On();
        audio.SetVolume(12);
        Console.WriteLine("TV установлен на аудиовход (AUX).");
    }

    public void SetVolume(int level)
    {
        Console.WriteLine("\nИзменение громкости");
        audio.SetVolume(level);
    }
}
class Program
{
    static void Main(string[] args)
    {
        
        TV tv = new TV();
        AudioSystem audio = new AudioSystem();
        DVDPlayer dvd = new DVDPlayer();
        GameConsole console = new GameConsole();

        
        HomeTheaterFacade homeTheater = new HomeTheaterFacade(tv, audio, dvd, console);

        
        homeTheater.WatchMovie();
        homeTheater.SetVolume(18);
        homeTheater.StopMovie();

    
        homeTheater.PlayGame("Battlefield 2042");
        homeTheater.StopGame();

    
        homeTheater.ListenMusic();
        homeTheater.SetVolume(8);
    }
}
// 2 Задание
using System;
using System.Collections.Generic;


public abstract class FileSystemComponent
{
    public string Name { get; set; }

    protected FileSystemComponent(string name)
    {
        Name = name;
    }

    public abstract void Display(string indent = "");
    public abstract int GetSize();
}


public class File : FileSystemComponent
{
    private int size;

    public File(string name, int size) : base(name)
    {
        this.size = size;
    }

    public override void Display(string indent = "")
    {
        Console.WriteLine($"{indent}- Файл: {Name} ({size} КБ)");
    }

    public override int GetSize() => size;
}


public class Directory : FileSystemComponent
{
    private List<FileSystemComponent> components = new List<FileSystemComponent>();

    public Directory(string name) : base(name) { }

    public void Add(FileSystemComponent component)
    {
        if (components.Contains(component))
        {
            Console.WriteLine($"Компонент '{component.Name}' уже существует в '{Name}'.");
            return;
        }
        components.Add(component);
    }

    public void Remove(FileSystemComponent component)
    {
        if (!components.Contains(component))
        {
            Console.WriteLine($"Компонент '{component.Name}' не найден в '{Name}'.");
            return;
        }
        components.Remove(component);
    }

    public override void Display(string indent = "")
    {
        Console.WriteLine($"{indent}+ Папка: {Name}");
        foreach (var component in components)
        {
            component.Display(indent + "   ");
        }
    }

    public override int GetSize()
    {
        int totalSize = 0;
        foreach (var component in components)
        {
            totalSize += component.GetSize();
        }
        return totalSize;
    }
}


class Program
{
    static void Main(string[] args)
    {
        File file1 = new File("Resume.docx", 120);
        File file2 = new File("Photo.png", 540);
        File file3 = new File("Music.mp3", 3200);
        File file4 = new File("Video.mp4", 8000);
        File file5 = new File("Notes.txt", 40);

        Directory documents = new Directory("Документы");
        Directory images = new Directory("Изображения");
        Directory music = new Directory("Музыка");
        Directory videos = new Directory("Видео");
        Directory mainFolder = new Directory("Главная папка");

        documents.Add(file1);
        documents.Add(file5);

        images.Add(file2);
        music.Add(file3);
        videos.Add(file4);

        mainFolder.Add(documents);
        mainFolder.Add(images);
        mainFolder.Add(music);
        mainFolder.Add(videos);

        Console.WriteLine("=== Содержимое файловой системы ===");
        mainFolder.Display();

        Console.WriteLine($"\nОбщий размер папки '{mainFolder.Name}': {mainFolder.GetSize()} КБ");
        Console.WriteLine("\nПопытка удалить файл 'Photo.png' из 'Главной папки' (ошибка, файл в подпапке):");
        mainFolder.Remove(file2);

        Console.WriteLine("\nУдаляем файл из его папки:");
        images.Remove(file2);
        images.Display();
    }
}
