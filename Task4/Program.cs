using System;

public interface IEmployee
{
    void Work(int hours);
    void TakeBreak(int minutes);
}

public class Employee : IEmployee
{
    public int WorkedHours { get; private set; }
    public int BreakTime { get; private set; }

    public event Action<int> WorkPerformed;
    public event Action<int> BreakTaken;

    public void Work(int hours)
    {
        WorkedHours += hours;
        WorkPerformed?.Invoke(hours);
    }

    public void TakeBreak(int minutes)
    {
        BreakTime += minutes;
        BreakTaken?.Invoke(minutes);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Employee emp1 = new Employee();
        Employee emp2 = new Employee();

        emp1.WorkPerformed += WorkPerformedHandler;
        emp1.BreakTaken += BreakTakenHandler;

        emp2.WorkPerformed += WorkPerformedHandler;
        emp2.BreakTaken += BreakTakenHandler;

        emp1.Work(3);
        emp1.TakeBreak(15);

        emp2.Work(5);
        emp2.TakeBreak(25);
    }

    public static void WorkPerformedHandler(int hours)
    {
        string hoursText = GetHoursText(hours);
        Console.WriteLine($"Проработал {hours} {hoursText}.");
    }

    public static void BreakTakenHandler(int minutes)
    {
        Console.WriteLine($"Сделал перерыв в течение {minutes} минут.");
    }

    public static string GetHoursText(int hours)
    {
        if (hours == 1)
        {
            return "час";
        }
        else if (hours >= 2 && hours <= 4)
        {
            return "часа";
        }
        else
        {
            return "часов";
        }
    }
}