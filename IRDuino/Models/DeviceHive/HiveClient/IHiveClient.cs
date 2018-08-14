using System.Collections.Generic;
using System.Threading.Tasks;
using IRDuino.Models.DomainModels;

namespace IRDuino.Models.DeviceHive.HiveClient
{
    interface IHiveClient
    {
        Task<IEnumerable<Device>> GetDevices();

        Task SendDeviceCommand(string deviceID);

        Task TurnOn(string deviceID);
    }
}
