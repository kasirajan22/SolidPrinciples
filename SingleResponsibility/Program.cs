namespace SingleResponsibility
{
    // here we seprated journal and persistance, so its using single responsiblity principle.
    public class Journal
    {
        private readonly List<string> entries = new List<string>();
        private static int count = 0;

        public int AddEntries(string text)
        {
            entries.Add($"{++count} : {text}");
            return count;
        }
        public void RemoveEntries()
        {
            entries.RemoveAt(count);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
    }

    public class Persistance
    {
        public void SaveToFile(Journal journal, string fileName, bool overwrite = false)
        {
            if(overwrite || !File.Exists(fileName)) 
                File.WriteAllText(fileName, journal.ToString());
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            journal.AddEntries("Welcome");
            journal.AddEntries("Test");
            Console.WriteLine(journal.ToString());

            Persistance persistance = new Persistance();
            persistance.SaveToFile(journal, @"c:\Videos\Demo.txt", true);
        }
    }
}
