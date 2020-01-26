using System;
using System.IO;

namespace GameSelector
{
    class Entry
    {
        static readonly Random rnd = new Random();

        static void Main(string[] args)
        {
            var libFile = Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles(x86)"), "steam", "steamapps", "libraryfolders.vdf");
            var libs = Collector.GetLibraries(libFile);
            libs.Add(Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles(x86)"), "steam", "steamapps"));

            var games = Collector.GetGames(libs);

            var exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Options:\n\t ENTER: Select a random steam game\n\tESCAPE: exit");
                var input = Console.ReadKey();

                if (input.Key == ConsoleKey.Enter)
                {
                    int r = rnd.Next(games.Count);
                    Console.WriteLine(games[r]);
                    input = Console.ReadKey();
                }

                if (input.Key == ConsoleKey.Escape)
                {
                    exit = true;
                }
            }
        }
    }
}
