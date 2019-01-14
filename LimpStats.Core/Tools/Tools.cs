using System.Net;

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

        //TODO: fix
        public static string GenerateNextNumber(string number)
        {
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
