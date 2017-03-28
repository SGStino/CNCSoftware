using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCDriver
{
    public class InputViewModel : ViewModelBase
    {
        private uint posToSteps(double value, double stepScaleZ) => (uint)(value / stepScaleZ);
        private double stepsToPos(uint value, double stepScaleZ) => value * stepScaleZ;

        private double stepScaleX = 31.8 / 7500;
        private double stepScaleY = 25.3 / 12000;
        private double stepScaleZ = 23.6 / 96000;

        private uint stepsX;
        private uint stepsY;
        private uint stepsZ;
        private double posX;
        private double posY;
        private double posZ;
        private uint lastStepZ;
        private uint lastStepY;
        private uint lastStepX;

        public double PosZ
        {
            get { return posZ; }
            set
            {
                if (this.SetProperty(ref posZ, value))
                {
                    StepsZ = posToSteps(value, stepScaleZ);
                    DistanceChanged();
                }
            }
        }


        public double PosY
        {
            get { return posY; }
            set
            {
                if (this.SetProperty(ref posY, value))
                {
                    StepsY = posToSteps(value, stepScaleY);
                    DistanceChanged();
                }
            }
        }

        public double PosX
        {
            get { return posX; }
            set
            {
                if (this.SetProperty(ref posX, value))
                {
                    StepsX = posToSteps(value, stepScaleX);
                    DistanceChanged();
                }
            }
        }

        public uint StepsZ
        {
            get
            {
                return stepsZ;
            }
            set { if (this.SetProperty(ref stepsZ, value)) PosZ = stepsToPos(value, stepScaleZ); }
        }


        public uint StepsY
        {
            get { return stepsY; }
            set { if (this.SetProperty(ref stepsY, value)) PosY = stepsToPos(value, stepScaleY); }
        }

        public uint StepsX
        {
            get { return stepsX; }
            set { if (this.SetProperty(ref stepsX, value)) PosX = stepsToPos(value, stepScaleX); }
        }

        internal void SetLastPos()
        {
            SetLastPos(stepsX, stepsY, stepsZ);
        }
        internal void SetLastPos(uint v1, uint v2, uint v3)
        {
            this.lastStepX = v1;
            this.lastStepY = v2;
            this.lastStepZ = v2;
            DistanceChanged();
        }

        public void DistanceChanged()
        {
            NotifyPropertyChanged(nameof(Distance));
            NotifyPropertyChanged(nameof(Time));
        }
        private double speed = 1; //cm/s

        // in microseconds
        public uint Time => (uint)(1000000 * Distance / Speed);

        public double Distance
        {
            get
            {
                var x = stepsToPos(lastStepX, stepScaleX) - posX;
                var y = stepsToPos(lastStepY, stepScaleY) - posY;
                var z = stepsToPos(lastStepZ, stepScaleZ) - posZ;
                return Math.Sqrt(x * x + y * y + z * z);
            }
        }
        // cm/sec
        public double Speed
        {
            get { return speed; }
            set { if (this.SetProperty(ref speed, value)) NotifyPropertyChanged(nameof(Time)); }
        }
    }
}
