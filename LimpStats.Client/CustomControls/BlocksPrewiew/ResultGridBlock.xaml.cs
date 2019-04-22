using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Model;
using LimpStats.Model.Problems;
using System.IO;
using OfficeOpenXml;
using Microsoft.Win32;
using System.Linq;

namespace LimpStats.Client.CustomControls.BlocksPrewiew
{
    public partial class ResultGridBlock : UserControl
    {
        private ProblemsPack _pack;
        private List<LimpUser> _users;
        public ResultGridBlock(List<LimpUser> users, ProblemsPack pack)
        {
            InitializeComponent();
            _pack = pack;
            _users = users;
            DataTable table = InitDataTable();
            
            ResGrid.ItemsSource = table.DefaultView;
        }

        private DataTable InitDataTable()
        {
            var table = new DataTable();

            //TODO: Нужно будет потом все подобные строки вынести отдельно, чтобы изменять можно было нормально
            table.Columns.Add("Name");
            foreach (Problem problem in _pack.Problems)
            {
                table.Columns.Add(problem.Title);
            }
            table.Columns.Add("Sum");
            foreach (LimpUser user in _users)
            {
                var data = new List<object> {user.Username};
                foreach (Problem problem  in _pack.Problems)
                {
                    data.Add(problem.GetUserResult(user));
                }
                //знаю что дичь, оставлю TODO
                data.Add(_pack.GetResults(_users).Find(e => e.Username == user.Username).SumOfPoint);
                table.Rows.Add(data.ToArray());
            }

            return table;
        }

        private void SaveToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            Core.Tools.Tools.SaveToExcel(_pack, _users);
        }
        
    }
}
