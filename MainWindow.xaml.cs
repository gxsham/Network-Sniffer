using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using System.Threading;
using System.Collections.ObjectModel;

namespace lab4PR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Full_packet> list = new ObservableCollection<Full_packet>();
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Sniffer sniff = new Sniffer();
            list_view.ItemsSource = list; 
            while (true)
            {
                var packet = await sniff.getAsync();
                list.Add(packet);         
            }              
        }

        private void short_grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int i = list_view.SelectedIndex;
            if (i < 0)
            {
                i = 0; 
            }
            fill_data(list.ElementAt(i));
        }

        private void fill_data(Full_packet packet)
        {
            color_switch(packet.Protocol); 

            Version.Text = packet.Version.ToString();
            Header_len.Text = packet.Header_len.ToString();
            TOS.Text = packet.TOS.ToString();
            Total_length.Text = packet.Total_len.ToString();
            Identifier.Text = packet.Identification.ToString();
            Flags.Text = packet.Flags.ToString();
            Fragment_offset.Text = packet.Fragment_offset.ToString();
            TTL.Text = packet.TTL.ToString();
            Protocol.Text = packet.Protocol.ToString();
            Header_Checksum.Text = packet.Header_checksum.ToString();
            SRC.Text = packet.Source;
            DST.Text = packet.Destination;

            print_payload(packet.Payload, packet.Protocol);
        }
        private void print_payload(byte[] payload, int protocol)
        {
            if ((ProtocolType)protocol == ProtocolType.Udp)
            {
				UDP_packet udp = new UDP_packet(payload);
				Payload.Text = udp.ToString(); 
            }
            else 
            {
                Payload.Text = payload.Aggregate(string.Empty, (acc, v) => acc + v + " "); 
            }
        }
        private void color_switch( int protocol)
        {
            if ((ProtocolType)protocol == ProtocolType.Tcp)
            {
                Data_grid.Background = Brushes.LightSeaGreen;
            }
            else if ((ProtocolType)protocol == ProtocolType.Udp)
            {
                Data_grid.Background = Brushes.LightPink;
            }
            else
            {
                Data_grid.Background = Brushes.White;
            }
        }

    }
}
