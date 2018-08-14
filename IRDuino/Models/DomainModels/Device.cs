using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IRDuino.Models.DomainModels
{
    public class Device
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public object Data { get; set; }

        public int NetworkID { get; set; }

        public int DeviceTypeID { get; set; }

        public bool IsBlocked { get; set; }
    }
}

