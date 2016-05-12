using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace lab4PR
{
    class Sniffer
    {
      
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("192.168.0.255"), 8167);
        byte[] buffer; 
        public Sniffer()
        {
            socket.Bind(endpoint);
            socket.IOControl(IOControlCode.ReceiveAll, new byte[4] { 1, 0, 0, 0 }, new byte[4] { 0, 0, 0, 0 });
            buffer = new byte[65565];
        }

        public Full_packet GetPacket()
        {
            var length = socket.Receive(buffer);
            return  parse(buffer.Take(length).ToArray());
        }

        public Full_packet parse(byte[] array)
        {
            
            return new Full_packet
            {
                Version = array[0] >> 4,
                Header_len = array[0] & 0x0f,
                TOS = array[1],
                Total_len = array[3] + (array[2] << 8),
                Identification = array[5] + (array[4] << 8),
                Flags = array[6] >> 4,
                Fragment_offset = array[7] + (array[6] << 4),
                TTL = array[8],
                Protocol = array[9],
                Header_checksum = $"{array[10]}{array[11]}",
                Source = $"{array[12]}.{array[13]}.{array[14]}.{array[15]}.{array[15]}",
                Destination = $"{array[16]}.{array[17]}.{array[18]}.{array[19]}",
                Payload = array.Skip((array[0] & 0x0f)*4).ToArray()
            };

        }

        public async Task<Full_packet> getAsync() => await Task.Run( () => GetPacket());

    }
}
