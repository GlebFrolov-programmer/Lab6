using System;
using System.Threading;

public class TrafficLight
{
    private Action<string, ConsoleColor> OnSignalChanged;

    public TrafficLight()
    {
        OnSignalChanged = (signal, color) =>
        {
            Console.ForegroundColor = color;
            Console.WriteLine(signal);
            Console.ResetColor();
        };
    }

    public void Start()
    {
        while (true)
        {
            ChangeSignal("Красный", ConsoleColor.Red, 10);
            ChangeSignal("Жёлтый", ConsoleColor.Yellow, 3);
            ChangeSignal("Зелёный", ConsoleColor.Green, 5);
        }
    }

    private void ChangeSignal(string signal, ConsoleColor color, int duration)
    {
        OnSignalChanged.Invoke(signal, color);
        Thread.Sleep(duration * 1000);
    }
}

class Program
{
    static void Main(string[] args)
    {
        TrafficLight trafficLight = new TrafficLight();
        trafficLight.Start();
    }
}