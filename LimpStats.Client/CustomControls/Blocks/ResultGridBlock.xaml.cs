using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.Blocks
{
    public partial class ResultGridBlock : UserControl
    {
        public ResultGridBlock(List<LimpUser> users, ProblemPackInfo pack)
        {
            InitializeComponent();
            DataTable table = InitDataTable(pack, users);
            
            ResGrid.ItemsSource = table.DefaultView;
        }

        private DataTable InitDataTable(ProblemPackInfo pack, List<LimpUser> users)
        {
            var table = new DataTable();

            table.Columns.Add("Name");
            foreach (int item in pack.ProblemIdList)
            {
                table.Columns.Add(item.ToString());
            }

            foreach (LimpUser user in users)
            {
                var data = new List<object> {user.Username};
                foreach (int problemNum  in pack.ProblemIdList)
                {
                    data.Add(user.UserProfileResult[problemNum]);
                }

                table.Rows.Add(data.ToArray());
            }

            return table;
        }
    }
}
