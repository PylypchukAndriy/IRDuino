namespace IRDuino.Models.DeviceHive
{
    public static class HiveRoutes
    {
        public static string Devices => "device";

        public static string GetDeviceCommandRoute(string deviceID) => $"device/{deviceID}/command";
    }
}
