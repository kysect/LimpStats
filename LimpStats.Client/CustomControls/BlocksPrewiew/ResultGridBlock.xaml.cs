using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using LimpStats.Model;
using LimpStats.Model.Problems;

namespace LimpStats.Client.CustomControls.Blocks
{
    public partial class ResultGridBlock : UserControl
    {
        public ResultGridBlock(List<LimpUser> users, ProblemsPack pack)
        {
            InitializeComponent();
            DataTable table = InitDataTable(pack, users);
            
            ResGrid.ItemsSource = table.DefaultView;
        }

        private DataTable InitDataTable(ProblemsPack pack, List<LimpUser> users)
        {
            var table = new DataTable();

            table.Columns.Add("Name");
            foreach (Problem problem in pack.Problems)
            {
                table.Columns.Add(problem.Title);
            }

            foreach (LimpUser user in users)
            {
                var data = new List<object> {user.Username};
                foreach (Problem problem  in pack.Problems)
                {
                    data.Add(problem.GetUserResult(user));
                }

                table.Rows.Add(data.ToArray());
            }

            return table;
        }
    }
}
