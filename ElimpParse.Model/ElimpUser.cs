namespace ElimpParse.Model
{
    public class ElimpUser
    {
        public string Login { get; set; }
        public string Title { get; set; }
        public UserProfileResult ProfileResult { get; set; }

        //TODO: replace with UserProfileResult
        public TaskPack TaskPack { get; set; }
        //TODO: remove (ref to UserProfileResult)
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
            return $"{Login}";
        }
    }
}