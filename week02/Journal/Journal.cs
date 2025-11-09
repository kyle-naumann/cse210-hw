using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Journal
{
    public List<Entry> _entries;

    public Journal()
    {
        _entries = new List<Entry>();
    }

    public void AddEntry(Entry newEntry)
    {
        _entries.Add(newEntry);
    }

    public void DisplayAll()
    {
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SearchEntries(string searchTerm)
    {
        var foundEntries = _entries.Where(entry =>
            entry._entryText.ToLower().Contains(searchTerm.ToLower()) ||
            entry._promptText.ToLower().Contains(searchTerm.ToLower()) ||
            entry._date.Contains(searchTerm)
        ).ToList();

        if (foundEntries.Count == 0)
        {
            Console.WriteLine($"No entries found containing '{searchTerm}'");
        }
        else
        {
            Console.WriteLine($"Found {foundEntries.Count} entries containing '{searchTerm}':");
            Console.WriteLine();
            foreach (Entry entry in foundEntries)
            {
                entry.Display();
            }
        }
    }

    public void SaveToFile(string file)
    {
        using (StreamWriter outputFile = new StreamWriter(file))
        {
            foreach (Entry entry in _entries)
            {
                outputFile.WriteLine($"{entry._date}~|~{entry._promptText}~|~{entry._entryText}");
            }
        }
    }

    public void LoadFromFile(string file)
    {
        _entries.Clear();
        string[] lines = File.ReadAllLines(file);

        foreach (string line in lines)
        {
            string[] parts = line.Split("~|~");

            Entry entry = new Entry();
            entry._date = parts[0];
            entry._promptText = parts[1];
            entry._entryText = parts[2];

            _entries.Add(entry);
        }
    }
}