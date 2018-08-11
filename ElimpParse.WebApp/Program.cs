using System;
using System.IO;
using System.Linq;
using ElimpParse.Core;
using ElimpParse.DatabaseProvider;
using ElimpParse.Model;
using Newtonsoft.Json;
namespace ElimpParse.WebApp
{
    static class Program
    {
        static void Main(string[] args)
        {
            FinalTest();
        }

        private static void LoadAllPackInfo()
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            group.LoadStudentsResult();

            //var result = group.GetPackResult(group.ProblemPackList.First());
            var res = group.GetAllPackResult();
                    
            var jsonString = JsonConvert.SerializeObject(res);
            Console.WriteLine(jsonString);
        }

        private static void FinalTest()
        {
            StudyGroup group = DataGenerator.GenerateTemplateGroup();
            group.LoadStudentsResult();
            var res = group.GetPackResult(group.ProblemPackList.Last());

            foreach (var packResult in res)
            {
                if (packResult.ProblemResultList[0] == 27)
                {
                    packResult.ProblemResultList[0] = 100;
                }
            }
            var jsonString = JsonConvert.SerializeObject(res);
            Console.WriteLine(jsonString);
        }
    }
}
