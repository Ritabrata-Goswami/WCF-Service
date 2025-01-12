using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace WarehouseManagement_WCF
{
    public class WareHouse : IWareHouse
    {
        private string SqlConnString = "Server=DESKTOP-7H6L3KN;Database=Ritabrata;Integrated Security=True;TrustServerCertificate=True;";


        public string InsertItemData(string ItemId, string ItemName, char Category, decimal Price, int Qty, string ItemType, string WhsName)
        {
            SqlConnection Conn = null;

            try
            {
                Conn = new SqlConnection(SqlConnString);
                Conn.Open();

                string SqlQuery = "INSERT INTO WarehouseManagement_WCF (ItemId,ItemName,ItemCategory,ItemPrice,ItemQty," +
                                    "ItemType,WhName)VALUES(@ItemId,@ItemName,@Cate,@Price,@Qty,@Type,@WhsName)";
                SqlCommand cmd = new SqlCommand(SqlQuery, Conn);

                cmd.Parameters.AddWithValue("@ItemId",ItemId);
                cmd.Parameters.AddWithValue("@ItemName", ItemName);
                cmd.Parameters.AddWithValue("@Cate", Category);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Qty", Qty);
                cmd.Parameters.AddWithValue("@Type", ItemType);
                cmd.Parameters.AddWithValue("@WhsName", WhsName);

                int RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    return "Item Id: " + ItemId + " has inserted Successfully. Rows affected:- " + RowsAffected;
                }
                else
                {
                    return "Something Went Wrong to insert item data.";
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("=============Exception Message===============");
                return ex.Message;
            }
            finally 
            {
                Conn.Close();
            }
      
        }


        public dynamic GetAllItemData() 
        {
            SqlConnection Conn = null;

            try
            {
                Conn = new SqlConnection(SqlConnString);
                Conn.Open();

                string SqlQuery = "SELECT * FROM WarehouseManagement_WCF";
                SqlCommand cmd = new SqlCommand(SqlQuery, Conn);

                SqlDataReader DataReader = cmd.ExecuteReader();
                List<dynamic> ItemsList = new List<dynamic>();

                dynamic DataObject = null;

                if (DataReader.HasRows)
                {
                    while (DataReader.Read()) 
                    {
                        DataObject = new
                        {
                            ItemId = DataReader["ItemId"],
                            ItemName = DataReader["ItemName"],
                            Category = DataReader["ItemCategory"],
                            Price = DataReader["ItemPrice"],
                            Quantity = DataReader["ItemQty"],
                            ItemType = DataReader["ItemType"],
                            WarehouseName = DataReader["WhName"],
                        };
                        ItemsList.Add(DataObject);
                    }
                }
                else
                {
                    DataObject = new
                    {
                        Message = "No data found in database!"
                    };
                    ItemsList.Add(DataObject);
                }
            
                return ItemsList;
            }
            catch(Exception e)
            {
                return e.Message;
            }
            finally
            {
                Conn.Close();
            }
        }


        public dynamic GetSpecificItemData(string Id)
        {
            SqlConnection Conn = null;

            try
            {
                Conn = new SqlConnection(SqlConnString);
                Conn.Open();

                string SqlQuery = "SELECT * FROM WarehouseManagement_WCF WHERE ItemId=@Id";
                SqlCommand cmd = new SqlCommand(SqlQuery, Conn);

                cmd.Parameters.AddWithValue("@Id",Id);

                SqlDataReader DataReader = cmd.ExecuteReader();
                List<dynamic> ItemsList = new List<dynamic>();

                dynamic DataObject = null;

                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        DataObject = new
                        {
                            ItemId = DataReader["ItemId"],
                            ItemName = DataReader["ItemName"],
                            Category = DataReader["ItemCategory"],
                            Price = DataReader["ItemPrice"],
                            Quantity = DataReader["ItemQty"],
                            ItemType = DataReader["ItemType"],
                            WarehouseName = DataReader["WhName"],
                        };
                        ItemsList.Add(DataObject);
                    }
                }
                else
                {
                    DataObject = new
                    {
                        Message = "No data found in database!"
                    };
                    ItemsList.Add(DataObject);
                }

                return ItemsList;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                Conn.Close();
            }
        }

    }
}
