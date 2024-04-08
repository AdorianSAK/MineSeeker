using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Player
{
    public string Name { get; set; }
    public int Score { get; set; }
}

public partial class MainGameCode
{
    static bool IsHighScore(int newScore)
    {
        //  Read existing players scores
        List<Player> players = ReadPlayerScores("/home/hunish/Desktop/coding/MineSeeker/MineSeekerPrj/scores.txt");

        //  Get the lowest score in the current records
        int lowestScore = players.Any() ? players.Min(p => p.Score) : int.MinValue;

        //  Check if the new score is greater than the lowest score
        return newScore > lowestScore;
    }

    static void ScoreRecorder()
    {
        // Read player scores from the text file
        List<Player> players = ReadPlayerScores("/home/hunish/Desktop/coding/MineSeeker/MineSeekerPrj/scores.txt");

        // Order players by high score
        var sortedPlayers = players.OrderByDescending(p => p.Score);

        // Display the scoreboard
        Console.WriteLine("Scoreboard:");
        foreach (var player in sortedPlayers)
        {
            Console.WriteLine($"{player.Name}:\t{player.Score}");
        }
    }

    static List<Player> ReadPlayerScores(string filepath)
    {
        List<Player> players = new List<Player>();

        try
        {
            // Read player scores from the text file
            string[] lines = File.ReadAllLines(filepath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                {
                    players.Add(new Player { Name = parts[0], Score = score });
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }

        return players;
    }

    static void RegisterPlayer(string playerName, int score)
    {
        // Read existing player scores
        List<Player> players = ReadPlayerScores("/home/hunish/Desktop/coding/MineSeeker/MineSeekerPrj/scores.txt");

        // Register the new player or update the existing player's score
        /*
        Player existingPlayer = players.FirstOrDefault(p => p.Name == playerName);

        if (existingPlayer != null)
        {
            // Player exists, update the score if it's higher
            if (score > existingPlayer.Score)
            {
                existingPlayer.Score = score;
            }
        }
        else
        {
            // Player doesn't exist, add them to the list
            players.Add(new Player { Name = playerName, Score = score });
        }
        */

        players.Add(new Player { Name = playerName, Score = score });

        // Order players by high score
        players = players.OrderByDescending(p => p.Score).ToList();

        // Keep only the top 10 players
        players = players.Take(10).ToList();

        // Save the updated scores to the text file
        SavePlayerScores("/home/hunish/Desktop/coding/MineSeeker/MineSeekerPrj/scores.txt", players);
    }

    static void SavePlayerScores(string filepath, List<Player> players)
    {
        try
        {
            // Save player scores to the text file
            string[] lines = players.Select(p => $"{p.Name}:{p.Score}").ToArray();
            File.WriteAllLines(filepath, lines);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error writing file: {ex.Message}");
        }
    }
}
