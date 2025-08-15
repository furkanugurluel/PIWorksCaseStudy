using System;
using System.Collections.Generic;
using System.IO;

namespace MusicPlayApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "MusicPlayApp/exhibitA-input.csv";

            if (!File.Exists(inputPath))
            {
                Console.WriteLine("File not found: " + inputPath);
                return;
            }

            // Reading CSV file and creating user-song mappings
            var fileReader = new FileReader();
            var clientSongMap = fileReader.ReadMusicPlayData(inputPath);

            // Analyzing how many distinct songs each user played
            var dataAnalyzer = new DataAnalyzer();
            var result = dataAnalyzer.AnalyzeDistinctSongCounts(clientSongMap);
            
            // Calculating values needed for questions
            int usersWith346Songs = dataAnalyzer.GetUsersWithSpecificSongCount(result, 346);
            int maxDistinctSongs = dataAnalyzer.GetMaximumDistinctSongs(result);

            // Displaying results to console
            var consoleOutput = new ConsoleOutput();
            consoleOutput.DisplayResults(result, usersWith346Songs, maxDistinctSongs);
        }
    }
}
