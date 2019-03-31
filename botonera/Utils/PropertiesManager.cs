using System;
using Xamarin.Essentials;
namespace botonera.Utils
{
    public static class PropertiesManager
    {
        public static bool PlayOnDevice
        {
            get
            {
                return Preferences.Get("PlayOnDevice", false);
            }
            set
            {
                Preferences.Set("PlayOnDevice", value);
            }
        }
        public static string Endpoint
        {
            get
            {
                return Preferences.Get("Endpoint", "http://172.22.0.144:8080");
            }
            set
            {
                Preferences.Set("Endpoint", value);
            }
        }
    }
}
