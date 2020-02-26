using Q42.HueApi;
using Q42.HueApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueController
{
    class Program
    {
        static void Main(string[] args)
        {           
            var client = Configuration.GetClient();
            var manager = new Manager(client);
           
            //      ** Examples of how to call the various light commands **

            //manager.ColorsCycle(Colors.RainbowPalette(), brightness: 0.8m, repetitions: 1000);
            //manager.Breathing(Colors.Red, Colors.Red, 50000, 0.1m, 0.3m);
            //manager.RandomColorBreathing(1000, 0.1m, 0.5m);
            //manager.ColorsBreathing(Colors.Darker(), 10000, 0.1m, 0.3m);
            
            client.TurnOff();      
            Console.ReadKey();
        }
    }
}
