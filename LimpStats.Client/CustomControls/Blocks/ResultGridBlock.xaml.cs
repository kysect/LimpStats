using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LimpStats.Model;

namespace LimpStats.Client.CustomControls.Blocks
{
    /// <summary>
    /// Логика взаимодействия для ResultGridBlock.xaml
    /// </summary>
    public partial class ResultGridBlock : UserControl
    {
        private StudyGroup _group;
        private string _packTitle;
        public ResultGridBlock(StudyGroup group, string packTitle)
        {
            InitializeComponent();
            _group = group;
            _packTitle = packTitle;
            var pack = _group.ProblemPackList.Find(e => e.PackTitle == _packTitle);
            DataTable dt = new DataTable();
            foreach (var item in pack.ProblemIdList)
            {
                dt.Columns.Add(item.ToString());
            }
            foreach (var item in _group.UserList)
            {
                dt.Rows.Add(item.Username);
            }

            int i = 0;
            foreach (var user in _group.UserList)
            {
                for (int j = 0; j < pack.ProblemIdList.Count; j++)
                {
                    dt.Rows[i].ItemArray[j] = user.UserProfileResult.First(e => e.Key == pack.ProblemIdList[j]);
                }
                i++;
            }
            ResGrid.ItemsSource = dt.DefaultView;
        }
    }
}
