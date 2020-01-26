using Gameloop.Vdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameSelector
{
    public static class Collector
    {
        public static List<string> GetLibraries(string libFile)
        {
            var libraries = new List<string>();

            if (File.Exists(libFile))
            {
                dynamic steam = VdfConvert.Deserialize(File.ReadAllText(libFile));
                var pattern = @"^\d$";
                var rg = new Regex(pattern);
                foreach (var item in steam.Value.Children())
                {
                    if (rg.IsMatch(item.Key))
                    {
                        libraries.Add(Path.Combine(item.Value.ToString(), "steamapps"));
                    }
                }
            }


            return libraries;
        }

        public static List<string> GetGames(List<string> libs)
        {
            var games = new List<string>();

            foreach (var dir in libs)
            {
                if (Directory.Exists(dir))
                {
                    DirectoryInfo d = new DirectoryInfo(dir);
                    FileInfo[] files = d.GetFiles("appmanifest*.acf");

                    foreach (var file in files)
                    {
                        dynamic info = VdfConvert.Deserialize(File.ReadAllText(file.FullName));
                        games.Add(info.Value.name.ToString());
                    }
                }
            }

            return games;
        }

    }
}
