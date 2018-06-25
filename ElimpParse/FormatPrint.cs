using System.Linq;

namespace ElimpParse
{
    public static class FormatPrint
    {
        public static string ConsoleFormat(ElimpUser user)
        {
            return $"{user.Login} [{user.Title}] ({user.CompletedTaskCount})";
        }

        public static string WebFormat(ElimpUser user)
        {
            return $"{user.Login}|{user.CompletedTaskCount}";
        }

        public static string TelegramFormat(ElimpUser user)
        {
            var list = BackUpManager.LoadJson().Where(u => u.Login == user.Login).First();
            return $"{user.Login, -14} |{user.CompletedTaskCount, -3} ({user.CompletedTaskCount - list.CompletedTaskCount})";
        }
    }
}