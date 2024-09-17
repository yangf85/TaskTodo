using System.Net.Http.Json;
using TaskTodo.Shared.Models;

namespace TaskApp.Client.Services
{
    public class TaskService
    {
        private readonly HttpClient _httpClient;

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TaskItem>>("api/tasks");
        }

        public async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TaskItem>($"api/tasks/{id}");
        }

        public async Task AddAsync(TaskItem task)
        {
            await _httpClient.PostAsJsonAsync("api/tasks", task);
        }

        public async Task UpdateAsync(TaskItem task)
        {
            await _httpClient.PutAsJsonAsync($"api/tasks/{task.Id}", task);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/tasks/{id}");
        }
    }
}