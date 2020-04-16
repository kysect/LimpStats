using LimpStats.Model;
using System;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.Win32;
using System.Windows;
using LimpStats.Model.Problems;
using System.Collections.Generic;
using System.Threading.Tasks;
using LimpStats.Core.Parsers;
using LimpStats.Database;
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

        public static string GenerateNextNumber(string number)
        {
            if (number == "")
                return "A";
            var n = number.ToCharArray();
            n[number.Length - 1]++;
            string s = "";
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
        //public static void SaveToExcel(ProfilePreviewData studentsData)
        //{
        //    var studentsData = ProfilePreviewData.GetProfilePreview(_group);
        //    using (var excel = new ExcelPackage(new FileInfo(SaveFileDialog() + ".xlsx")))
        //    {
        //        var ws = excel.Workbook.Worksheets.Add("StudentGroup");
        //        int i = 1;
        //        foreach (var curRes in studentsData)
        //        {
        //            ws.Cells[i, 1].Value = curRes.Username;
        //            ws.Cells[i, 2].Value = curRes.Points;
        //            i++;
        //        }
        //        excel.Save();
        //    }

        //}
    }
}
