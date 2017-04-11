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
        private const string SQL_CreateMascot = "INSERT INTO mascot VALUES (@mascot_image, @child_id, null, null, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);";
        private const string SQL_GetMascot = "SELECT * FROM mascot INNER JOIN child on child.child_id = mascot.child_id WHERE child.child_id = @child_id;";
        private const string SQL_ChangeCurrentItem = "UPDATE mascot SET @property = @itemName WHERE mascot.child_id = @child_id;";
        private const string SQL_PurchaseItem = "UPDATE mascot SET @itemName = 1 WHERE mascot.child_id = @child_id;   UPDATE child SET carrots =  carrots - @price Where child_id = @child_id;";
        private const string SQL_UpdateHat = "UPDATE mascot SET current_hat = @currentHat WHERE mascot_id = @mascot_Id;";
        private const string SQL_UpdateBackground = "UPDATE mascot SET current_background = @currentBackground WHERE mascot_id = @mascot_Id;";


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
                            FlowerCrown = Convert.ToBoolean(reader["flower"]),
                            PropellerHat = Convert.ToBoolean(reader["propeller_hat"]),
                            Sombrero = Convert.ToBoolean(reader["sombrero"]),
                            TopHat = Convert.ToBoolean(reader["top_hat"]),
                            Beach = Convert.ToBoolean(reader["beach"]),
                            City = Convert.ToBoolean(reader["city"]),
                            Desert = Convert.ToBoolean(reader["desert"]),
                            Forest = Convert.ToBoolean(reader["forest"]),
                            Mountain = Convert.ToBoolean(reader["mountain"]),
                            Ocean = Convert.ToBoolean(reader["ocean"]),
                        };

                        if (mascot.CurrentHat == "")
                        {
                            mascot.CurrentHat = null;
                        }

                        if (mascot.CurrentBackground == "")
                        {
                            mascot.CurrentBackground = null;
                        }
                    }
                    return mascot;
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public void PurchaseItem(int childId, string itemName, int itemPrice)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"UPDATE mascot SET {itemName} = 1 WHERE mascot.child_id = @child_id;   UPDATE child SET carrots =  carrots - @price Where child_id = @child_id;", conn);     
                    cmd.Parameters.AddWithValue("@child_id", childId);
                    cmd.Parameters.AddWithValue("@price", itemPrice);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public void UpdateHat(int mascot_Id, string currentHat)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_UpdateHat, conn);
                    cmd.Parameters.AddWithValue("@currentHat", currentHat);
                    cmd.Parameters.AddWithValue("@mascot_Id", mascot_Id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public void UpdateBackground(int mascot_Id, string currentBackground)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_UpdateBackground, conn);
                    cmd.Parameters.AddWithValue("@currentBackground", currentBackground);
                    cmd.Parameters.AddWithValue("@mascot_Id", mascot_Id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw;
            }
        }

        public List<int> GetListOfItems(int childId)
        {
            MascotModel mascot = new MascotModel();
            List<int> itemList = new List<int>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetMascot, conn);
                    cmd.Parameters.AddWithValue("@child_Id", childId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        mascot.BaseballHat = Convert.ToBoolean(reader["baseball_hat"]);
                        mascot.Beanie = Convert.ToBoolean(reader["beanie"]);
                        mascot.Bonnet = Convert.ToBoolean(reader["bonnet"]);
                        mascot.Bow = Convert.ToBoolean(reader["bow"]);
                        mascot.BucketHat = Convert.ToBoolean(reader["bucket_hat"]);
                        mascot.Crown = Convert.ToBoolean(reader["crown"]);
                        mascot.FlowerCrown = Convert.ToBoolean(reader["flower"]);
                        mascot.PropellerHat = Convert.ToBoolean(reader["propeller_hat"]);
                        mascot.Sombrero = Convert.ToBoolean(reader["sombrero"]);
                        mascot.TopHat = Convert.ToBoolean(reader["top_hat"]);
                        mascot.Beach = Convert.ToBoolean(reader["beach"]);
                        mascot.City = Convert.ToBoolean(reader["city"]);
                        mascot.Desert = Convert.ToBoolean(reader["desert"]);
                        mascot.Forest = Convert.ToBoolean(reader["forest"]);
                        mascot.Mountain = Convert.ToBoolean(reader["mountain"]);
                        mascot.Ocean = Convert.ToBoolean(reader["ocean"]);


                    };
                    if (mascot.BaseballHat)
                    {
                        itemList.Add(1);
                    };
                    if (mascot.Beanie)
                    {
                        itemList.Add(2);
                    }
                    if (mascot.Bow)
                    {
                        itemList.Add(3);
                    }
                    if (mascot.BucketHat)
                    {
                        itemList.Add(4);
                    }
                    if (mascot.Bonnet)
                    {
                        itemList.Add(5);
                    }
                    if (mascot.PropellerHat)
                    {
                        itemList.Add(6);
                    }
                    if (mascot.Sombrero)
                    {
                        itemList.Add(7);
                    }
                    if (mascot.TopHat)
                    {
                        itemList.Add(8);
                    }
                    if (mascot.FlowerCrown)
                    {
                        itemList.Add(9);
                    }
                    if (mascot.Crown)
                    {
                        itemList.Add(10);
                    }
                    if (mascot.Beach)
                    {
                        itemList.Add(11);
                    }
                    if (mascot.City)
                    {
                        itemList.Add(12);
                    }
                    if (mascot.Desert)
                    {
                        itemList.Add(13);
                    }
                    if (mascot.Forest)
                    {
                        itemList.Add(14);
                    }
                    if (mascot.Mountain)
                    {
                        itemList.Add(15);
                    }
                    if (mascot.Ocean)
                    {
                        itemList.Add(16);
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