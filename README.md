# Music Play Counter

## Project Description
This project is a C# console application that analyzes music play data. It counts how many different songs each user played on a specific date and performs statistical analysis.

## Requirements
- .NET 8.0 SDK or higher

## Installation and Usage

1. Download the project
2. Run the following command:
   ```
   dotnet run --project MusicPlayApp
   ```

## Input Data Format
The CSV file contains the following columns:
- Column 1: Record ID
- Column 2: Song ID  
- Column 3: User ID
- Column 4: Date (in DD/MM/YYYY format)

## Output
The program produces the following results:
1. Number of users for each distinct song count
2. Number of users who played 346 distinct songs
3. Maximum number of distinct songs played

Results are displayed in console and saved to output.csv file.
