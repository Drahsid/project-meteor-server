﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVClassic_Map_Server.packets.WorldPackets.Receive
{
    class LinkshellResultPacket
    {
        public int resultCode;

        public bool invalidPacket = false;

        public LinkshellResultPacket(byte[] data)
        {
            using (MemoryStream mem = new MemoryStream(data))
            {
                using (BinaryReader binReader = new BinaryReader(mem))
                {
                    try
                    {
                        resultCode = binReader.ReadInt32();                       
                    }
                    catch (Exception)
                    {
                        invalidPacket = true;
                    }
                }
            }

        }
    }
}
