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
        private const string SQL_AddActivity = "Insert into activity VALUES (@carrots, '2017-01-01', @child_id, @seconds);";

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
                    cmd.Parameters.AddWithValue("@carrots", 1/*activity.Carrots*/);
                    cmd.Parameters.AddWithValue("@child_id", 1/*activity.ChildId*/);
                    //cmd.Parameters.AddWithValue("@activity_date", activity.Date.Date.ToString());
                    cmd.Parameters.AddWithValue("@seconds", 1/*activity.Seconds*/);

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