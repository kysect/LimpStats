using System.Collections.Generic;

namespace ElimpParse.Model
{
    public class ProblemPackInfo
    {
        public ProblemPackInfo(string packTitle, List<int> problemIdList)
        {
            PackTitle = packTitle;
            ProblemIdList = problemIdList;
        }

        public ProblemPackInfo(string packTitle, List<int> problemIdList, int fullSolutionPoints)
        {
            PackTitle = packTitle;
            ProblemIdList = problemIdList;
            FullSolutionPoints = fullSolutionPoints;
        }

        public string PackTitle { get; set; }
        public List<int> ProblemIdList { get; set; }
        public int FullSolutionPoints { get; set; }
    }
}