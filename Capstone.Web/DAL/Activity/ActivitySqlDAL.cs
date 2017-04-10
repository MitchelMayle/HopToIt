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
        private const string SQL_GetSteps = "Select SUM(seconds) from Activity where child_id =@child_id;";
        private const string SQL_GetMinutes = "Select SUM(carrots) from Activity where child_id = @child_id;";
        private const string SQL_IdExists = "Select count(*) from activity where child_id = @child_id;";
        private const string SQL_GetActivities = "Select * from activity where child_id = @child_id order by activity_date asc;";

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
        public int GetSteps(int child_Id)
        {
            int steps = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetSteps, conn);
                    cmd.Parameters.AddWithValue("@child_id", child_Id);
                    steps = ((int)cmd.ExecuteScalar()* 10);
                    return steps;           
                }
            }
            catch (Exception)
            {

                throw;
            }          
        }
        public int GetMinutes(int child_Id)
        {
            int minutes = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetMinutes, conn);
                    cmd.Parameters.AddWithValue("@child_Id", child_Id);

                    minutes = (int)cmd.ExecuteScalar();
                    return minutes;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool IdExists(int child_Id)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_IdExists, conn);
                    cmd.Parameters.AddWithValue("@child_id", child_Id);
                   
                    int result = (int)cmd.ExecuteScalar();

                    return result > 0;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ActivityModel> GetActivities(int child_Id)
        {
            
            List<ActivityModel> activityList = new List<ActivityModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetActivities, conn);
                    cmd.Parameters.AddWithValue("@child_id", child_Id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        ActivityModel activity = new ActivityModel()
                        {
                            Carrots = Convert.ToInt32(reader["carrots"]),
                            Date = Convert.ToDateTime(reader["activity_date"]),
                            ChildId = Convert.ToInt32(reader["child_id"]),
                            Seconds = Convert.ToInt32(reader["seconds"]),
                            
                        };
                        activityList.Add(activity);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return activityList;

        }
    }
}