using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4PR
{
	class UDP_packet
	{
		public int Source_port { set; get; }
		public int Destination_port { set; get; }
		public int Total_len { set; get; }
		public int Checksum { set; get; }
		public string Data { set; get; }

		public UDP_packet(byte[] array)
		{
			Source_port = array[1] + (array[0] << 8);
			Destination_port = array[3] + (array[2] << 8);
			Total_len = array[5] + (array[4] << 8);
			Checksum = array[7] + (array[6] << 8);
			Data = array.Skip(8).Aggregate(string.Empty, (acc, v) => acc + v + " ");
		}

		public override string ToString() =>
			$"Source Port number : {Source_port}\t\tDestination port number : {Destination_port}\r\nTotal Length : {Total_len}\t\t\tChecksum : {Checksum}\r\nData : {Data}";
	}
}
