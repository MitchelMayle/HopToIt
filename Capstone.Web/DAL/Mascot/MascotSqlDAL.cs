using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL.Mascot
{
    public class MascotSqlDAL : IMascotDAL
    {
        private readonly string connectionString;
        private const string SQL_CreateMascot = "INSERT INTO mascot VALUES ('default.png', @child_id, null, 0, 0, 0, 0, 0, 0, 0, 0, 0);";
        private const string SQL_GetMascot = "SELECT * FROM mascot INNER JOIN child on child.child_id = mascot.child_id WHERE child.child_id = @child_id;";

        public MascotSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateMascot(ChildModel child)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_CreateMascot, conn);

                    cmd.Parameters.AddWithValue("@child_id", child.Child_Id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public MascotModel GetMascot(ChildModel child)
        {
            MascotModel mascot = new MascotModel();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetMascot, conn);
                    cmd.Parameters.AddWithValue("@child_id", child.Child_Id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        mascot = new MascotModel
                        {
                            Mascot_Id = Convert.ToInt32(reader["mascot_id"]),
                            Mascot_Image = Convert.ToString(reader["mascot_image"]),
                            Child_Id = Convert.ToInt32(reader["child_id"]),
                            CurrentHat = Convert.ToString(reader["current"]),
                            BaseballHat = Convert.ToBoolean(reader["baseball_hat"]),
                            Beanie = Convert.ToBoolean(reader["beanie"]),
                            Bonnet = Convert.ToBoolean(reader["bonnet"]),
                            BucketHat = Convert.ToBoolean(reader["bucket_hat"]),
                            Crown = Convert.ToBoolean(reader["crown"]),
                            Flower = Convert.ToBoolean(reader["flower"]),
                            PropellerHat = Convert.ToBoolean(reader["propeller_hat"]),
                            Sombrero = Convert.ToBoolean(reader["sombrero"]),
                            TopHat = Convert.ToBoolean(reader["top_hat"]),
                        };
                    }

                    return mascot;
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }
    }
}