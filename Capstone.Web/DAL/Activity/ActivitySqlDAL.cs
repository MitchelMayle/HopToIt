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
                    cmd.Parameters.AddWithValue("@carrots", activity.Carrots);
                    cmd.Parameters.AddWithValue("@child_id", activity.ChildId);
                    cmd.Parameters.AddWithValue("@activity_date", activity.Date.Date.ToString());
                    cmd.Parameters.AddWithValue("@seconds", activity.Seconds);

                    cmd.ExecuteNonQuery();
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}