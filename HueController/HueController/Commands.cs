using Q42.HueApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Q42.HueApi.Converters;
using Q42.HueApi.Interfaces;
using System.Drawing;
using HueController.Models;

namespace HueController
{
    public static class Commands
    {
        public static void RandomTransition(ILocalHueClient client, int transitionMs, int msToStay, decimal brightness)
        {
            ChangeColor(client, transitionMs, msToStay, Utils.GetRandomColor(), brightness);
        }

        private static void ChangeColor(ILocalHueClient client, int transitionMs, int msToStay, XYColor toColor, decimal brightness)
        {
            var cmd = new LightCommand()
                .SetTransitionTime(transitionMs)
                .SetLightColor(toColor)
                .SetBrightness(brightness)
                .TurnOn();

            client.SendCommandAsync(cmd);
            Thread.Sleep(transitionMs + msToStay);          
        }

        public static void Breathing(ILocalHueClient client, XYColor lowColor, XYColor highColor, decimal lowBrightness, decimal highBrightness)
        {
            ChangeColor(client, 5000, 1500, highColor, highBrightness);
            ChangeColor(client, 3500, 1000, lowColor, lowBrightness);
            ChangeColor(client, 700, 500, lowColor, 0);
        }

        public static void RandomColorBreathing(ILocalHueClient client, decimal lowBrightness, decimal highBrightness)
        {
            var color = Utils.GetRandomColor();
            Breathing(client, color, color, lowBrightness, highBrightness);
        }

        public static void ColorsBreathing(ILocalHueClient client, List<XYColor> colors, decimal lowBrightness, decimal highBrightness)
        {
            foreach(var color in colors)
            {
                Breathing(client, color, color, lowBrightness, highBrightness);
            }
        }

        public static void ColorsCycle(ILocalHueClient client, int transitionMs, int msToStay, decimal brightness, List<XYColor> colors)
        {
            foreach(var color in colors)
            {
                ChangeColor(client, transitionMs, msToStay, color, brightness);
            }
        }

        public static void Repeater(Action action, int repetitions)
        {
            for(int i = 1; i <= repetitions; i++)
            {
                action();
            }
        }
    }
}
