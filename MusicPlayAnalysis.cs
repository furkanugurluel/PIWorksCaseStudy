
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string filePath = "input.txt"; // input file path

        var clientSongMap = new Dictionary<string, HashSet<string>>();

        foreach (var line in File.ReadLines(filePath).Skip(1)) // skip header
        {
            var parts = line.Split('\t');
            if (parts.Length < 4) continue;

            string clientId = parts[2].Trim();
            string songId = parts[1].Trim();
            string playTs = parts[3].Trim();

            if (playTs.StartsWith("10/08/2016"))
            {
                if (!clientSongMap.ContainsKey(clientId))
                    clientSongMap[clientId] = new HashSet<string>();

                clientSongMap[clientId].Add(songId);
            }
        }

        var result = new Dictionary<int, int>();

        foreach (var entry in clientSongMap)
        {
            int distinctCount = entry.Value.Count;
            if (!result.ContainsKey(distinctCount))
                result[distinctCount] = 0;
            result[distinctCount]++;
        }

        Console.WriteLine("DISTINCT_PLAY_COUNT,CLIENT_COUNT");
        foreach (var kvp in result.OrderBy(k => k.Key))
        {
            Console.WriteLine($"{kvp.Key},{kvp.Value}");
        }
    }
}
