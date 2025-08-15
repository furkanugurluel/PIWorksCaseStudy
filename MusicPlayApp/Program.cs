using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "exhibitA-input.csv";
        string outputPath = "output.csv";
        
        if (!File.Exists(inputPath))
        {
            inputPath = "MusicPlayApp/exhibitA-input.csv";
            outputPath = "MusicPlayApp/output.csv";
        }

        ReadOnlySpan<char> targetDate = "10/08/2016".AsSpan();

        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Dosya bulunamadı: " + inputPath);
            return;
        }

        var sw = Stopwatch.StartNew();

        var clientSongMap = new Dictionary<int, HashSet<int>>(capacity: 4096);

        const int bufferSize = 1 << 20;
        using (var fs = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize))
        using (var reader = new StreamReader(fs))
        {
            string? line;
            bool isFirst = true;

            while ((line = reader.ReadLine()) != null)
            {
                if (isFirst) { isFirst = false; continue; }

                var parts = line.Split('\t');
                if (parts.Length < 4) continue;

                if (!parts[3].AsSpan().StartsWith(targetDate, StringComparison.Ordinal)) continue;

                if (!int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int songId)) continue;
                if (!int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out int clientId)) continue;

                if (!clientSongMap.TryGetValue(clientId, out var set))
                {
                    set = new HashSet<int>();
                    clientSongMap[clientId] = set;
                }
                set.Add(songId);
            }
        }

        var result = new Dictionary<int, int>();
        foreach (var kvp in clientSongMap)
        {
            int distinctCount = kvp.Value.Count;
            if (!result.ContainsKey(distinctCount))
                result[distinctCount] = 0;
            result[distinctCount]++;
        }

        Console.WriteLine("=== Q1: DISTINCT_PLAY_COUNT,CLIENT_COUNT ===");
        foreach (var kvp in result.OrderBy(k => k.Key))
            Console.WriteLine($"{kvp.Key},{kvp.Value}");

        int usersWith346Songs = result.ContainsKey(346) ? result[346] : 0;
        Console.WriteLine($"\n=== Q2: How many users played 346 distinct songs? ===");
        Console.WriteLine($"{usersWith346Songs}");

        int maxDistinctSongs = result.Keys.Count > 0 ? result.Keys.Max() : 0;
        Console.WriteLine($"\n=== Q3: What is the maximum number of distinct songs played? ===");
        Console.WriteLine($"{maxDistinctSongs}");

        using (var writer = new StreamWriter(outputPath))
        {
            writer.WriteLine("DISTINCT_PLAY_COUNT,CLIENT_COUNT");
            foreach (var kvp in result.OrderBy(k => k.Key))
                writer.WriteLine($"{kvp.Key},{kvp.Value}");
            
            writer.WriteLine();
            writer.WriteLine("Q2: How many users played 346 distinct songs?");
            writer.WriteLine($"{usersWith346Songs}");
            writer.WriteLine();
            writer.WriteLine("Q3: What is the maximum number of distinct songs played?");
            writer.WriteLine($"{maxDistinctSongs}");
        }

        sw.Stop();
        long bytes = GC.GetTotalMemory(forceFullCollection: false);

        Console.WriteLine("Çıktı dosyaya yazıldı: " + outputPath);
        Console.WriteLine($"Süre: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine($"Yaklaşık bellek kullanımı: {bytes / (1024.0 * 1024.0):F2} MB");
    }
}
