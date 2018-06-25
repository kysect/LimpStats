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
            return $"{user.Login, -14} |{user.CompletedTaskCount}";
        }
    }
}