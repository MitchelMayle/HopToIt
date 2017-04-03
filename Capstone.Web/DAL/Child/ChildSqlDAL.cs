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
        private const string SQL_CreateChild = "INSERT INTO child VALUES (@parent_id, @username, @first_name, 0, 0, @password);";
        private const string SQL_GetChild = "SELECT * FROM child WHERE child.child_id = @child_id;";

        public ChildSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CreateChild(ChildModel newChild)
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

                    int result = cmd.ExecuteNonQuery();

                    return result > 0;

                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public ChildModel GetChild(int childId)
        {
            ChildModel child = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetChild, conn);
                    cmd.Parameters.AddWithValue("@child_id", childId);

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
                            Active_Minutes = Convert.ToInt32(reader["active_minutes"]),
                            Steps = Convert.ToInt32(reader["steps"]),
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
    }
}