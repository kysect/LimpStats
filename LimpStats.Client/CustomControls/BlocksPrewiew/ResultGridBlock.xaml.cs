using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using LimpStats.Model;
using LimpStats.Model.Problems;
using System.IO;
using OfficeOpenXml;
using Microsoft.Win32;

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

            foreach (LimpUser user in _users)
            {
                var data = new List<object> {user.Username};
                foreach (Problem problem  in _pack.Problems)
                {
                    data.Add(problem.GetUserResult(user));
                }

                table.Rows.Add(data.ToArray());
            }

            return table;
        }

        private void SaveToExcelButton_Click(object sender, RoutedEventArgs e)
        {
      
           
            using (var excel = new ExcelPackage(new FileInfo(SaveFileDialog() + ".xlsx")))
            {
                var ws = excel.Workbook.Worksheets.Add("StudentGroup");
                int i = 2;
                foreach (Problem problem in _pack.Problems)
                {
                    ws.Cells[1, i].Value = problem.Title;
                    i++;
                }
                i = 2;
                foreach (LimpUser user in _users)
                {
                    int k = 2;
                    ws.Cells[i, 1].Value = user.Username;
                    foreach (Problem problem in _pack.Problems)
                    {
                        ws.Cells[i, k].Value = problem.GetUserResult(user);
                        k++;
                    }           
                    i++;
                }
               
                excel.Save();
            }


        }
        private static string SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                return saveFileDialog.FileName;
            }
            return null;
        }
    }
}
