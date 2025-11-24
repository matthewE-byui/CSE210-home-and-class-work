using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    public class Journal
    {
        private List<Entry> _entries = new List<Entry>();

        public void AddEntry(Entry entry)
        {
            _entries.Add(entry);
        }

        public void DisplayAll()
        {
            if (_entries.Count == 0)
            {
                Console.WriteLine("No entries yet.\n");
                return;
            }

            foreach (Entry e in _entries)
            {
                e.Display();
            }
        }

        public void SaveToFile(string filename)
        {
            string savedJournalsPath = Path.Combine("saved_journals");
            
            if (!Directory.Exists(savedJournalsPath))
            {
                Directory.CreateDirectory(savedJournalsPath);
            }

            string fullPath = Path.Combine(savedJournalsPath, filename);
            
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                foreach (Entry e in _entries)
                {
                    writer.WriteLine($"{e.Date}|{e.Prompt}|{e.Response}");
                }
            }
            Console.WriteLine($"Journal saved successfully to saved_journals/{filename}!");
        }

        public void LoadFromFile(string filename)
        {
            string savedJournalsPath = Path.Combine("saved_journals");
            string fullPath = Path.Combine(savedJournalsPath, filename);

            if (!File.Exists(fullPath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            _entries.Clear();
            string[] lines = File.ReadAllLines(fullPath);

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    Entry entry = new Entry(parts[0], parts[1], parts[2]);
                    _entries.Add(entry);
                }
            }

            Console.WriteLine($"\nJournal loaded successfully from saved_journals/{filename}!\n");
        }
    }
}
