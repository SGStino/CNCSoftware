using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNCDriver
{
    public class Instruction : ViewModelBase
    {
        private double posZ;
        private double posY;
        private double posX;
        private double speed;


        public double Speed
        {
            get { return speed; }
            set
            {
                this.SetProperty(ref speed, value);
            }
        }
        public double PosZ
        {
            get { return posZ; }
            set
            {
                this.SetProperty(ref posZ, value);
            }
        }


        public double PosY
        {
            get { return posY; }
            set
            {
                this.SetProperty(ref posY, value);
            }
        }

        public double PosX
        {
            get { return posX; }
            set
            {
                this.SetProperty(ref posX, value);
            }
        }
    }
}
