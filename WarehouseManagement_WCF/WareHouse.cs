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


        public string InsertItemData(string ItemId, string ItemName, string Category, string ItemType, string WhsName, double Price = 0.00, int Qty = 0)
        {
            SqlConnection Conn = null;

            try
            {
                if(ItemId != "" && ItemName != "" && Category !="" && ItemType != "" && WhsName != "")
                {
                    Conn = new SqlConnection(SqlConnString);
                    Conn.Open();

                    string SqlQuery = "INSERT INTO WarehouseManagement_WCF (ItemId,ItemName,ItemCategory,ItemPrice,ItemQty," +
                                        "ItemType,WhName)VALUES(@ItemId,@ItemName,@Cate,@Price,@Qty,@Type,@WhsName)";
                    SqlCommand cmd = new SqlCommand(SqlQuery, Conn);

                    cmd.Parameters.AddWithValue("@ItemId", ItemId);
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
                else
                {
                    return "All the parameters should be given.";
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

        public List<ItemWareHouse> GetAllItemData() 
        {
            SqlConnection Conn = null;

            try
            {
                Conn = new SqlConnection(SqlConnString);
                Conn.Open();

                string SqlQuery = "SELECT * FROM WarehouseManagement_WCF";
                SqlCommand cmd = new SqlCommand(SqlQuery, Conn);

                SqlDataReader DataReader = cmd.ExecuteReader();
                List<ItemWareHouse> ItemsList = new List<ItemWareHouse>();

                dynamic DataObject = null;

                if (DataReader.HasRows)
                {
                    while (DataReader.Read()) 
                    {
                        DataObject = new
                        {
                            RowId = Convert.ToInt32(DataReader["Id"]),
                            ItemId = DataReader["ItemId"].ToString(),
                            ItemName = DataReader["ItemName"].ToString(),
                            ItemCategory = DataReader["ItemCategory"].ToString(),
                            ItemPrice = Convert.ToDecimal(DataReader["ItemPrice"]),
                            ItemQty = Convert.ToInt32(DataReader["ItemQty"]),
                            ItemType = DataReader["ItemType"].ToString(),
                            WhName = DataReader["WhName"].ToString(),
                        };
                        ItemsList.Add(DataObject);
                    }
                }
                else
                {
                    ItemsList.Add(new ItemWareHouse { Message = "No data found in database!" });
                }
            
                return ItemsList;
            }
            catch(Exception e)
            {
                return new List<ItemWareHouse>
                            {
                                new ItemWareHouse { Err = e.Message }
                            };
            }
            finally
            {
                Conn.Close();
            }
        }

        public List<ItemWareHouse> GetSpecificItemData(string Id)
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
                List<ItemWareHouse> ItemsList = new List<ItemWareHouse>();

                dynamic DataObject = null;

                if (DataReader.HasRows)
                {
                    while (DataReader.Read())
                    {
                        DataObject = new
                        {
                            RowId = Convert.ToInt32(DataReader["Id"]),
                            ItemId = DataReader["ItemId"].ToString(),
                            ItemName = DataReader["ItemName"].ToString(),
                            ItemCategory = DataReader["ItemCategory"].ToString(),
                            ItemPrice = Convert.ToDecimal(DataReader["ItemPrice"]),
                            ItemQty = Convert.ToInt32(DataReader["ItemQty"]),
                            ItemType = DataReader["ItemType"].ToString(),
                            WhName = DataReader["WhName"].ToString(),
                        };
                        ItemsList.Add(DataObject);
                    }
                }
                else
                {
                    ItemsList.Add(new ItemWareHouse { Message = "Database rowId: " + Id + ", not found in database!" });
                }

                return ItemsList;
            }
            catch (Exception e)
            {
                return new List<ItemWareHouse>
                            {
                                new ItemWareHouse { Err = e.Message }
                            };
            }
            finally
            {
                Conn.Close();
            }
        }

    }
}
