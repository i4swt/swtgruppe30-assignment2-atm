using System;
using System.Linq;
using System.Windows;
using AirTrafficMonitor.Lib.EventArgs;
using AirTrafficMonitor.Lib.Interfaces;
using AirTrafficMonitor.Lib.Factories;
using TransponderReceiver;

namespace AirTrafficMonitor.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRender
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            var atm = new Lib.AirTrafficMonitor(new BillundAirTrafficMonitorFactory(), transponderReceiver);

            atm.TrackingsChanged += RenderTrackings;
            atm.SeparationEventsChanged += RenderSeparationEvents;
        }

        public void RenderTrackings(object sender, TrackEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ListViewTrackings.ItemsSource = e.Trackings.ToList();
            }));
        }

        public void RenderSeparationEvents(object sender, SeparationEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ListViewSeparationEvents.ItemsSource = e.SeparationEvents;
            }));
        }
    }
}
