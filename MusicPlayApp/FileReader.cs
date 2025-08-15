using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MusicPlayApp
{
    public class FileReader
    {
        private const int BufferSize = 1 << 20;
        private const string TargetDate = "10/08/2016";

        // Reads CSV file and creates user-song mappings
        public Dictionary<int, HashSet<int>> ReadMusicPlayData(string inputPath)
        {
            var clientSongMap = new Dictionary<int, HashSet<int>>(capacity: 4096);

            using var fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize);
            using var reader = new StreamReader(fileStream);

            string? line;
            bool isFirst = true;

            while ((line = reader.ReadLine()) != null)
            {
                if (isFirst) 
                { 
                    isFirst = false; // Skip header row
                    continue; 
                }

                ProcessLine(line, clientSongMap);
            }

            return clientSongMap;
        }

        private void ProcessLine(string line, Dictionary<int, HashSet<int>> clientSongMap)
        {
            var parts = line.Split('\t');
            if (parts.Length < 4) return;

            if (!IsTargetDate(parts[3])) return;

            if (!TryParseIds(parts, out int songId, out int clientId)) return; 

            AddSongToClient(clientSongMap, clientId, songId);
        }
        
        // Performs target date check
        private bool IsTargetDate(string dateString)
        {
            return dateString.AsSpan().StartsWith(TargetDate, StringComparison.Ordinal); 
        }

        // Parses song and user IDs
        private bool TryParseIds(string[] parts, out int songId, out int clientId) 
        {
            songId = 0;
            clientId = 0;

            if (!int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out songId)) 
                return false;
            
            if (!int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out clientId)) 
                return false;

            return true;
        }

        // Maps song IDs to user IDs
        private void AddSongToClient(Dictionary<int, HashSet<int>> clientSongMap, int clientId, int songId)
        {
            if (!clientSongMap.TryGetValue(clientId, out var songSet))
            {
                songSet = new HashSet<int>();
                clientSongMap[clientId] = songSet;
            }
            songSet.Add(songId);
        }
    }
}
