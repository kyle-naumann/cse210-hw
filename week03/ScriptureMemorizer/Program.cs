using System;
using System.Collections.Generic;

/*
 * EXCEEDING REQUIREMENTS:
 * Multiple scripture library - Users can choose from several pre-loaded scriptures
 * instead of being limited to just one hardcoded scripture.
 */

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptureLibrary = LoadScriptureLibrary();
        
        Console.WriteLine("Welcome to the Scripture Memorizer!");
        Console.WriteLine("Choose a scripture to memorize:");
        
        for (int i = 0; i < scriptureLibrary.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {scriptureLibrary[i].GetDisplayText().Split(' ')[0]} {scriptureLibrary[i].GetDisplayText().Split(' ')[1]}");
        }
        
        Console.Write("Enter your choice (1-3): ");
        int choice = int.Parse(Console.ReadLine()) - 1;
        Scripture selectedScripture = scriptureLibrary[choice];
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine(selectedScripture.GetDisplayText());
            Console.WriteLine();
            
            if (selectedScripture.IsCompletelyHidden())
            {
                Console.WriteLine("Congratulations! You've memorized the entire scripture!");
                break;
            }
            
            Console.WriteLine("Press enter to continue or type 'quit' to finish:");
            
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                break;
            }
            
            selectedScripture.HideRandomWords(3);
        }
    }
    
    static List<Scripture> LoadScriptureLibrary()
    {
        List<Scripture> scriptures = new List<Scripture>();
        
        Reference ref1 = new Reference("John", 3, 16);
        Scripture scripture1 = new Scripture(ref1, "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");
        
        Reference ref2 = new Reference("Proverbs", 3, 5, 6);
        Scripture scripture2 = new Scripture(ref2, "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.");
        
        Reference ref3 = new Reference("1 Nephi", 3, 7);
        Scripture scripture3 = new Scripture(ref3, "And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them.");
        
        scriptures.Add(scripture1);
        scriptures.Add(scripture2);
        scriptures.Add(scripture3);
        
        return scriptures;
    }
}