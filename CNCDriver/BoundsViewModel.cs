using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCDriver
{
    public class BoundsViewModel : ViewModelBase
    {
        private double x = 8.5;
        private double y = 2;
        private double width = 20;
        private double height = 16;
        private double depth = 0.15;
        private double travelDepth = 0.5;
        private double step = 0.2;

        public double X
        {
            get { return x; }
            set
            {
                this.SetProperty(ref x, value);
            }
        }
        public double Y
        {
            get { return y; }
            set
            {
                this.SetProperty(ref y, value);
            }
        }
        public double Width
        {
            get { return width; }
            set
            {
                this.SetProperty(ref width, value);
            }
        }
        public double Height
        {
            get { return height; }
            set
            {
                this.SetProperty(ref height, value);
            }
        }


        public double Depth
        {
            get { return depth; }
            set
            {
                this.SetProperty(ref depth, value);
            }
        }
        public double TravelDepth
        {
            get { return travelDepth; }
            set
            {
                this.SetProperty(ref travelDepth, value);
            }
        }
        public double Step
        {
            get { return step; }
            set
            {
                this.SetProperty(ref step, value);
            }
        }
    }
}
