using Microsoft.Extensions.DependencyInjection;
using TaskManager.Logic;

namespace TaskManager.TestHarness
{
    internal class Program
    {
        private static bool exit;


        /// <summary>
        /// Forgive my monolith Main method, I just wanted to get this working quickly. 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Task Manager Test Harness!");
            Console.WriteLine("Press 1 for Class-based implementation, 2 for Record-based implementation, or 3 for Sync List-based implementation:");
            string? choice = Console.ReadLine();
            if (choice != null)
            {
                switch (choice)
                {
                    case "1":
                        RunClassBased();
                        break;
                    case "2":
                        RunRecordBased();
                        break;
                    case "3":
                        RunSyncListBased();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Exiting.");
                        break;
                }
            }
        }

        private static void RunClassBased()
        {
            // Initialize the Class-based service
            StronglyTypedToDoService ServiceManager = new StronglyTypedToDoService();
            bool exit = false;
            Console.WriteLine("Class Task manager initialized.");
            Console.WriteLine("Please enter your name:");
            string owner = Console.ReadLine() ?? "DefaultOwner";
            while (!exit)
            {
                Console.WriteLine("Enter a command (add, remove, complete, list, exit):");
                string? command = Console.ReadLine()?.ToLower();
                switch (command)
                {
                    case "add":
                        Console.WriteLine("Enter task name:");
                        string? taskName = Console.ReadLine();
                        Console.WriteLine("Enter due date (yyyy-MM-dd):");
                        string? dueDateInput = Console.ReadLine();
                        if (!DateTime.TryParse(dueDateInput, out DateTime dueDate))
                        {
                            Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                            break;
                        }
                        if (string.IsNullOrEmpty(taskName))
                        {
                            Console.WriteLine("Task name cannot be empty.");
                            break;
                        }
                        ServiceManager.AddTask(taskName, dueDate, owner);
                        break;
                    case "remove":
                        Console.WriteLine("Enter task name to remove:");
                        taskName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(taskName))
                        {
                            ServiceManager.RemoveTask(taskName);
                        }
                        break;
                    case "complete":
                        Console.WriteLine("Enter task name to mark as complete:");
                        taskName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(taskName))
                        {
                            ServiceManager.CompleteTask(taskName);
                        }
                        break;
                    case "list":
                        var tasks = ServiceManager.GetTasks();
                        foreach (var task in tasks)
                        {
                            Console.WriteLine($"Task: {task.TaskName}, Due: {task.DueDate.ToShortDateString()}, Owner: {task.Owner}");
                        }
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Unknown command. Please try again.");
                        break;
                }
            }
        }
        private static void RunRecordBased()
        {
            // Initialize the Record-based service
            RecordToDoService ServiceManager = new RecordToDoService();
            bool exit = false;

            Console.WriteLine("Record Task manager initialized.");
            Console.WriteLine("Please enter your name:");
            string owner = Console.ReadLine() ?? "DefaultOwner";

            while (!exit)
            {
                Console.WriteLine("Enter a command (add, remove, list, exit):");
                string? command = Console.ReadLine()?.ToLower();
                switch (command)
                {
                    case "add":
                        Console.WriteLine("Enter task name:");
                        string? taskName = Console.ReadLine();
                        Console.WriteLine("Enter due date (yyyy-MM-dd):");
                        string? dueDateInput = Console.ReadLine();

                        if (!DateTime.TryParse(dueDateInput, out DateTime dueDate))
                        {
                            Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                            break;
                        }
                        if (string.IsNullOrEmpty(taskName))
                        {
                            Console.WriteLine("Task name cannot be empty.");
                            break;
                        }

                        ServiceManager.AddTask(taskName, dueDate, owner);
                        break;

                    case "remove":
                        Console.WriteLine("Enter task name to remove:");
                        taskName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(taskName))
                        {
                            ServiceManager.RemoveTask(taskName);
                        }
                        break;

                    case "list":
                        var tasks = ServiceManager.GetTasks();
                        foreach (var task in tasks)
                        {
                            Console.WriteLine($"Task: {task.TaskName}, Due: {task.DueDate.ToShortDateString()}, Owner: {task.Owner}");
                        }
                        break;

                    case "exit":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Unknown command. Please try again.");
                        break;
                }
            }
        }
        private static void RunSyncListBased()
        {
            // Initialize the Sync List-based service
            SyncListsToDoService ServiceManager = new SyncListsToDoService();
            bool exit = false;
            Console.WriteLine("Sync List Task manager initialized.");
            Console.WriteLine("Please enter your name:");
            string owner = Console.ReadLine() ?? "DefaultOwner";
            while (!exit)
            {
                Console.WriteLine("Enter a command (add, remove, list, exit):");
                string? command = Console.ReadLine()?.ToLower();
                switch (command)
                {
                    case "add":
                        Console.WriteLine("Enter task name:");
                        string? taskName = Console.ReadLine();
                        Console.WriteLine("Enter due date (yyyy-MM-dd):");
                        string? dueDateInput = Console.ReadLine();
                        if (!DateTime.TryParse(dueDateInput, out DateTime dueDate))
                        {
                            Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                            break;
                        }
                        if (string.IsNullOrEmpty(taskName))
                        {
                            Console.WriteLine("Task name cannot be empty.");
                            break;
                        }
                        ServiceManager.AddTask(taskName, dueDate, owner);
                        break;
                    case "remove":
                        Console.WriteLine("Enter task name to remove:");
                        taskName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(taskName))
                        {
                            ServiceManager.RemoveTask(taskName);
                        }
                        break;
                    case "list":
                        var tasks = ServiceManager.GetTasks();
                        foreach (var task in tasks)
                        {
                            Console.WriteLine($"Task: {task.TaskName}, Due: {task.DueDate.ToShortDateString()}, Owner: {task.Owner}");
                        }
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Unknown command. Please try again.");
                        break;
                }
            }
        }

    }
}


//internal class Program
//{
//    static void Main(string[] args)
//    {
//        // 1. Setup the DI Container (The "Factory")
//        var services = new ServiceCollection();
//        Console.WriteLine("Welcome to the Task Manager Test Harness!");
//        Console.WriteLine("Press 1 for Class-based implementation, 2 for Record-based implementation, or 3 for Sync List-based implementation:");
//        string? choice = Console.ReadLine();
//        if (choice != null)
//        {
//            switch (choice)
//            {
//                case "1":
//                    services.AddSingleton<IToDoService, StronglyTypedToDoService>();
//                    break;
//                case "2":
//                    services.AddSingleton<IToDoService, RecordToDoService>();
//                    break;
//                case "3":
//                    services.AddSingleton<IToDoService, SyncListsToDoService>();
//                    break;
//                default:
//                    Console.WriteLine("Invalid choice. Exiting.");
//                    return;
//            }

//            // 2. Build the provider
//            var serviceProvider = services.BuildServiceProvider();

//            // 3. Request the service from the container (Dependency Resolution)
//            IToDoService serviceManager = serviceProvider.GetRequiredService<IToDoService>();

//            // 4. Run your exact same interactive application loop
//            bool exit = false;
//            Console.WriteLine($"Task manager initialized using: {serviceManager.GetType().Name}");
//            Console.WriteLine("Please enter your name:");
//            string owner = Console.ReadLine() ?? "DefaultOwner";

//            while (!exit)
//            {
//                Console.WriteLine("\nEnter a command (add, remove, complete, list, exit):");
//                string? command = Console.ReadLine()?.ToLower();
//                switch (command)
//                {
//                    case "add":
//                        Console.WriteLine("Enter task name:");
//                        string? taskName = Console.ReadLine();
//                        Console.WriteLine("Enter due date (yyyy-MM-dd):");
//                        string? dueDateInput = Console.ReadLine();

//                        if (!DateTime.TryParse(dueDateInput, out DateTime dueDate))
//                        {
//                            Console.WriteLine("Invalid date format.");
//                            break;
//                        }
//                        if (string.IsNullOrEmpty(taskName))
//                        {
//                            Console.WriteLine("Task name cannot be empty.");
//                            break;
//                        }

//                        serviceManager.AddTask(taskName, dueDate, owner);
//                        break;

//                    case "remove":
//                        Console.WriteLine("Enter task name to remove:");
//                        taskName = Console.ReadLine();
//                        if (!string.IsNullOrEmpty(taskName)) serviceManager.RemoveTask(taskName);
//                        break;

//                    case "complete":
//                        Console.WriteLine("Enter task name to mark complete:");
//                        taskName = Console.ReadLine();
//                        if (!string.IsNullOrEmpty(taskName)) serviceManager.CompleteTask(taskName);
//                        break;

//                    case "list":
//                        for (int i = 0; i < serviceManager.GetTaskCount(); i++)
//                        {
//                            var task = serviceManager.GetTask(i);
//                            Console.WriteLine($"Task: {task.TaskName}, Due: {task.DueDate.ToShortDateString()}, Status/Owner: {task.Status}");
//                        }
//                        break;

//                    case "exit":
//                        exit = true;
//                        break;

//                    default:
//                        Console.WriteLine("Unknown command.");
//                        break;
//                }
//            }
//        }
//    }
//}
