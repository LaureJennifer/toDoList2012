﻿using Microsoft.AspNetCore.WebUtilities;
using TodoList.Models;
using TodoList.Models.Pagination;

namespace toDoList2712.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        public HttpClient _httpClient;
        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateTask(TaskCreateRequest taskCreateRequest)
        {
            var result = await _httpClient.PostAsJsonAsync("api/tasks", taskCreateRequest);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTask(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/tasks/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<TaskDto> GetTaskDetails(string id)
        {
            var result = await _httpClient.GetFromJsonAsync<TaskDto>($"/api/tasks/{id}");
            return result;
        }

        public async Task<PagedList<TaskDto>> GetTasks(TaskListSearch taskListSearch)
        {
            var queryStringPara = new Dictionary<string, string>
            {
                ["pageNumber"] = taskListSearch.PageNumber.ToString()
            };
            if (!string.IsNullOrEmpty(taskListSearch.Name)) queryStringPara.Add("name", taskListSearch.Name);
            string url = QueryHelpers.AddQueryString("/api/tasks", queryStringPara);
            var result = await _httpClient.GetFromJsonAsync<PagedList<TaskDto>>(url);
            return result;
        }

        public async Task<bool> NameTask(Guid id, NameTaskRequest nameTaskRequest)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/tasks/{id}/taskname", nameTaskRequest);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTask(Guid id, TaskUpdateRequest taskUpdateRequest)
        {
            var result = await _httpClient.PutAsJsonAsync($"/api/tasks/{id}", taskUpdateRequest);
            return result.IsSuccessStatusCode;
        }
    }
}
