using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.Blocks
{
    public partial class ResultGridBlock : UserControl
    {
        //TODO: remove variables
        private StudyGroup _group;
        private string _packTitle;
        public ResultGridBlock(StudyGroup group, string packTitle)
        {
            InitializeComponent();
            _group = group;
            _packTitle = packTitle;
            ProblemPackInfo pack = _group.ProblemPackList.Find(e => e.PackTitle == _packTitle);
            DataTable dt = InitDataTable(pack, group.UserList);
            
            ResGrid.ItemsSource = dt.DefaultView;
        }

        private DataTable InitDataTable(ProblemPackInfo pack, List<LimpUser> users)
        {
            var dt = new DataTable();
            dt.Columns.Add("Name");
            foreach (int item in pack.ProblemIdList)
            {
                dt.Columns.Add(item.ToString());
            }


            foreach (LimpUser user in users)
            {
                var data = new List<object> {user.Username};
                foreach (int problemNum  in pack.ProblemIdList)
                {
                    data.Add(user.UserProfileResult[problemNum]);
                }

                dt.Rows.Add(data.ToArray());
            }

            return dt;
        }
    }
}
