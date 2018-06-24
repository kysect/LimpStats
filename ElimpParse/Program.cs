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
            var users = new List<ElimpUser>()
            {
                new ElimpUser("Andrey2005"),
                new ElimpUser("DDsov"),
                new ElimpUser("Den4758"),
                new ElimpUser("Gladtoseeyou"),
                new ElimpUser("Koteika"),
                new ElimpUser("liza.898"),
                new ElimpUser("Mr.Hovik"),
                new ElimpUser("NastyaVadko284"),
                new ElimpUser("papercut6820"),
                new ElimpUser("Pozitiv4ik"),
                new ElimpUser("prostoroma"),
                new ElimpUser("Swoop"),
                new ElimpUser("v_7946"),
                new ElimpUser("Versuzzz"),
                new ElimpUser("Xsqten"),
                new ElimpUser("Enosha", "Нет места на всеукре :с")
            };
            var oldPlayers = new List<ElimpUser>
            {
                new ElimpUser("Strannik", "II место на области"),
                new ElimpUser("iNooByX", "III место на области"),
                new ElimpUser("Maxkolpak", "III место на городе"),
                new ElimpUser("krab397", "III место на облати"),
                new ElimpUser("i4happy", "I место на городе"),
                new ElimpUser("vlad986523", "II место на городе")
            };
            users.InsertRange(0, oldPlayers);
            PrintResult(users, false);
            
        }

        public static void PrintResult(List<ElimpUser> users, bool isHtml)
        {
            foreach (var user in users)
            {
                user.ComplitedTaskCount = Parser.ComplitedTaskCount(user.Login);
            }

            var sorted = users.OrderByDescending(e => e.ComplitedTaskCount).ToList();
            for (var i = 0; i < sorted.Count; i++)
            {
                if (isHtml)
                {
                    Console.WriteLine($"<tr><td>{i}.</td><td>{sorted[i].Login}</td><td>{sorted[i].ComplitedTaskCount}</td></tr>");
                }
                else
                {
                    Console.WriteLine($"{i}. {sorted[i]}");
                }
            }
        }
    }
}
