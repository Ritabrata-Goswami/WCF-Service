using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WarehouseManagement_WCF
{
    [ServiceContract]
    public interface IWareHouse
    {
        [OperationContract]
        string InsertItemData(string ItemId, string ItemName, string Category, string ItemType, string WhsName, double Price = 0.00, int Qty = 0);

        [OperationContract]
        List<ItemWareHouse> GetAllItemData();

        [OperationContract]
        List<ItemWareHouse> GetSpecificItemData(string Id);
    }

}
