using System;

namespace TaskManager.Logic
{
    /// <summary>
    /// A Class representation demonstrating behavior, state tracking, and business logic.
    /// Use a CLASS when the object needs to manage its own internal state and perform actions.
    /// </summary>
    public class ManagedTask
    {
        // 1. Same core data properties as the record
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public string Owner { get; set; }

        // 2. Class-specific internal state
        public string Status { get; private set; } // Only this class can change the status
        public DateTime? CompletedAt { get; private set; }

        // Constructor to initialize the class
        public ManagedTask(string name, DateTime dueDate, string owner)
        {
            Name = name;
            DueDate = dueDate;
            Owner = owner;
            Status = "Pending"; // Default initial state
            CompletedAt = null;
        }

        // 3. A simple method containing business logic
        /// <summary>
        /// Updates the internal state of the task to completed and logs the timestamp.
        /// </summary>
        public void CompleteTask()
        {
            if (Status == "Completed")
            {
                Console.WriteLine("Task is already completed!");
                return;
            }

            Status = "Completed";
            CompletedAt = DateTime.Now;

            Console.WriteLine($"Success: '{Name}' has been marked complete by {Owner}.");
        }
    }
}