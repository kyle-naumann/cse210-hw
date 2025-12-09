/*
Extra Features:

I added two gamification features to make the program more engaging:

1. Level System - Every 100 points you earn raises your level by 1. When you level up,
   you get a "LEVEL UP!" message. It's a simple way to show
   progress beyond just the point total.

2. Achievement System - There are 8 achievements you can unlock:
   - First Steps: Create your first goal
   - Getting Started: Complete your first goal
   - Persistent: Complete 5 goals
   - Point Hunter: Earn 500 points
   - Level Up: Reach level 3
   - Dedicated: Complete 10 goals
   - Master: Earn 1000 points
   - Legend: Reach level 5

   Each achievement shows as [EARNED] or [LOCKED] and gives you a little celebration message
   when you unlock it.

These features save with your goals so your progress persists between sessions.
*/

using System;

class Program
{
    static void Main(string[] args)
    {
        QuestManager questManager = new QuestManager();

        while (true)
        {
            Console.WriteLine();
            questManager.DisplayPlayerInfo();
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. View Achievements");
            Console.WriteLine("  7. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoal(questManager);
                    break;
                case "2":
                    questManager.ListGoalDetails();
                    break;
                case "3":
                    Console.Write("What is the filename for the goal file? ");
                    string saveFilename = Console.ReadLine();
                    questManager.SaveGoals(saveFilename);
                    break;
                case "4":
                    Console.Write("What is the filename for the goal file? ");
                    string loadFilename = Console.ReadLine();
                    questManager.LoadGoals(loadFilename);
                    break;
                case "5":
                    questManager.ListGoalNames();
                    Console.Write("Which goal did you accomplish? ");
                    int goalNumber = int.Parse(Console.ReadLine()) - 1;
                    questManager.RecordEvent(goalNumber);
                    break;
                case "6":
                    questManager.DisplayAchievements();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void CreateGoal(QuestManager questManager)
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");

        string choice = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();

        Console.Write("What is the amount of points associated with this goal? ");
        int points = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case "1":
                questManager.AddGoal(new SimpleGoal(name, description, points));
                break;
            case "2":
                questManager.AddGoal(new EternalGoal(name, description, points));
                break;
            case "3":
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("What is the bonus for accomplishing it that many times? ");
                int bonus = int.Parse(Console.ReadLine());
                questManager.AddGoal(new ChecklistGoal(name, description, points, target, bonus));
                break;
        }
    }
}