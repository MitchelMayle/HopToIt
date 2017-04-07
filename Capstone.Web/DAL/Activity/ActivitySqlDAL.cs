using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL.Activity
{
    public class ActivitySqlDAL : IActivityDAL
    {
        private readonly string connectionString;
        private const string SQL_AddActivity = "Insert into activity VALUES (@child_id, @activity_date, @seconds, @carrots);";
        private const string SQL_ActivityToChildDB = "UPDATE child SET seconds = seconds + @seconds, carrots = carrots + @carrots WHERE child.child_id = @child_id;";

        public ActivitySqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void AddActivity(ActivityModel activity)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddActivity, conn);
                    SqlCommand cmd2 = new SqlCommand(SQL_ActivityToChildDB, conn);
                    cmd.Parameters.AddWithValue("@carrots", activity.Carrots);
                    cmd.Parameters.AddWithValue("@child_id", activity.ChildId);
                    cmd.Parameters.AddWithValue("@activity_date", activity.Date.Date.ToString());
                    cmd.Parameters.AddWithValue("@seconds", activity.Seconds);
                    cmd2.Parameters.AddWithValue("@carrots", activity.Carrots);
                    cmd2.Parameters.AddWithValue("@child_id", activity.ChildId);
                    cmd2.Parameters.AddWithValue("@seconds", activity.Seconds);

                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}