using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LimpStats.Core.Tools
{
    public static class Tools
    {

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
        public static  bool ConnectionAvailable(string strServer)
        {
            try
            {
                var reqFP = (HttpWebRequest)WebRequest.Create(strServer);

                var rspFP = (HttpWebResponse)reqFP.GetResponse();
                if (HttpStatusCode.OK == rspFP.StatusCode)
                {
                    rspFP.Close();
                    return true;
                }

                rspFP.Close();
                return false;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}
