using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

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
    }
}
