// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
string calibrationDocument =
    "/home/tom/RiderProjects/cube-triad-filter/cube-triad-filter/Dataset.txt";
    
string[] games = File.ReadAllLines(calibrationDocument);
foreach (var currentGame in games)
{
    Console.WriteLine("-------------------------");

    var gameNumber = currentGame.Substring(0, currentGame.IndexOf(':')).Split(" ")[1];
    Console.WriteLine($"Current Game Number: {gameNumber}");

    var gameData = currentGame.Split(": ")[1];
    Console.WriteLine(gameData);

    var gameRounds = gameData.Split("; ");
    foreach (var round in gameRounds)
    {
        Console.WriteLine($"Round data: {round}");
    }
    
    Console.WriteLine("-------------------------");

}
