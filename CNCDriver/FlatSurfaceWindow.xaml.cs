using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CNCDriver
{
    /// <summary>
    /// Interaction logic for FlatSurfaceWindow.xaml
    /// </summary>
    public partial class FlatSurfaceWindow : Window
    {
        private BoundsViewModel vm;

        public FlatSurfaceWindow()
        {
            InitializeComponent();
            this.DataContext = this.vm = new BoundsViewModel();
        }
        private double workSpeed = 1;
        private double verticalSpeed = 0.05;
        private double travelSpeed = 2;
        private void ButtonContour_Click(object sender, RoutedEventArgs e)
        {
            instructions.Items.Add(new Instruction
            {
                PosX = 0,
                PosY = 0,
                PosZ = vm.TravelDepth,
                Speed = verticalSpeed
            });
            instructions.Items.Add(new Instruction
            {
                PosX = vm.X,
                PosY = vm.Y,
                PosZ = vm.TravelDepth,
                Speed = travelSpeed
            });
            instructions.Items.Add(new Instruction
            {
                PosX = vm.X,
                PosY = vm.Y,
                PosZ = vm.Depth,
                Speed = verticalSpeed


            });
            instructions.Items.Add(new Instruction
            {
                PosX = vm.X + vm.Width,
                PosY = vm.Y,
                PosZ = vm.Depth,
                Speed = workSpeed
            });
            instructions.Items.Add(new Instruction
            {
                PosX = vm.X + vm.Width,
                PosY = vm.Y + vm.Height,
                PosZ = vm.Depth,
                Speed = workSpeed
            });
            instructions.Items.Add(new Instruction
            {
                PosX = vm.X,
                PosY = vm.Y + vm.Height,
                PosZ = vm.Depth,
                Speed = workSpeed
            });
            instructions.Items.Add(new Instruction
            {
                PosX = vm.X,
                PosY = vm.Y,
                PosZ = vm.Depth,
                Speed = workSpeed
            });
        }

        internal void Execute()
        {
            if (instructions.Items.Count > 0)
            {
                var instruction = instructions.Items[0] as Instruction;
                instructions.Items.RemoveAt(0);
                var main = Owner as MainWindow;
                main.Execute(instruction);
            }
        }

        private void ButtonExecute_Click(object sender, RoutedEventArgs e)
        {
            Execute();
        }

        private void ButtonFill_Click(object sender, RoutedEventArgs e)
        {
            for (double y = 0; y < vm.Height; y += vm.Step * 2)
            {
                instructions.Items.Add(new Instruction
                {
                    PosX = vm.X,
                    PosY = vm.Y + y,
                    PosZ = vm.Depth,
                    Speed = workSpeed
                });
                instructions.Items.Add(new Instruction
                {
                    PosX = vm.X + vm.Width,
                    PosY = vm.Y + y,
                    PosZ = vm.Depth,
                    Speed = workSpeed
                });
                instructions.Items.Add(new Instruction
                {
                    PosX = vm.X + vm.Width,
                    PosY = vm.Y + y + vm.Step,
                    PosZ = vm.Depth,
                    Speed = workSpeed
                });
                instructions.Items.Add(new Instruction
                {
                    PosX = vm.X,
                    PosY = vm.Y + y + vm.Step,
                    PosZ = vm.Depth,
                    Speed = workSpeed
                });
            }
        }
    }
}
