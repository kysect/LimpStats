using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ElimpParse.Model;

namespace ElimpParse.DatabaseProvider.repositories
{
    public static class UserRepositoriy
    {
        public static string connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=_telegramDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //      Console.WriteLine(connectionString);
        public static SqlConnection connection = new SqlConnection(connectionString);

        public static void ConnectToDB()
        {

            try
            {
                // Открываем подключение
                connection.Open();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // закрываем подключение
                connection.Close();
                Console.WriteLine("Подключение закрыто...");
            }



        }

        public static void UpdateDB(string username, int taskcount)
        {
            try
            {
                connection.Open();
                string sqlExpression =
                    String.Format("UPDATE UsersTaskInfo SET taskcount={0} WHERE username='{1}'", taskcount, username);
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // закрываем подключение
                connection.Close();
                Console.WriteLine("Подключение закрыто...");
            }
           
        }
        public static void UpdateAllInfoDB(string username, int taskid, int res)
        {
            try
            {
                connection.Open();
                string sqlExpression =
                    String.Format("INSERT INTO AllInfo (taskid, res, username) VALUES ({0}, {1}, '{2}')", taskid, res, username);
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandText = sqlExpression;
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // закрываем подключение
                connection.Close();
          //      Console.WriteLine("Подключение закрыто...");
            }

        }
    }
}