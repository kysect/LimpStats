using LimpStats.Model;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using OfficeOpenXml;
using LimpStats.Model.Problems;
using System.Collections.Generic;

namespace LimpStats.Core.Tools
{
    public static class Tools
    {
        //public static string GenerateNextNumber(int position)
        //{
        //   
        //    const int size = 26;
        //    string res = "";
        //    do
        //    {
        //        res += (char)('A' + position % size);
        //        position /= size;
        //    } while (position > size);

        //    return res;
        //}

        //TODO: Burn in hell
        public static string GenerateNextNumber(string number)
        {
            if (number == "")
                return "A";
            char[] n = number.ToCharArray();
            n[number.Length - 1]++;
            var s = string.Empty;
            if (n[number.Length - 1] > 'Z')
            {
                for (int i = 0; i < number.Length; i++)
                    s += "A";
                s += "A";
            }
            foreach (char i in n)
            {
                s += i.ToString();
            }
            return s;
        }

        public static  bool CheckInternetConnect()
        {
            try
            {
                var ping = new Ping();
                PingReply result = ping.Send("1.1.1.1", 5000);
                return result?.Status == IPStatus.Success;
            }
            catch (WebException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public static void SaveToExcel(ProblemsPack pack, List<LimpUser> users, string fileName)
        {
            using (var excel = new ExcelPackage(new FileInfo(fileName)))
            {
                var ws = excel.Workbook.Worksheets.Add("StudentGroup");
                int i = 2;
                foreach (Problem problem in pack.Problems)
                {
                    ws.Cells[1, i].Value = problem.Title;
                    i++;
                }
                i = 2;
                foreach (LimpUser user in users)
                {
                    int k = 2;
                    ws.Cells[i, 1].Value = user.Username;
                    foreach (Problem problem in pack.Problems)
                    {
                        ws.Cells[i, k].Value = problem.GetUserResult(user);
                        k++;
                    }
                    i++;
                }

                excel.Save();
            }
        }
    }
}
