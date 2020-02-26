using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueController.Models
{
    public class XYColor
    {
        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                XYColor color = (XYColor)obj;
                return (color.X == this.X) && (color.Y == this.Y);
            }
        }


        public XYColor(double x, double y)
        {
            X = x;
            Y = y;
        }
        public XYColor()
        {

        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}
