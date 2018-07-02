using System;
using System.IO;
using System.Linq;
using ElimpParse.Core;
using ElimpParse.DatabaseProvider;
using ElimpParse.Model;
using Newtonsoft.Json;
namespace ElimpParse.WebApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadAllPackInfo();
        }

        private static void LoadAllPackInfo()
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            group.LoadStudentsResultMultiThread();

            //var result = group.GetPackResult(group.ProblemPackList.First());
            var res = group.GetAllPackResult().Select(r => r.results);
                    
            var jsonString = JsonConvert.SerializeObject(res);
            Console.WriteLine(jsonString);
        }
    }
}
