using System;

// Класс, представляющий задачу
class Task
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
}

// Делегат для события изменения статуса задачи
delegate void TaskStatusChangedEventHandler(Task task);

// Класс, отвечающий за управление задачами
class TaskManager
{
    // Событие изменения статуса задачи
    public event TaskStatusChangedEventHandler TaskStatusChanged;

    // Метод для завершения задачи
    public void CompleteTask(Task task)
    {
        task.IsCompleted = true;

        // Вызов события для уведомления подписчиков
        TaskStatusChanged?.Invoke(task);
    }
}

// Класс, который будет подписываться на событие изменения статуса задачи
class NotificationCenter
{
    // Метод, вызываемый при событии изменения статуса задачи
    public static void TaskCompletedNotification(Task task)
    {
        Console.WriteLine($"Задача \"{task.Title}\" была выполнена.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создание экземпляра TaskManager
        var taskManager = new TaskManager();

        // Подписка на событие изменения статуса задачи
        taskManager.TaskStatusChanged += NotificationCenter.TaskCompletedNotification;

        // Создание задач
        var task1 = new Task { Title = "Задача 1", Description = "Описание задачи 1", IsCompleted = false };
        var task2 = new Task { Title = "Задача 2", Description = "Описание задачи 2", IsCompleted = false };
        var task3 = new Task { Title = "Задача 3", Description = "Описание задачи 3", IsCompleted = false };
        var task4 = new Task { Title = "Задача 4", Description = "Описание задачи 4", IsCompleted = false };

        // Завершение задачи
        taskManager.CompleteTask(task1);
        taskManager.CompleteTask(task3);
    }
}