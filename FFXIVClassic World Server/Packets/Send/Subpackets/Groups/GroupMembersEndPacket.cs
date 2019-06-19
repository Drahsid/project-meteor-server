﻿/*
===========================================================================
Copyright (C) 2015-2019 Project Meteor Dev Team

This file is part of Project Meteor Server.

Project Meteor Server is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Project Meteor Server is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with Project Meteor Server. If not, see <https:www.gnu.org/licenses/>.
===========================================================================
*/

using FFXIVClassic.Common;
using FFXIVClassic_World_Server.DataObjects.Group;
using System;
using System.IO;

namespace FFXIVClassic_World_Server.Packets.Send.Subpackets.Groups
{
    class GroupMembersEndPacket
    {
        public const ushort OPCODE = 0x017E;
        public const uint PACKET_SIZE = 0x38;

        public static SubPacket buildPacket(uint sessionId, uint locationCode, ulong sequenceId, Group group)
        {
            byte[] data = new byte[PACKET_SIZE - 0x20];

            using (MemoryStream mem = new MemoryStream(data))
            {
                using (BinaryWriter binWriter = new BinaryWriter(mem))
                {
                    //Write List Header
                    binWriter.Write((UInt64)locationCode);
                    binWriter.Write((UInt64)sequenceId);
                    //Write List Info
                    binWriter.Write((UInt64)group.groupIndex);
                }
            }

            return new SubPacket(OPCODE, sessionId, data);
        }

    }
}
