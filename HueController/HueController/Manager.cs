using HueController.Models;
using Q42.HueApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueController
{
    public class Manager
    {
        private readonly ILocalHueClient _client;

        public Manager(ILocalHueClient client)
        {
            _client = client;
        }

        public void RandomCycles(int transitionMs, int msToStay, decimal brightness, int repetitions)
        {
            void randomTransition() => Commands.RandomTransition(_client, transitionMs, msToStay, brightness);
            Commands.Repeater(randomTransition, repetitions);
        }

        public void ColorsCycle(List<XYColor> colors, int transitionMs = 10000, int msToStay = 2000, decimal brightness = 0.5m, int repetitions = 10)
        {
            void rainbowTransition() => Commands.ColorsCycle(_client, transitionMs, msToStay, brightness, colors);
            Commands.Repeater(rainbowTransition, repetitions);
        }

        public void Breathing(XYColor lowColor, XYColor highColor, int repetitions, decimal lowBrightness, decimal highBrightness)
        {
            void breathing() => Commands.Breathing(_client, lowColor, highColor, lowBrightness, highBrightness);
            Commands.Repeater(breathing, repetitions);
        }

        public void ColorsBreathing(List<XYColor> colors, int repetitions, decimal lowerBrightness, decimal higherBrightness)
        {
            void colorsBreathing() => Commands.ColorsBreathing(_client, Colors.Darker(), lowerBrightness, higherBrightness);
            Commands.Repeater(colorsBreathing, repetitions);
        }
        public void RandomColorBreathing(int repetitions, decimal lowBrightness, decimal highBrightness)
        {
            void randomBreathing() => Commands.RandomColorBreathing(_client, lowBrightness, highBrightness);
            Commands.Repeater(randomBreathing, repetitions);
        }

        public void PoliceMode(int repetitons)
        {
            void policeMode() => Commands.ColorsCycle(_client, 0, 1300, 1, new List<XYColor> { Colors.Blue, Colors.Red });
            Commands.Repeater(policeMode, repetitons);
        }
    }
}
