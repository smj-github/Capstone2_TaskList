using System;
using System.Collections.Generic;

namespace Capstone_TaskList
{
    class Program
    {
        public static List<string> menuOptions = new List<string>()
        {
            "1. List Tasks", "2. Add Task", "3. Delete Task", "4. Mark task complete", "5. Display task for a member ", "6. Display tasks before Due date", "7. Edit Task", "8. Quit"
        };

        public static List<CapstoneTask> taskList = new List<CapstoneTask>();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Task Manager!");
            int searchOpt = 0;

            while (searchOpt != 8)
            {
                // Display menu options
                DisplayMenuOptions();

                // Get User input
                Console.WriteLine("What would you like to do?");

                string userOpt = Console.ReadLine();
                while (!ValidateUserOption(userOpt, out searchOpt))
                {
                    Console.WriteLine("Invalid option! Try Again!");
                    userOpt = Console.ReadLine();
                }

                switch(searchOpt)
                {
                    case 1:
                        ListTasks();
                        break;
                    case 2:
                        AddTask();
                        break;
                    case 3:
                        DeleteTask();
                        break;
                    case 4:
                        MarkTaskComplete();
                        break;
                    case 5:
                        DisplayTaskForAMember();
                        break;
                    case 6:
                        DisplayTasksBeforeDueDate();
                        break;
                    case 7:
                        EditTask();
                        break;
                    case 8:
                        break;
                }
            }
        }
        public static void EditTask()
        {
            if (taskList.Count == 0)
            {
                Console.WriteLine("Cannot edit task! Task list is currently empty.\n");
            }
            else
            {
                // Get task no. to edit           
                int taskNo = GetTaskNumber("Please enter the task number to be edited.");

                bool bUpdateRequired = false;
                // Get the updates from user 
                string editName = GetUserInput("Would you like to edit Team Member Name? y/n");
                if (editName.ToLower()[0]=='y')
                {
                    bUpdateRequired = true;
                    taskList[taskNo - 1].TeamMemberName = GetUserInput("Please enter new Name");
                }

                string editDueDate = GetUserInput("Would you like to edit Due Date? y/n");
                if (editDueDate.ToLower()[0] == 'y')
                {
                    bUpdateRequired = true;
                    taskList[taskNo - 1].DueDate = GetDateInfo();
                }

                string editDesc = GetUserInput("Would you like to edit Description? y/n");
                if (editDesc.ToLower()[0] == 'y')
                {
                    bUpdateRequired = true;
                    taskList[taskNo - 1].TaskDesc = GetUserInput("Please enter new description");                    
                }

                if(bUpdateRequired)
                {
                    Console.WriteLine("Task Updated!\n");  
                }
                else
                    Console.WriteLine("Task not updated\n");
            }
        }
        public static void DisplayTasksBeforeDueDate()
        {
            if (taskList.Count == 0)
            {
                Console.WriteLine("Cannot search for tasks! Task list is currently empty\n");
            }
            else
            {
                DateTime dtDueDate = GetDateInfo();

                // Get the tasks before the due date and display the same 
                ShowTasksBeforeDueDate(dtDueDate);
                Console.WriteLine();
            }
        }

        public static void ShowTasksBeforeDueDate(DateTime dtDueDate)
        {
            DisplayHeader();
            bool bFound = false;
            for (int i = 0; i < taskList.Count; i++)
            {
                if (taskList[i].DueDate < dtDueDate)
                {
                    bFound = true;
                    ListTask(i + 1);
                }
            }
            if (!bFound)
                Console.WriteLine("No matching records found\n");
        }
        public static void DisplayHeader()
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-12} {3,-12} {4,-20}", "No.", "Name", "Due Date", "Status", "Description");
            Console.WriteLine("{0,-5} {1,-20} {2,-12} {3,-12} {4,-20}", "=====", "====================", "===========", "==========", "===========");
        }
        public static void DisplayTaskForAMember()
        {
            if (taskList.Count == 0)
            {
                Console.WriteLine("Cannot search for member! Task list is currently empty\n");
            }
            else
            {
                // Get team member's name            
                string name = GetUserInput("Please enter team member's name");
                while (name == string.Empty)
                {
                    Console.WriteLine("Please enter a valid name");
                    name = Console.ReadLine();
                }
                // Get the task list for this member and display the same 
                ShowTasksForMember(name);
                Console.WriteLine();
            }
        }
        public static void ShowTasksForMember(string memberName)
        {
            DisplayHeader();
            bool bFound = false;
            for (int i=0; i<taskList.Count; i++)
            {
                if (taskList[i].TeamMemberName.ToLower() == memberName.ToLower())
                {
                    bFound = true;
                    ListTask(i+1);
                }
            }
            if(!bFound)
                Console.WriteLine("No matching records found\n");
        }
        public static void MarkTaskComplete()
        {
            int taskNo = GetTaskNumber("Please enter the task number to be marked completed");

            Console.WriteLine("Are you sure you want to mark this task as complete? y/n");
            if (Console.ReadLine().ToLower()[0] == 'y')
            {
                // Mark the task complete
                try
                {
                    taskList[taskNo - 1].Complete();
                    Console.WriteLine("Task {0} marked completed\n", taskNo);
                }
                catch
                {
                    Console.WriteLine("Error occurred! Task not marked completed\n");
                }
            }
        }
        public static void DisplayMenuOptions()
        {
            Console.WriteLine("MAIN MENU");
            foreach(string menu in menuOptions)
            {
                Console.WriteLine(menu);
            }
        }
        public static bool ValidateUserOption(string userOption, out int number)
        {
            if (int.TryParse(userOption, out number))
            {
                if ((number > 0) && (number <= menuOptions.Count))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }
        public static void ListTasks()
        {
            // Display each item in the task list
            if (taskList.Count == 0)
            {
                Console.WriteLine("Task List is currently empty\n");
            }
            else
            {                
                DisplayHeader();
                for (int i = 0; i < taskList.Count; i++)
                {
                    string strStatus = "";
                    if (taskList[i].TaskStatus)
                        strStatus = "Complete";
                    else
                        strStatus = "Incomplete";
                    Console.WriteLine("{0,-5} {1,-20} {2,-12} {3,-12} {4,-20}", i + 1 , taskList[i].TeamMemberName, taskList[i].DueDate.ToShortDateString(), strStatus, taskList[i].TaskDesc);
                }
                Console.WriteLine("\n");
            }
        }
        public static DateTime GetDateInfo()
        {
            string dueDate = GetUserInput("Please enter due date as MM/DD/YYYY");
            bool validDate = false;
            DateTime dtDueDate = DateTime.MinValue;
            while (!validDate)
            {
                try
                {
                    dtDueDate = DateTime.Parse(dueDate);
                    validDate = true;
                }
                catch
                {
                    validDate = false;
                    Console.WriteLine("Invalid entry! Please enter a valid date");
                    dueDate = Console.ReadLine();
                }
            }
            return dtDueDate;
        }
        public static void AddTask()
        {
            Console.Clear();
            // Get team member's name            
            string name = GetUserInput("Please enter team member's name"); 
            while (name==string.Empty)
            {
                Console.WriteLine("Please enter a valid name");
                name = Console.ReadLine();
            }

            // Get task description
            
            string desc = GetUserInput("Please enter task description");
            while (desc == string.Empty)
            {
                Console.WriteLine("Please enter a valid description");
                desc = Console.ReadLine();
            }
                        
            // Create a new task object
            CapstoneTask newTask = new CapstoneTask();
            newTask.TeamMemberName = name;
            newTask.TaskDesc = desc;
            newTask.DueDate = GetDateInfo(); 

            // Add the task to the list
            try
            {
                taskList.Add(newTask);
                Console.WriteLine("New task added\n");
            }
            catch
            {
                Console.WriteLine("Error occurred! Task not added\n");
            }            
        }
        public static string GetUserInput(string msg)
        {
            Console.WriteLine(msg);
            return(Console.ReadLine());
        }
        public static void DeleteTask()
        {
            int taskNo = GetTaskNumber("Please enter the Task Number to be deleted");
            
            Console.WriteLine("Are you sure you want to delete this task? y/n");
            if(Console.ReadLine().ToLower()[0] == 'y')
            {
                // Delete the task from the list
                try
                {
                    taskList.RemoveAt(taskNo - 1);
                    Console.WriteLine("Task {0} deleted\n", taskNo);
                }
                catch
                {
                    Console.WriteLine("Error occurred! Task not deleted\n");
                }
            }  
        }
        public static int GetTaskNumber(string msg)
        {
            Console.Clear();
            ListTasks();
            string input = GetUserInput(msg);
            int taskNo;
            while (!ValidateTaskNumber(input, out taskNo))
            {
                Console.WriteLine("Invalid entry!. Please enter a number between 1 - " + taskList.Count);
                input = Console.ReadLine();
            }

            // Display selected task
            Console.WriteLine("You selected: ");
            DisplayHeader();
            ListTask(taskNo);
            return taskNo;
        }
        public static void ListTask(int taskNo)
        {
            // shift index
            int i = taskNo - 1;            
            
            string strStatus = "";
            if (taskList[i].TaskStatus)
                strStatus = "Complete";
            else
                strStatus = "Incomplete";
            Console.WriteLine("{0,-5} {1,-20} {2,-12} {3,-12} {4,-20}", i + 1, taskList[i].TeamMemberName, taskList[i].DueDate.ToShortDateString(), strStatus, taskList[i].TaskDesc);

        }
        public static bool ValidateTaskNumber(string input, out int taskNumber)
        {
            taskNumber = -1;      
            while(!int.TryParse(input, out taskNumber))
            {  
                Console.WriteLine("Invalid entry! Please enter a valid task number");
                input = Console.ReadLine();
                
            }
            if ((1 <= taskNumber) && (taskNumber <= taskList.Count))
            {
                return true;
            }
            return false;
        }
    }
}
