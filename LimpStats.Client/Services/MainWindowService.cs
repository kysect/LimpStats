using System.Collections.Generic;
using System.Linq;
using LimpStats.Client.Models;
using LimpStats.Core;
using LimpStats.Model;

namespace LimpStats.Client.Services
{
    public static class MainWindowService
    {
        //public static IEnumerable<UserCard> GetCardWithEnosha()
        //{
        //    var group = InstanceGenerator.GenerateTemplateGroup(0);
        //    group.UserList.Add(new ElimpUser { Login = "Enosha" });
        //    group.LoadProfiles();
        //    var res = group
        //        .GetTotalPoints()
        //        .Select(item => new UserCard(item.Username, item.Username, item.Points));
        //    return res;
        //}

        public static IEnumerable<ProfilePreviewData> LoadProfilePreview(StudyGroup group)
        {
            return group
                .GetTotalPoints()
                .Select(res => new ProfilePreviewData(res.Username, res.Points));
        }
    }
}