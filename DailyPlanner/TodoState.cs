using DailyPlanner.Models;

namespace DailyPlanner;

public class TodoState
{
    private readonly SemaphoreSlim _lock = new(1, 1);

    public List<TaskItem> Todos { get; set; } = new();

    public event Action? OnChange;

    public void AddTodo(TaskItem item)
    {
        Todos.Add(item);
        NotifyChange();
    }

    public void RemoveTodo(TaskItem item)
    {
        Todos.Remove(item);
        NotifyChange();
    }

    public int GetUnfinished()
    {
        return Todos.Count(todo => !todo.IsCompleted);
    }

    public int GetFinished()
    {
        return Todos.Count(todo => todo.IsCompleted);
    }

    public int PercentRemaining =>
        Todos.Count == 0 ? 0 :
        (int)Math.Round((Todos.Count - GetFinished()) * 100.0 / Todos.Count);

    public int PercentDone =>
        Todos.Count == 0 ? 0 :
        (int)Math.Round((Todos.Count - GetUnfinished()) * 100.0 / Todos.Count);

    public void NotifyChange() => OnChange?.Invoke();

}
