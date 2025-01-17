﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;  //For [DataContract] and [DataMember]
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagement_WCF
{
    [DataContract]
    public class ItemWareHouse
    {
        [DataMember]
        public int RowId { get; set; }
        [DataMember]
        public string ItemId { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember] 
        public string ItemCategory { get; set; }
        [DataMember]
        public decimal ItemPrice { get; set; }
        [DataMember]
        public int ItemQty { get; set; }
        [DataMember]
        public string ItemType { get; set; }
        [DataMember]
        public string WhName { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string Err { get; set; }
    }
}
