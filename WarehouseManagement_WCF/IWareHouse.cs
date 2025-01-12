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
        string InsertItemData(string ItemId, string ItemName, char Category, decimal Price, int Qty, string ItemType, string WhsName);

        [OperationContract]
        dynamic GetAllItemData();

        [OperationContract]
        dynamic GetSpecificItemData(string Id);
    }

}
