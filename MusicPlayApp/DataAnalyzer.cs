using System.Collections.Generic;
using System.Linq;

namespace MusicPlayApp
{
    // Calculates the number of users based on the number of distinct songs each user played
    public class DataAnalyzer
    {
        public Dictionary<int, int> AnalyzeDistinctSongCounts(Dictionary<int, HashSet<int>> clientSongMap)
        {
            // Creating mapping structure from song count to user count
            var result = new Dictionary<int, int>();
            
            foreach (var kvp in clientSongMap)
            {
                int distinctCount = kvp.Value.Count;
                
                // If this song count hasn't been seen before, create new entry
                if (!result.ContainsKey(distinctCount))
                    result[distinctCount] = 0;
                
                // Increase the number of users with this song count
                result[distinctCount]++;
            }

            return result;
        }

        // Returns the number of users who played a specific number of songs
        public int GetUsersWithSpecificSongCount(Dictionary<int, int> result, int songCount)
        {
            return result.ContainsKey(songCount) ? result[songCount] : 0;
        }

        // Returns the maximum number of distinct songs
        public int GetMaximumDistinctSongs(Dictionary<int, int> result)
        {
            return result.Keys.Count > 0 ? result.Keys.Max() : 0;
        }
    }
}
