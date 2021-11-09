using System.Collections.Concurrent;
using System.Threading.Tasks;
using static System.Console;

namespace Concurrent_Dictionary
{
    class Program
    {
        private static ConcurrentDictionary<string, string> capitals =
            new ConcurrentDictionary<string, string>();

        public static void AddParis()
        {
            bool success = capitals.TryAdd("France", "Paris");
            string who = Task.CurrentId.HasValue ? $"Task {Task.CurrentId}" : "Main Thread";
            WriteLine($"{who} {(success?"added":"did not add")} the element");
        }
        static void Main()
        {
            Task.Factory.StartNew(AddParis).Wait();
            AddParis();

            WriteLine();

            //capitals["Russia"] = "Leningrad";
            capitals.AddOrUpdate("Russia", "Moscow",(k,old)=>$"{old} --> Moscow");
            WriteLine($"The Capital of Russia is : {capitals["Russia"]}");

            WriteLine();

            capitals["Sweden"] = "Uppsala";
            var capOfSweden = capitals.GetOrAdd("Sweden","Stockholm");
            WriteLine($"The Capital of Sweden is : {capOfSweden}");

            WriteLine();

            const string toRemove = "Russia";
            string removed;
            var didRemove = capitals.TryRemove(toRemove, out removed);
            if (didRemove)
            {
                WriteLine($"we just removed {removed}");
            }
            else
            {
                WriteLine($"Failed to remove the capitals of {toRemove}");
            }

            WriteLine();

            foreach (var kv in capitals)
            {
                WriteLine($" - {kv.Value} is Capital of {kv.Key}");
            }
            ReadKey();
        }
    }
}
