namespace ElimpParse.Model
{
    public class ElimpUser
    {
        public string Login { get; set; }
        public string Title { get; set; }
        public TaskPack TaskPack { get; set; }
        public int CompletedTaskCount { get; set; }

        public ElimpUser()
        {

        }

        public ElimpUser(string login)
        {
            Login = login;
        }

        public ElimpUser(string login, string title)
        {
            Login = login;
            Title = title;
        }

        public override string ToString()
        {
            return $"{Login}, {CompletedTaskCount}";
        }
    }
}