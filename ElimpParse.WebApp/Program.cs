using System;
using System.IO;
using System.Linq;
using LimpStats.Core;
using LimpStats.Core.Parsers;
using LimpStats.Database;
using LimpStats.Model;
using Newtonsoft.Json;
namespace ElimpParse.WebApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            var jsonData = FinalTest();
            Console.WriteLine(jsonData);
        }

        private static string LoadAllPackInfo()
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            MultiThreadParser.LoadProfiles(group);
            var res = group.GetAllPackResult();
                    
            return JsonConvert.SerializeObject(res);
        }

        private static string FinalTest()
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            MultiThreadParser.LoadProfiles(group);
            var res = group.GetPackResult(group.ProblemPackList.Last());
            
            return JsonConvert.SerializeObject(res);
        }
    }
}
