using System.Collections.Generic;

namespace ElimpParse.Model
{
    public class TaskGroupInfo
    {
        public string GroupTitle { get; }
        public List<int> TaskIdList { get; }
        public int? FullSolutionPoints { get; }

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
    }
}