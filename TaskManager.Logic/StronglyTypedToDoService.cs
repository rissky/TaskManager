using System;
using System.Collections.Generic;

namespace TaskManager.Logic
{
    /// <summary>
    /// TodoService class that manages tasks using a single list of ManagedTask objects.  
    /// </summary>
    public class StronglyTypedToDoService
    {
        // Unified into a single list of our specific ManagedTask class type
        private List<ManagedTask> ToDoList { get; set; } = new List<ManagedTask>();

        /// <summary>
        /// Adds a new task to the to-do list with the specified name, due date, and owner.
        /// </summary>
        public void AddTask(string taskName, DateTime dueDate, string owner)
        {
            ToDoList.Add(new ManagedTask(taskName, dueDate, owner));
        }

        /// <summary>
        /// Removes a task from the to-do list based on the specified task name. 
        /// </summary>
        public void RemoveTask(string taskName)
        {
            ManagedTask? taskToRemove = ToDoList.Find(t => t.Name == taskName);
            if (taskToRemove != null)
            {
                ToDoList.Remove(taskToRemove);
            }
        }

        /// <summary>
        /// New behavior enabled by using a Class: Marks a specific task as complete by name, 
        /// triggering the task's internal logic and timestamp.
        /// </summary>
        public void CompleteTask(string taskName)
        {
            ManagedTask? task = ToDoList.Find(t => t.Name == taskName);
            if (task != null)
            {
                task.CompleteTask(); // Executes the method inside the ManagedTask class
            }
        }

        /// <summary>
        /// Returns a list of tuples containing the task names, due dates, and owners.
        /// (Maintained to preserve your existing method signature)
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
        /// Returns a tuple containing the task name, due date, and its current live status.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the task index is out of range</exception>
        public (string TaskName, DateTime DueDate, string Status) GetTask(int taskIndex)
        {
            if (taskIndex >= 0 && taskIndex < ToDoList.Count)
            {
                var task = ToDoList[taskIndex];
                // Now safely maps the real class Status ("Pending" or "Completed") instead of the Owner
                return (task.Name, task.DueDate, task.Status);
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