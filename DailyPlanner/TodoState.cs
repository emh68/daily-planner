using DailyPlanner.Models;

namespace DailyPlanner;

// Holds the state for the UI so widgets and pages stay synced
public class TodoState
{
    // Display list of tasks
    public List<TaskItem> Todos { get; set; } = new();
    // Checks if a change occurred
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

    // Count unfinished versus finished tasks
    public int GetUnfinished()
    {
        return Todos.Count(todo => !todo.IsCompleted);
    }

    public int GetFinished()
    {
        return Todos.Count(todo => todo.IsCompleted);
    }

    // Calculate precentages for progress widget
    public int PercentRemaining =>
        Todos.Count == 0 ? 0 :
        (int)Math.Round((Todos.Count - GetFinished()) * 100.0 / Todos.Count);

    public int PercentDone =>
        Todos.Count == 0 ? 0 :
        (int)Math.Round((Todos.Count - GetUnfinished()) * 100.0 / Todos.Count);

    public void NotifyChange() => OnChange?.Invoke();

}
