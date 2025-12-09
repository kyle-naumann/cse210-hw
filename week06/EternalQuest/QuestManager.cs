using System;
using System.Collections.Generic;
using System.IO;

public class QuestManager
{
    private List<Goal> _goals;
    private int _score;
    private int _level;
    private List<Achievement> _achievements;

    public QuestManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _level = 1;
        _achievements = new List<Achievement>
        {
            new Achievement("First Steps", "Create your first goal"),
            new Achievement("Getting Started", "Complete your first goal"),
            new Achievement("Persistent", "Complete 5 goals"),
            new Achievement("Point Hunter", "Earn 500 points"),
            new Achievement("Level Up", "Reach level 3"),
            new Achievement("Dedicated", "Complete 10 goals"),
            new Achievement("Master", "Earn 1000 points"),
            new Achievement("Legend", "Reach level 5")
        };
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
        CheckAchievement("First Steps");
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < _goals.Count)
        {
            int pointsEarned = _goals[goalIndex].RecordEvent();
            _score += pointsEarned;

            if (pointsEarned > 0)
            {
                Console.WriteLine($"Congratulations! You earned {pointsEarned} points!");

                // Check for level up
                int newLevel = CalculateLevel();
                if (newLevel > _level)
                {
                    _level = newLevel;
                    Console.WriteLine($"LEVEL UP! You are now level {_level}!");
                    if (_level >= 3) CheckAchievement("Level Up");
                    if (_level >= 5) CheckAchievement("Legend");
                }

                // Check achievements
                CheckAchievement("Getting Started");
                if (_score >= 500) CheckAchievement("Point Hunter");
                if (_score >= 1000) CheckAchievement("Master");

                int completedGoals = CountCompletedGoals();
                if (completedGoals >= 5) CheckAchievement("Persistent");
                if (completedGoals >= 10) CheckAchievement("Dedicated");
            }
        }
    }

    private int CalculateLevel()
    {
        return (_score / 100) + 1;
    }

    private int CountCompletedGoals()
    {
        int count = 0;
        foreach (Goal goal in _goals)
        {
            if (goal.IsComplete()) count++;
        }
        return count;
    }

    private void CheckAchievement(string achievementName)
    {
        foreach (Achievement achievement in _achievements)
        {
            if (achievement.Name == achievementName && !achievement.IsEarned)
            {
                achievement.IsEarned = true;
                Console.WriteLine($"Achievement Unlocked: {achievement.Name} - {achievement.Description}!");
                break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"You have {_score} points.");
        Console.WriteLine($"Level: {_level}");

        int earnedCount = 0;
        foreach (Achievement achievement in _achievements)
        {
            if (achievement.IsEarned) earnedCount++;
        }
        Console.WriteLine($"Achievements: {earnedCount}/{_achievements.Count}");
    }

    public void ListGoalNames()
    {
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
        }
    }

    public void ListGoalDetails()
    {
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void DisplayAchievements()
    {
        Console.WriteLine("\n=== ACHIEVEMENTS ===");
        foreach (Achievement achievement in _achievements)
        {
            string status = achievement.IsEarned ? "[EARNED]" : "[LOCKED]";
            Console.WriteLine($"{status} {achievement.Name} - {achievement.Description}");
        }
    }

    public void SaveGoals(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);
            outputFile.WriteLine(_level);

            // Save achievements
            foreach (Achievement achievement in _achievements)
            {
                outputFile.WriteLine($"Achievement:{achievement.Name},{achievement.IsEarned}");
            }

            // Save goals
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
    }

    public void LoadGoals(string filename)
    {
        if (File.Exists(filename))
        {
            string[] lines = File.ReadAllLines(filename);

            _score = int.Parse(lines[0]);
            _level = int.Parse(lines[1]);

            _goals.Clear();

            for (int i = 2; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':');
                string type = parts[0];

                if (type == "Achievement")
                {
                    string[] achievementParts = parts[1].Split(',');
                    string name = achievementParts[0];
                    bool isEarned = bool.Parse(achievementParts[1]);

                    foreach (Achievement achievement in _achievements)
                    {
                        if (achievement.Name == name)
                        {
                            achievement.IsEarned = isEarned;
                            break;
                        }
                    }
                }
                else if (type == "SimpleGoal")
                {
                    string[] goalParts = parts[1].Split(',');
                    string name = goalParts[0];
                    string description = goalParts[1];
                    int points = int.Parse(goalParts[2]);
                    bool isComplete = bool.Parse(goalParts[3]);
                    _goals.Add(new SimpleGoal(name, description, points, isComplete));
                }
                else if (type == "EternalGoal")
                {
                    string[] goalParts = parts[1].Split(',');
                    string name = goalParts[0];
                    string description = goalParts[1];
                    int points = int.Parse(goalParts[2]);
                    _goals.Add(new EternalGoal(name, description, points));
                }
                else if (type == "ChecklistGoal")
                {
                    string[] goalParts = parts[1].Split(',');
                    string name = goalParts[0];
                    string description = goalParts[1];
                    int points = int.Parse(goalParts[2]);
                    int bonus = int.Parse(goalParts[3]);
                    int target = int.Parse(goalParts[4]);
                    int amountCompleted = int.Parse(goalParts[5]);
                    _goals.Add(new ChecklistGoal(name, description, points, target, bonus, amountCompleted));
                }
            }
        }
    }
}