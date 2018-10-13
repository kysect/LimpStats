using System.Collections.Generic;

namespace LimpStats.Model
{
    public class ProblemPackInfo
    {
        public ProblemPackInfo(string packTitle, List<int> problemIdList, int fullSolutionPoints = 0)
        {
            PackTitle = packTitle;
            ProblemIdList = problemIdList;
            FullSolutionPoints = fullSolutionPoints;
        }

        //TODO: remove set;
        public string PackTitle { get; set; }
        public List<int> ProblemIdList { get; set; }
        public int FullSolutionPoints { get; set; }
    }
}