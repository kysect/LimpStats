using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElimpParse
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> names = new List<string>()
            {
                "Andrey2005",
                "DDsov",
                "Den4758",
                "Gladtoseeyou",
                "Koteika",
                "liza.898",
                "Mr.Hovik",
                "NastyaVadko284",
                "papercut6820",
                "Pozitiv4ik",
                "prostoroma",
                "Swoop",
                "v_7946",
                "Versuzzz",
                "Xsqten",
                "Enosha"
            };
            List<string> oldPlayers = new List<string>
            {
                "Strannik",
                "iNooByX",
                "Maxkolpak",
                "krab397",
                "i4happy",
                "vlad986523"
            };
            names.InsertRange(0, oldPlayers);
            PrintResult(names, false);
            
        }

        public static void PrintResult(List<string> names, bool isHtml)
        {
            List<(string login, int count)> result = new List<(string, int)>();
            foreach (var name in names)
            {
                result.Add((name, Parser.ComplitedTaskCount(name)));
            }

            var sorted = result.OrderByDescending(e => e.count).ToList();
            for (var i = 0; i < sorted.Count; i++)
            {
                if (isHtml)
                {
                    Console.WriteLine($"<tr><td>{i}.</td><td>{sorted[i].login}</td><td>{sorted[i].count}</td></tr>");
                }
                else
                {
                    Console.WriteLine($"{i}. {sorted[i].login} ({sorted[i].count})");
                }
            }
        }
    }
}
