using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

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
            //Todo: работает, но если есть варик попроще надо его юзать
            //TODO: Вынести в .Core? создать там папку /Tools

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

        public static  bool CheckInternetConnect(string strServer = "https://www.google.com")
        {
            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(strServer);
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    return response != null && HttpStatusCode.OK == response.StatusCode;
                }
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}
