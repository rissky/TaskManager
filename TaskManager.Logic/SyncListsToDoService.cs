namespace TaskManager.Logic
{
    /// <summary>
    /// TodoService class that manages a list of tasks, including their names, due dates, and owners using synchronized simple lists.  
    /// </summary>
    public class SyncListsToDoService : IToDoService
    {
        private List<string> ToDoList_TaskNames { get; set; } = new List<string>();
        private List<DateTime> ToDoList_TaskDueDate { get; set; } = new List<DateTime>();
        private List<string> ToDoList_TaskOwner { get; set; } = new List<string>();

        /// <summary>
        /// Adds a new task to the to-do list with the specified name, due date, and owner.
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="dueDate"></param>
        /// <param name="owner"></param>
        public void AddTask(string taskName, DateTime dueDate, string owner)
        {
            ToDoList_TaskNames.Add(taskName);
            ToDoList_TaskDueDate.Add(dueDate);
            ToDoList_TaskOwner.Add(owner);
        }

        /// <summary>
        /// removes a task from the to-do list based on the specified task name. 
        /// If the task name exists, it removes the corresponding entries from all three lists (task names, due dates, and owners) to maintain synchronization.
        /// </summary>
        /// <param name="taskName"></param>
        public void RemoveTask(string taskName)
        {
            int index = ToDoList_TaskNames.IndexOf(taskName);
            if (index >= 0)
            {
                ToDoList_TaskNames.RemoveAt(index);
                ToDoList_TaskDueDate.RemoveAt(index);
                ToDoList_TaskOwner.RemoveAt(index);
            }
        }

        /// <summary>
        /// Returns a tuple containing the task names, the latest due date, and the task owners.
        /// A tuple is a data structure that holds a fixed number of values together as a single unit, 
        /// allowing you to group multiple data types without making a custom class.
        /// They are cool af - they allow you to return multiple values from a method without creating a separate class or struct.
        /// </summary>
        /// <returns>tuple containing the task names, the latest due date, and the task owners</returns>
        public List<(string TaskName, DateTime DueDate, string Owner)> GetTasks()
        {
            int count = ToDoList_TaskNames.Count;

            var returnList = new List<(string, DateTime, string)>();
            for (int i = 0; i < count; i++)
            {
                returnList.Add((ToDoList_TaskNames[i], ToDoList_TaskDueDate[i], ToDoList_TaskOwner[i]));
            }

            return returnList;

        }

        /// <summary>
        /// Returns a tuple containing the task name, due date, and status for a specific task index.
        /// </summary>
        /// <param name="taskIndex">The index of the task to retrieve</param>
        /// <returns>A tuple containing the task name, due date, and status</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the task index is out of range</exception>
        public (string TaskName, DateTime DueDate, string Status) GetTask(int taskIndex)
        {
            if (taskIndex >= 0 && taskIndex < ToDoList_TaskNames.Count)
            {
                return (ToDoList_TaskNames[taskIndex], ToDoList_TaskDueDate[taskIndex], ToDoList_TaskOwner[taskIndex]);
            }

            throw new ArgumentOutOfRangeException(nameof(taskIndex));
        }

        public bool ClearTasks()
        {
            ToDoList_TaskNames.Clear();
            ToDoList_TaskDueDate.Clear();
            ToDoList_TaskOwner.Clear();
            return true;

        }

        public int GetTaskCount()
        {
            return ToDoList_TaskNames.Count;
        }

    }
    
}
