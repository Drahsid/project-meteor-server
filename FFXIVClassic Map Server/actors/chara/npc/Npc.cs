﻿using FFXIVClassic_Lobby_Server;
using FFXIVClassic_Lobby_Server.common;
using FFXIVClassic_Lobby_Server.packets;
using FFXIVClassic_Map_Server.actors;
using FFXIVClassic_Map_Server.lua;
using FFXIVClassic_Map_Server.packets.send.actor;
using FFXIVClassic_Map_Server.utils;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIVClassic_Map_Server.Actors
{
    class Npc : Character
    {
        public Npc(uint id, string actorName, uint zoneId, uint actorTemplateId, float posX, float posY, float posZ, float rot, ushort actorState, uint animationId, string className)
            : base(id)
        {
            this.actorName = actorName;
            this.positionX = posX;
            this.positionY = posY;
            this.positionZ = posZ;
            this.rotation = rot;
            this.animationId = animationId;
            this.className = className;

            this.displayNameId = displayNameId;
            this.customDisplayName = customDisplayName;

            this.zoneId = zoneId;

            loadNpcTemplate(actorTemplateId);
        }

        public SubPacket createAddActorPacket(uint playerActorId)
        {
            return AddActorPacket.buildPacket(actorId, playerActorId, 8);
        }

        public override SubPacket createScriptBindPacket(uint playerActorId)
        {
            List<LuaParam> lParams = LuaUtils.createLuaParamList("/Chara/Npc/Populace/PopulaceStandard", false, false, false, false, false, 0xF47F6, false, false, 0, 1, "TEST");
            return ActorInstantiatePacket.buildPacket(actorId, playerActorId, actorName, className, lParams);
        }

        public override BasePacket getSpawnPackets(uint playerActorId, uint spawnType)
        {
            List<SubPacket> subpackets = new List<SubPacket>();
            subpackets.Add(createAddActorPacket(playerActorId));
            subpackets.AddRange(getEventConditionPackets(playerActorId));
            subpackets.Add(createSpeedPacket(playerActorId));            
            subpackets.Add(createSpawnPositonPacket(playerActorId, 0x0));            
            subpackets.Add(createAppearancePacket(playerActorId));
            subpackets.Add(createNamePacket(playerActorId));
            subpackets.Add(createStatePacket(playerActorId));
            subpackets.Add(createIdleAnimationPacket(playerActorId));
            subpackets.Add(createInitStatusPacket(playerActorId));
            subpackets.Add(createSetActorIconPacket(playerActorId));
            subpackets.Add(createIsZoneingPacket(playerActorId));           
            subpackets.Add(createScriptBindPacket(playerActorId));            

            return BasePacket.createPacket(subpackets, true, false);
        }

        public void loadNpcTemplate(uint id)
        {
            using (MySqlConnection conn = new MySqlConnection(String.Format("Server={0}; Port={1}; Database={2}; UID={3}; Password={4}", ConfigConstants.DATABASE_HOST, ConfigConstants.DATABASE_PORT, ConfigConstants.DATABASE_NAME, ConfigConstants.DATABASE_USERNAME, ConfigConstants.DATABASE_PASSWORD)))
            {
                try
                {
                    conn.Open();

                    string query = @"
                                    SELECT                                     
                                    displayNameId,
                                    customDisplayName,
                                    base,
                                    size,
                                    hairStyle,
                                    hairHighlightColor,
                                    hairVariation,
                                    faceType,
                                    characteristics,
                                    characteristicsColor,
                                    faceEyebrows,
                                    faceIrisSize,
                                    faceEyeShape,
                                    faceNose,
                                    faceFeatures,
                                    faceMouth,
                                    ears,
                                    hairColor,
                                    skinColor,
                                    eyeColor,
                                    voice,
                                    mainHand,
                                    offHand,
                                    spMainHand,
                                    spOffHand,
                                    throwing,
                                    pack,
                                    pouch,
                                    head,
                                    body,
                                    legs,
                                    hands,
                                    feet,
                                    waist,
                                    neck,
                                    leftEars,
                                    rightEars,
                                    leftIndex,
                                    rightIndex,
                                    leftFinger,
                                    rightFinger
                                    FROM gamedata_actor_templates
                                    INNER JOIN gamedata_actor_appearance ON gamedata_actor_templates.id = gamedata_actor_appearance.id
                                    WHERE gamedata_actor_templates.id = @templateId
                                    ";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@templateId", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            //Handle Name

                            if (reader.IsDBNull(1))
                                displayNameId = reader.GetUInt32(0);
                            else
                            {
                                customDisplayName = reader.GetString(1);
                                displayNameId = 0xFFFFFFF;
                            }

                            //Handle Appearance
                            modelId = reader.GetUInt32(2);
                            appearanceIds[Character.SIZE] = reader.GetUInt32(3);
                            appearanceIds[Character.COLORINFO] = (uint)(reader.GetUInt32(18) | (reader.GetUInt32(17) << 10) | (reader.GetUInt32(19) << 20)); //17 - Skin Color, 16 - Hair Color, 18 - Eye Color
                            appearanceIds[Character.FACEINFO] = PrimitiveConversion.ToUInt32(CharacterUtils.getFaceInfo(reader.GetByte(8), reader.GetByte(9), reader.GetByte(7), reader.GetByte(16), reader.GetByte(15), reader.GetByte(14), reader.GetByte(13), reader.GetByte(12), reader.GetByte(11), reader.GetByte(10)));
                            appearanceIds[Character.HIGHLIGHT_HAIR] = (uint)(reader.GetUInt32(5) | reader.GetUInt32(4) << 10); //5- Hair Highlight, 4 - Hair Style
                            appearanceIds[Character.VOICE] = reader.GetUInt32(19);
                            appearanceIds[Character.WEAPON1] = reader.GetUInt32(21);
                            //appearanceIds[Character.WEAPON2] = reader.GetUInt32(22);
                            appearanceIds[Character.HEADGEAR] = reader.GetUInt32(28);
                            appearanceIds[Character.BODYGEAR] = reader.GetUInt32(29);
                            appearanceIds[Character.LEGSGEAR] = reader.GetUInt32(30);
                            appearanceIds[Character.HANDSGEAR] = reader.GetUInt32(31);
                            appearanceIds[Character.FEETGEAR] = reader.GetUInt32(32);
                            appearanceIds[Character.WAISTGEAR] = reader.GetUInt32(33);
                            appearanceIds[Character.R_EAR] = reader.GetUInt32(34);
                            appearanceIds[Character.L_EAR] = reader.GetUInt32(35);
                            appearanceIds[Character.R_FINGER] = reader.GetUInt32(38);
                            appearanceIds[Character.L_FINGER] = reader.GetUInt32(39);

                        }
                    }

                }
                catch (MySqlException e)
                { Console.WriteLine(e); }
                finally
                {
                    conn.Dispose();
                }
            }
        }

        public void loadEventConditions(string eventConditions)
        {
            EventList conditions = JsonConvert.DeserializeObject<EventList>(eventConditions);
            this.eventConditions = conditions;
        }
    }
}