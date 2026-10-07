using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManager.Logic
{
    /// <summary>
    /// Represents a specific task data type. 
    /// The Record(TodoTask) — The Data Envelope
    /// Use it like a post - it note or a database row.It just holds data to move it around safely.
    /// It is immutable (read-only by default). If you want to change the owner, you don't modify the record; you make a copy of it with a new name using the with expression: var newRecord = oldRecord with { Owner = "Alice" };.

    /// </summary>
    public record TodoTask(string Name, DateTime DueDate, string Owner);

    /// <summary>
    /// TodoService class that manages tasks using a single, unified list.  
    /// </summary>
    public class RecordToDoService
    {
        // Unified into a single list of our specific task type
        private List<TodoTask> ToDoList { get; set; } = new List<TodoTask>();

        /// <summary>
        /// Adds a new task to the to-do list with the specified name, due date, and owner.
        /// </summary>
        public void AddTask(string taskName, DateTime dueDate, string owner)
        {
            ToDoList.Add(new TodoTask(taskName, dueDate, owner));
        }

        /// <summary>
        /// Removes a task from the to-do list based on the specified task name. 
        /// </summary>
        public void RemoveTask(string taskName)
        {
            // Find the task that matches the given name
            TodoTask? taskToRemove = ToDoList.Find(t => t.Name == taskName);
            if (taskToRemove != null)
            {
                ToDoList.Remove(taskToRemove);
            }
        }

        /// <summary>
        /// Returns a list of tuples containing the task names, due dates, and owners.
        /// (Maintained to preserve your existing method signature and return type)
        /// </summary>
        public List<(string TaskName, DateTime DueDate, string Owner)> GetTasks()
        {
            var returnList = new List<(string TaskName, DateTime DueDate, string Owner)>();

            foreach (var task in ToDoList)
            {
                returnList.Add((task.Name, task.DueDate, task.Owner));
            }

            return returnList;
        }

        /// <summary>
        /// Returns a tuple containing the task name, due date, and status for a specific task index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the task index is out of range</exception>
        public (string TaskName, DateTime DueDate, string Status) GetTask(int taskIndex)
        {
            if (taskIndex >= 0 && taskIndex < ToDoList.Count)
            {
                var task = ToDoList[taskIndex];
                // Note: Your original code mapped Owner into the 'Status' tuple field, preserved here
                return (task.Name, task.DueDate, task.Owner);
            }

            throw new ArgumentOutOfRangeException(nameof(taskIndex));
        }

        public bool ClearTasks()
        {
            ToDoList.Clear();
            return true;
        }

        public int GetTaskCount()
        {
            return ToDoList.Count;
        }
    }
}