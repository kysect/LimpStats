using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimpStats.Client
{
    class GridCard
    {
        public GridCard(string Name, string Nickname, int TotalPoints)
        {
            this.Name = Name;
            this.Nickname = Nickname;
            this.TotalPoints = TotalPoints;
        }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public int TotalPoints { get; set; }
    }
}
