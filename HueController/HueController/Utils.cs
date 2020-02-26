using HueController.Models;
using Q42.HueApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueController
{
    public static class Utils
    {
        private static Random _rng = new Random();

        public static XYColor GetXYFromRGB(double red, double green, double blue)
        {
            //thanks to this guy https://www.reddit.com/r/tasker/comments/4mzd01/using_rgb_colours_with_philips_hue_bulbs/
            if (red > 0.04045)
            {
                red = Math.Pow((red + 0.055) / (1.0 + 0.055), 2.4);
            }
            else red = (red / 12.92);

            if (green > 0.04045)
            {
                green = Math.Pow((green + 0.055) / (1.0 + 0.055), 2.4);
            }
            else green = (green / 12.92);

            if (blue > 0.04045)
            {
                blue = Math.Pow((blue + 0.055) / (1.0 + 0.055), 2.4);
            }
            else blue = (blue / 12.92);

            var X = red * 0.664511 + green * 0.154324 + blue * 0.162028;
            var Y = red * 0.283881 + green * 0.668433 + blue * 0.047685;
            var Z = red * 0.000088 + green * 0.072310 + blue * 0.986039;
            var x = X / (X + Y + Z);
            var y = Y / (X + Y + Z);
            var xy = new XYColor { X = x, Y = y };

            return xy;
        }

        public async static Task<string> GetSingleId(ILocalHueClient client)
        {
            try
            {
                var lights = await client.GetLightsAsync();
                return lights.First().Id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public static byte BrightnessPercentageToBytes(decimal percentage) =>(byte)Math.Floor(255 * percentage);
        
        public static int SecsToMS(int secs) => secs * 1000;

        private static int GetRandomInt(int lower, int higher) => _rng.Next(lower, higher + 1);

        public static XYColor GetRandomColor() => GetXYFromRGB(GetRandomInt(0, 255), GetRandomInt(0, 255), GetRandomInt(0, 255));

    }
}
