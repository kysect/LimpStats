using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ElimpParse.Model;
using Newtonsoft.Json;

namespace ElimpParse.DatabaseProvider.Repositories
{
    public class UserRepositoriy
    {
        public const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=_telegramDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public void Create(ElimpUser user, int taskId)
        {
            //TODO: дописать
            using (var connect = new SqlConnection(ConnectionString))
            {
                string sqlExpression =
                    String.Format("INSERT INTO AllInfo (taskid, res, username) VALUES ({0}, {1}, '{2}')", taskId, user.UserProfileResult, user.Login);
                SqlCommand command = new SqlCommand(sqlExpression, connect);
                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }
        }
        //public ElimpUser Read(ElimpUser user)
        //{
        //  //  var taskres = new List

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    using (SqlCommand command = new SqlCommand(String.Format("select taskid, res * from AllInfo where username = '{0}'", user.Login), connection))
        //    {
        //        connection.Open();
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
                        
        //            }
        //        }
        //    }

           
        //}
        public void Update(ElimpUser user, List<int> taskpack)
        {
            //TODO: дописать
            using (var connect = new SqlConnection(ConnectionString))
            {
                string sqlExpression = "";

                foreach (var currenttask in taskpack)
                {
                        sqlExpression += String.Format("INSERT INTO AllInfo (taskid, res, username) VALUES ({0}, {1}, '{2}') UNION", currenttask, user.UserProfileResult, user.Login);
                }
             //   string sqlExpression =
                 //   String.Format("INSERT INTO AllInfo (taskid, res, username) VALUES ({0}, {1}, '{2}')", currenttask, user.UserProfileResult, user.Login);
                SqlCommand command = new SqlCommand(sqlExpression, connect);   
                
                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }
        }
        //public void UpdateDB(string username, int taskcount)
        //{
        //    try
        //    {
        //        connection.Open();
        //        string sqlExpression =
        //            String.Format("UPDATE UsersTaskInfo SET taskcount={0} WHERE username='{1}'", taskcount, username);
        //        SqlCommand command = new SqlCommand(sqlExpression, connection);
        //        command.CommandText = sqlExpression;
        //        command.ExecuteNonQuery();
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        // закрываем подключение
        //        connection.Close();
        //        Console.WriteLine("Подключение закрыто...");
        //    }

        //}
    }
}