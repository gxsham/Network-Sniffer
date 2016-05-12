using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4PR
{
    class Full_packet
    {
        public int Version { get; set; }
        public int Header_len { get; set; }
        public int TOS { get; set; }
        public int Total_len { get; set; }
        public int Identification { get; set; }
        public int Flags { get; set; }
        public int Fragment_offset { get; set; }
        public int TTL { get; set; }
        public int Protocol { get; set; }
        public string Header_checksum { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }

        public byte[] Payload { get; set; }
    }
}
