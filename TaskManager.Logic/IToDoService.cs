using System;
using System.Collections.Generic;

namespace TaskManager.Logic
{
    public interface IToDoService
    {
        void AddTask(string taskName, DateTime dueDate, string owner);
        void RemoveTask(string taskName);
        List<(string TaskName, DateTime DueDate, string Owner)> GetTasks();
        (string TaskName, DateTime DueDate, string Status) GetTask(int taskIndex);
        bool ClearTasks();
        int GetTaskCount();

        // Optional: Add a default implementation or mock for versions that don't support it
        void CompleteTask(string taskName) { }
    }
}