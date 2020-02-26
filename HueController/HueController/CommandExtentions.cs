using HueController.Models;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HueController
{
    public static class CommandExtentions
    {
        /// <summary>
        /// Sets the transition speed of a LightCommand
        /// </summary>
        /// <param name="cmd">A light command</param>
        /// <param name="transitionSeconds">The time in seconds to set the light command transition speed to</param>
        /// <returns></returns>
        public static LightCommand SetTransitionTime(this LightCommand cmd, int transitionSeconds)
        {
            cmd.TransitionTime = TimeSpan.FromMilliseconds(transitionSeconds);
            return cmd;
        }

        public static LightCommand SetLightColor(this LightCommand cmd, XYColor color)
        {
            cmd.SetColor(color.X, color.Y);
            return cmd;
        }

        public static LightCommand SetBrightness(this LightCommand cmd, decimal brightnessPercent)
        {
            cmd.Brightness = Utils.BrightnessPercentageToBytes(brightnessPercent);
            return cmd;
        }

        public static void TurnOff(this ILocalHueClient client)
        {
            var cmd = new LightCommand()
                .SetTransitionTime(0);
            cmd.On = false;
            
            client.SendCommandAsync(cmd);
            Thread.Sleep(100);
        }

        public async static Task<XYColor> CurrentColor(this ILocalHueClient client)
        {
            var light = await client.GetLightAsync(await Utils.GetSingleId(client));
            var colorCoords = light.State.ColorCoordinates;

            return new XYColor(colorCoords[0], colorCoords[1]);
        }
    }
}
