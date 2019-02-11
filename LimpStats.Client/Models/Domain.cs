namespace LimpStats.Client.Models
{
    //TODO: Стоит сделать один енам на все случаи, когда мы зависить будем от сайта
    //Например, в .Models сейчас есть ProblemType, который для задач используется. По сути, это тоже самое.
    public enum Domain
    {
        Codeforces,
        Eolymp
    }
}