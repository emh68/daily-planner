using DailyPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Services
{
    public class TaskService
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public TaskService(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        // Add new task
        public async Task AddTask(string entraUserId, string title)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            // Get user by EntraUserId
            var user = await context.Users.SingleOrDefaultAsync(u => u.EntraUserId == entraUserId);

            // If user does not exist, create a demo user
            if (user == null)
            {
                user = new User
                {
                    EntraUserId = entraUserId,
                    DisplayName = "Demo",
                    Email = "demo@example.com"
                };
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            // Add the new task
            var task = new TaskItem
            {
                Title = title,
                UserId = user.Id,
                IsCompleted = false
            };

            context.Tasks.Add(task);
            await context.SaveChangesAsync();
        }

        // Toggle task completion
        public async Task ToggleComplete(int taskId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var task = await context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                await context.SaveChangesAsync();
            }
        }

        // Delete a task
        public async Task DeleteTask(int taskId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var task = await context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                context.Tasks.Remove(task);
                await context.SaveChangesAsync();
            }
        }

        // Get all tasks for a user
        public async Task<List<TaskItem>> GetTasks(string entraUserId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var user = await context.Users
                .Include(u => u.Tasks)
                .SingleOrDefaultAsync(u => u.EntraUserId == entraUserId);

            return user?.Tasks.ToList() ?? new List<TaskItem>();
        }

        // Update the task description
        public async Task UpdateDescription(int taskId, string description)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var task = await context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                task.Description = description;
                await context.SaveChangesAsync();
            }
        }
    }
}
