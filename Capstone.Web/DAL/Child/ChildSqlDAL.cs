using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL.Child
{
    public class ChildSqlDAL : IChildDAL
    {
        private readonly string connectionString;
        private const string SQL_CreateChild = "INSERT INTO child VALUES (@parent_id, @username, @first_name, 0, 0, @password, @salt);";
        private const string SQL_GetChild = "SELECT * FROM child WHERE child.username = @username;";
        private const string SQL_UpdateSeconds = "UPDATE child SET seconds = @seconds WHERE username = @userName;";
        private const string SQL_AddCarrot = "UPDATE child SET carrots = carrots + 1 WHERE username = @username;";
        private const string SQL_GetLeadersByCarrots = "  Select child.username, sum(activity.seconds) as seconds, sum(activity.carrots) as carrots from Activity inner join child on child.child_id = activity.child_id group by child.username order by carrots desc;";
        private const string SQL_GetLeadersBySteps = "  Select child.username, sum(activity.seconds) as seconds, sum(activity.carrots) as carrots from Activity inner join child on child.child_id = activity.child_id group by child.username order by carrots desc;";

        public ChildSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateChild(ChildModel newChild)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_CreateChild, conn);
                    cmd.Parameters.AddWithValue("@parent_id", newChild.Parent_Id);
                    cmd.Parameters.AddWithValue("@username", newChild.UserName);
                    cmd.Parameters.AddWithValue("@first_name", newChild.First_Name);
                    cmd.Parameters.AddWithValue("@password", newChild.Password);
                    cmd.Parameters.AddWithValue("@salt", newChild.Salt);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public ChildModel GetChild(string userName)
        {
            ChildModel child = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetChild, conn);
                    cmd.Parameters.AddWithValue("@username", userName);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        child = new ChildModel
                        {
                            UserName = Convert.ToString(reader["username"]),
                            First_Name = Convert.ToString(reader["first_name"]),
                            Child_Id = Convert.ToInt32(reader["child_id"]),
                            Parent_Id = Convert.ToInt32(reader["parent_id"]),
                            Password = Convert.ToString(reader["p_word"]),
                            Salt = Convert.ToString(reader["salt"]),
                            Carrots = Convert.ToInt32(reader["carrots"]),
                            Seconds = Convert.ToInt32(reader["seconds"]),
                        };
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }
            return child;
        }

        public void UpdateSeconds(string userName, int newSecondsTotal)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_UpdateSeconds, conn);
                    cmd.Parameters.AddWithValue("@seconds", newSecondsTotal);
                    cmd.Parameters.AddWithValue("@userName", userName);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public void AddCarrot(string userName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_AddCarrot, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw;
            }

        }

        public List<ChildModel> GetLeadersByCarrots()
        {
            List<ChildModel> childList = new List<ChildModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetLeadersByCarrots, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        ChildModel child = new ChildModel()
                        {
                            
                            Seconds = Convert.ToInt32(reader["seconds"]),
                            Carrots = Convert.ToInt32(reader["carrots"]),
                            UserName = Convert.ToString(reader["username"]),
                        };
                        childList.Add(child);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return childList;
        }

        public List<ChildModel> GetLeadersBySteps()
        {
            List<ChildModel> childList = new List<ChildModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetLeadersBySteps, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ChildModel child = new ChildModel()
                        {
                            
                            Seconds = Convert.ToInt32(reader["seconds"]),
                            Carrots = Convert.ToInt32(reader["carrots"]),
                            UserName = Convert.ToString(reader["username"]),
                        };
                        childList.Add(child);

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return childList;
        }
    }
}