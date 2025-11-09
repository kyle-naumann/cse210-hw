using System;

/*
 * EXCEEDING REQUIREMENTS:
 * Added search functionality to find specific journal entries by keyword.
 * Addresses the problem of finding past entries as the journal grows.
 */

class Program
{
    static void Main(string[] args)
    {
        Journal theJournal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        int userChoice = -1;

        while (userChoice != 6)
        {
            Console.WriteLine("Please select one of the following choices:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Search");
            Console.WriteLine("6. Quit");
            Console.Write("What would you like to do? ");

            string userInput = Console.ReadLine();
            userChoice = int.Parse(userInput);

            if (userChoice == 1)
            {
                Entry newEntry = new Entry();
                newEntry._date = DateTime.Now.ToShortDateString();
                newEntry._promptText = promptGenerator.GetRandomPrompt();

                Console.WriteLine(newEntry._promptText);
                Console.Write("> ");
                newEntry._entryText = Console.ReadLine();

                theJournal.AddEntry(newEntry);
            }
            else if (userChoice == 2)
            {
                theJournal.DisplayAll();
            }
            else if (userChoice == 3)
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine();
                theJournal.LoadFromFile(filename);
            }
            else if (userChoice == 4)
            {
                Console.Write("What is the filename? ");
                string filename = Console.ReadLine();
                theJournal.SaveToFile(filename);
            }
            else if (userChoice == 5)
            {
                Console.Write("What would you like to search for? ");
                string searchTerm = Console.ReadLine();
                theJournal.SearchEntries(searchTerm);
            }
        }
    }
}