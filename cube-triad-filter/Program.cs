﻿using System;
using System.IO;
using System.Linq;

class Program
{
    private static int _maxRed = 12;
    private static int _maxGreen = 13;
    private static int _maxBlue = 14;
    
    static void Main(string[] args)
    {
        string calibrationDocument =
            "/home/tom/RiderProjects/cube-triad-filter/cube-triad-filter/Dataset.txt";
            
        string[] games = File.ReadAllLines(calibrationDocument);
        int sumOfValidGameIds = 0;
        int sumOfGameMinPowers = 0;

        foreach (var currentGame in games)
        {
            var gameId = Int32.Parse(currentGame.Substring(0, currentGame.IndexOf(':')).Split(" ")[1]);

            var gameData = currentGame.Split(": ")[1];

            bool gameValid = true; //assume every game is possible until proven otherwise

            var gameRounds = gameData.Split("; ");
            int minRed = 0, minBlue = 0, minGreen = 0;
            int roundIndex = 0;
            foreach (var round in gameRounds)
            {
                var colours = round.Split(", ");
                int redCount = 0, blueCount = 0, greenCount = 0;
                foreach (var colour in colours)
                {
                    var colourName = colour.Split(" ")[1];
                    var colourCount = Int32.Parse(colour.Split(" ")[0]);

                    switch (colourName)
                    {
                        case "red":
                            redCount = colourCount;
                            break;
                        case "blue":
                            blueCount = colourCount;
                            break;
                        case "green":
                            greenCount = colourCount;
                            break;
                    }
                }
                
                if (roundIndex == 0)
                {
                    minRed = redCount;
                    minBlue = blueCount;
                    minGreen = greenCount;
                }

                //Set the minimum amount of colours that must have been in that game
                if (redCount > minRed) minRed = redCount;
                if (blueCount > minBlue) minBlue = blueCount;
                if (greenCount > minGreen) minGreen = greenCount;
                
                roundIndex ++;
                
                var validRound = IsValidRound(redCount, blueCount, greenCount);
                if (gameValid)
                {
                    if (!validRound) gameValid = false;
                }
            }
            
            if (gameValid)
            {   
                sumOfValidGameIds += gameId;
            }

            sumOfGameMinPowers += (minRed * minBlue * minGreen);
            Console.WriteLine($"The Lowest possible amount for each colour in game {gameId}: Red: {minRed} , Blue: {minBlue}, Green: {minGreen}");
        }
        
        
        Console.WriteLine($"Sum of valid IDs: {sumOfValidGameIds}"); //Answer to the problem (Advent of code day 2, part 1, 2023
        Console.WriteLine($"Sum of powers in games: {sumOfGameMinPowers}"); //Answer to the problem (Advent of code day 2, part 2, 2023
    }

    static bool IsValidRound(int red, int blue, int green)
    {
        if (red > _maxRed) return false;
        if (blue > _maxBlue) return false;
        if (green > _maxGreen) return false;
        return true;
    }
}

