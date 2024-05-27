using Milestone1_350.Models;
using System;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Milestone1_350.Service
{
    public class UsersDAO
    {

        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = minesweeper; Integrated Security = True; Connect Timeout = 30;";

        public bool FindByUsernameAndPassword(UserModel user)
        {
            bool success = false;
            string sqlStatement = "SELECT * FROM dbo.users WHERE USERNAME = @username AND PASSWORD = @password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = user.username;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = user.password;

                try
                {

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in FindByUsernameandPassword: " + ex.Message);
                }
            }

            return success;

        }
        public bool RegisterUser(UserModel user)
        {
            bool success = false;
            string sqlStatement = "INSERT INTO dbo.users (firstName, lastName, sex, age, state, email, username, password) VALUES (@firstName, @lastName, @sex, @age, @state, @email, @username, @password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@firstName", System.Data.SqlDbType.VarChar, 50).Value = user.firstName;
                command.Parameters.Add("@lastName", System.Data.SqlDbType.VarChar, 50).Value = user.lastName;
                command.Parameters.Add("@sex", System.Data.SqlDbType.VarChar, 50).Value = user.sex;
                command.Parameters.Add("@age", System.Data.SqlDbType.Int).Value = user.age;
                command.Parameters.Add("@state", System.Data.SqlDbType.VarChar, 50).Value = user.state;
                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 50).Value = user.email;
                command.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = user.username;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = user.password;

                try
                {
                    //start db connection
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    success = (rowsAffected > 0);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error in RegisterUser: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("General Error in RegisterUser: " + ex.Message);
                }

            }
            return success;

        }

    }
}
