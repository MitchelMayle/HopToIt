using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL.Item
{
    public class ItemsSqlDAL : IItemsDAL
    {
        private const string SQL_GetHats = "SELECT * from items where type = 'hat';";
        private const string SQL_GetBackgrounds = "SELECT * from items where type = 'background';";
        private const string SQL_GetItem = "SELECT * from items where item_id = @item_id;";
        private readonly string connectionString;

        public ItemsSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
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
        public ItemModel GetItem(int item_Id)
        {
            ItemModel item = new ItemModel();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetItem, conn);
                    cmd.Parameters.AddWithValue("@item_id", item_Id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        item.Image = Convert.ToString(reader["image"]);
                        item.Price = Convert.ToInt32(reader["price"]);
                        item.Type = Convert.ToString(reader["type"]);
                        item.Item_Id = Convert.ToInt32(reader["item_id"]);                                        
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return item;
        }

    }
}