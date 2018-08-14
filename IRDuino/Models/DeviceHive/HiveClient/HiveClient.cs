using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IRDuino.Models.DomainModels;
using IRDuino.Models.State;
using Newtonsoft.Json;

namespace IRDuino.Models.DeviceHive.HiveClient
{
    public class HiveClient : IHiveClient
    {
        public string DeviceHiveURL => "http://playground.devicehive.com/api/rest/";

        public HiveClient()
        {
            _deviceHive = new HttpClient
            {
                BaseAddress = new Uri(DeviceHiveURL),
                DefaultRequestHeaders =
                {
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") },
                    Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJwYXlsb2FkIjp7ImEiOlsyLDMsNCw1LDYsNyw4LDksMTAsMTEsMTIsMTUsMTYsMTddLCJlIjoxNTMyNzk1OTQwNTM5LCJ0IjoxLCJ1IjoyMzE2LCJuIjpbIjIyOTMiXSwiZHQiOlsiKiJdfX0.QKUWcfcRelmW1LDo4DhhtIle049DxmPoGQHGHptVGSE")
                }
            };
        }

        public Task<IEnumerable<Device>> GetDevices()
        {
            return PerformGetRequest<IEnumerable<Device>>(HiveRoutes.Devices);
        }

        void IHiveClient.TurnOn(string deviceID)
        {
            var route = HiveRoutes.GetDeviceCommandRoute(deviceID);
            var content = StateContent.GetContentForTurnOff();

            PerformPostRequest(route, content);
        }

        void IHiveClient.SendDeviceCommand(string deviceID)
        {
            var route = HiveRoutes.GetDeviceCommandRoute(deviceID);
            var content = StateContent.GetContentForCommand();

            PerformPostRequest(route, content);
        }

        private async Task<T> PerformGetRequest<T>(string route)
        {
            var response = await _deviceHive.GetAsync(route);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<T>();
        }

        private void PerformPostRequest(string route, HttpContent content)
        {
            _deviceHive.PostAsync(route, content);
        }

        private readonly HttpClient _deviceHive;
    }
}
