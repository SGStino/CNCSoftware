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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
using System.Threading;

namespace CNCDriver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPort serial;
        private InputViewModel vm;
        private FlatSurfaceWindow popup;

        public MainWindow()
        {
            this.DataContext = this.vm = new InputViewModel();
            InitializeComponent();
            StartSerial();

        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            if (popup == null)
            {
                popup = new FlatSurfaceWindow();
                popup.Owner = this;
                popup.Show();
            }
        }

        private void StartSerial()
        {
            serial = new SerialPort("COM5", 250000, Parity.None, 8);
            serial.Handshake = Handshake.None;
            serial.NewLine = "\r\n";
            serial.RtsEnable = false;
            serial.DtrEnable = false;
            serial.Open();
            var thread = new Thread(SerialThread);
            thread.IsBackground = true;
            thread.Start();
        }

        void SerialThread()
        {
            while (serial.IsOpen)
            {
                var result = serial.ReadLine();
                Dispatcher.Invoke(() => Log.Text += result + Environment.NewLine);
                if (result == "CMP XYZ")
                {
                    Dispatcher.Invoke(() => popup.Execute());
                }
            }
        }

        public uint T => vm.Time;
        public uint X => vm.StepsX;
        public uint Y => vm.StepsY;
        public uint Z => vm.StepsZ;
        public uint E => 0;




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Execute();
        }

        private void Execute()
        {
            serial.WriteLine($"M{T} {X} {Y} {Z} {E}");
            vm.SetLastPos();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            serial.WriteLine("R");
            vm.SetLastPos(0, 0, 0);
        }

        internal void Execute(Instruction instruction)
        {
            vm.PosX = instruction.PosX;
            vm.PosY = instruction.PosY;
            vm.PosZ = instruction.PosZ;
            vm.Speed = instruction.Speed;
            Execute();
        }
    }
}
