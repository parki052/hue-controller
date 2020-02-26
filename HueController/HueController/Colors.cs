using HueController.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HueController.Utils;

namespace HueController
{
    public static class Colors
    {
        public static List<XYColor> RainbowPalette() => new List<XYColor>
        {
            Red, Orange, Yellow, Green, Blue, Indigo, Violet
        };

        public static List<XYColor> Darker() => new List<XYColor>
        {   
            Red,
            Indigo,
            Violet,
            GetXYFromRGB(43, 47, 119),
            GetXYFromRGB(20, 24, 82),
            GetXYFromRGB(7, 11, 52)
        };

        public static XYColor None = GetXYFromRGB(1, 1, 1);
        public static XYColor Red = GetXYFromRGB(255, 0, 0);
        public static XYColor Orange = GetXYFromRGB(255, 127, 0);
        public static XYColor Yellow = GetXYFromRGB(255, 255, 0);
        public static XYColor Green = GetXYFromRGB(0, 255, 0);
        public static XYColor Blue = GetXYFromRGB(0, 0, 255);
        public static XYColor Indigo = GetXYFromRGB(75, 0, 130);
        public static XYColor Violet = GetXYFromRGB(143, 0, 255);
    }
}
