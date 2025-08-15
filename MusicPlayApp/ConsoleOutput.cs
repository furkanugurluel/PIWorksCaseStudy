using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlayApp
{
    // Displays results to console
    public class ConsoleOutput
    {
        public void DisplayResults(Dictionary<int, int> result, int usersWith346Songs, int maxDistinctSongs)
        {
            DisplayQuestion1(result);
            DisplayQuestion2(usersWith346Songs);
            DisplayQuestion3(maxDistinctSongs);
        }

        private void DisplayQuestion1(Dictionary<int, int> result)
        {
            Console.WriteLine("=== Q1: DISTINCT_PLAY_COUNT,CLIENT_COUNT ===");
            foreach (var kvp in result.OrderBy(k => k.Key))
            {
                Console.WriteLine($"{kvp.Key},{kvp.Value}");
            }
        }

        private void DisplayQuestion2(int usersWith346Songs)
        {
            Console.WriteLine($"\n=== Q2: How many users played 346 distinct songs? ===");
            Console.WriteLine($"{usersWith346Songs}");
        }

        private void DisplayQuestion3(int maxDistinctSongs)
        {
            Console.WriteLine($"\n=== Q3: What is the maximum number of distinct songs played? ===");
            Console.WriteLine($"{maxDistinctSongs}");
        }


    }
}
