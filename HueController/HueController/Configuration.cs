using Q42.HueApi;
using Q42.HueApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace HueController
{
    public static class Configuration
    {
        private readonly static string _bridgeIp;
        private readonly static string _appKey;
        private readonly static string _credFile = @"C:\Data\Creds\hueCreds.json";

        static Configuration()
        {
            var json = "";
            using(var sr = new StreamReader(_credFile))
            {
                json = sr.ReadToEnd();
            }

            var creds = (Dictionary<string, string>)JsonConvert.DeserializeObject(json, typeof(Dictionary<string, string>));

            _bridgeIp = creds["MyBridgeIp"];
            _appKey = creds["MyAppKey"];   
        }

        public static void GetBridgeIps(int secondsTimeout)
        {
            IBridgeLocator locator = new HttpBridgeLocator();
            var bridges = locator.LocateBridgesAsync(TimeSpan.FromSeconds(secondsTimeout)).GetAwaiter().GetResult();

            Console.WriteLine("_____________________________________");
            foreach (var bridge in bridges)
            {

                Console.WriteLine($"Bridge {bridge.BridgeId}: {bridge.IpAddress}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Registers an app to a device and returns the appKey
        /// </summary>
        /// <param name="bridgeIp"></param>
        /// <param name="appName">Must not contain spaces</param>
        /// <param name="device">Must not contain spaces</param>
        public async static Task<string> RegisterApp(string bridgeIp, string appName, string device)
        {
            ILocalHueClient client = new LocalHueClient(bridgeIp);
            var appKey =  await client.RegisterAsync(appName, device);

            return appKey;
        }

        /// <summary>
        /// Returns an initialized client ready to run commands
        /// </summary>
        /// <param name="bridgeIp"></param>
        /// <param name="appKey"></param>
        /// <returns></returns>
        public static ILocalHueClient GetClient()
        {
            var client = new LocalHueClient(_bridgeIp);
            client.Initialize(_appKey);

            client.TurnOff(); //in case the hue device is already on, turn it off
            return client;
        }
    }
}
