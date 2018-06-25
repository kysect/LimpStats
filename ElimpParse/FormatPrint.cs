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
            return $"{user.Login}{GetSpace(user.Login.Length)} |{user.CompletedTaskCount}";
        }

        private static string GetSpace(int loginLength)
        {
            string s = "";
            for (int i = 0; i < 15 - loginLength; i++)
                s += " ";
            return s;
        }
    }
}