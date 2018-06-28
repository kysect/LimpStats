using System.Collections.Generic;

namespace ElimpParse.Model
{
    public class TaskGroupInfo
    {
        public TaskGroupInfo(string groupTitle, List<int> taskIdList)
        {
            GroupTitle = groupTitle;
            TaskIdList = taskIdList;
        }

        public TaskGroupInfo(string groupTitle, List<int> taskIdList, int fullSolutionPoints)
        {
            GroupTitle = groupTitle;
            TaskIdList = taskIdList;
            FullSolutionPoints = fullSolutionPoints;
        }

        public string GroupTitle { get; set; }
        public List<int> TaskIdList { get; set; }
        public int? FullSolutionPoints { get; set; }
    }
}