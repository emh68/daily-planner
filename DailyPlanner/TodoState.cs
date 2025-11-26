
using DailyPlanner.Components.Pages;

public class TodoState
{
    private readonly SemaphoreSlim _lock = new(1, 1);
    public List<TodoItem> Todos { get; set; } = new();

    public event Action? OnChange;

    public void AddTodo(TodoItem item)
    {
        Todos.Add(item);
        NotifyStateChanged();
    }

    public void RemoveTodo(TodoItem item)
    {
        Todos.Remove(item);
        NotifyStateChanged();
    }

    public int GetUnfinished()
    {
        return Todos.Count(todo => !todo.IsDone);
    }

    public int GetFinished()
    {
        return Todos.Count(todo => todo.IsDone);
    }

    public int PercentRemaining => Todos.Count == 0 ? 0 : (int)Math.Round((Todos.Count - GetFinished()) * 100.0 / Todos.Count);
    public int PercentDone => Todos.Count == 0 ? 0 : (int)Math.Round((Todos.Count - GetUnfinished()) * 100.0 / Todos.Count);

    private void NotifyStateChanged() => OnChange?.Invoke();
}