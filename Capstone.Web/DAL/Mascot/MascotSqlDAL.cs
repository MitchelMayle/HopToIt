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
        private const string SQL_CreateMascot = "INSERT INTO mascot VALUES (@mascot_image, @child_id, null, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);";
        private const string SQL_GetMascot = "SELECT * FROM mascot INNER JOIN child on child.child_id = mascot.child_id WHERE child.child_id = @child_id;";
        private const string SQL_PurchaseItem = "UPDATE mascot SET @itemName = 1 WHERE mascot.child_id = @child_id;";
        private const string SQL_ChangeCurrentItem = "UPDATE mascot SET @property = @itemName WHERE mascot.child_id = @child_id;";
        private const string SQL_GetHats = "SELECT * from mascot where type = 'hat';";
        private const strgin SQL_GetBackgrounds = "SELECT * from mascot where type = 'background';";

        public MascotSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateMascot(MascotModel newMascot)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_CreateMascot, conn);

                    cmd.Parameters.AddWithValue("@mascot_image", newMascot.Mascot_Image);
                    cmd.Parameters.AddWithValue("@child_id", newMascot.Child_Id);

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
            MascotModel mascot = null;

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
                            CurrentHat = Convert.ToString(reader["current_hat"]),
                            CurrentBackground = Convert.ToString(reader["current_background"]),
                            BaseballHat = Convert.ToBoolean(reader["baseball_hat"]),
                            Beanie = Convert.ToBoolean(reader["beanie"]),
                            Bonnet = Convert.ToBoolean(reader["bonnet"]),
                            Bow = Convert.ToBoolean(reader["bow"]),
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

        public void PurchaseItem(int childId, string itemName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_PurchaseItem, conn);
                    cmd.Parameters.AddWithValue("@itemName", itemName);
                    cmd.Parameters.AddWithValue("@child_id", childId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public void ChangeCurrentItem(int childId, string property, string itemName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_ChangeCurrentItem, conn);
                    cmd.Parameters.AddWithValue("@property", property);
                    cmd.Parameters.AddWithValue("@itemName", itemName);
                    cmd.Parameters.AddWithValue("@child_id", childId);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        public List<ItemModel> GetHats()
        {
            
            List<ItemModel> itemList = new List<ItemModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetHats, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        ItemModel item = new ItemModel()
                        {
                            Image = Convert.ToString(reader["image"]),
                            Price = Convert.ToInt32(reader["price"]),
                            Type = Convert.ToString(reader["type"]),
                            Item_Id = Convert.ToInt32(reader["item_id"])
                        };
                        itemList.Add(item);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return itemList;
        }
        public List<ItemModel> GetBackgrounds()
        {

            List<ItemModel> itemList = new List<ItemModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetBackgrounds, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ItemModel item = new ItemModel()
                        {
                            Image = Convert.ToString(reader["image"]),
                            Price = Convert.ToInt32(reader["price"]),
                            Type = Convert.ToString(reader["type"]),
                            Item_Id = Convert.ToInt32(reader["item_id"])
                        };
                        itemList.Add(item);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return itemList;
        }
    }
}