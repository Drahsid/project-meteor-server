﻿using System.IO;
using System.Text;

using FFXIVClassic.Common;

namespace FFXIVClassic_Map_Server.packets.send.search
{
    class RetainerResultBodyPacket
    {
        public const ushort OPCODE = 0x01DB;
        public const uint PACKET_SIZE = 0x028;

        public static SubPacket BuildPacket(uint sourceActorId, bool isSuccess, string nameToAdd)
        {
            byte[] data = new byte[PACKET_SIZE - 0x20];
            using (MemoryStream mem = new MemoryStream(data))
            {
                using (BinaryWriter binWriter = new BinaryWriter(mem))
                {
                    binWriter.Write(Encoding.ASCII.GetBytes(sender), 0, Encoding.ASCII.GetByteCount(sender) >= 0x20 ? 0x20 : Encoding.ASCII.GetByteCount(sender));

                }
            }
            return new SubPacket(OPCODE, sourceActorId, data);
        }
    }
}