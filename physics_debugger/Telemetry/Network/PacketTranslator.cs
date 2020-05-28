﻿using Telemetry.FrameData;
using Telemetry.FrameData.Shapes;
using System;
using System.Collections.Generic;
using System.Numerics;
using Physics.Telemetry.Serialised;

namespace Telemetry.Network
{
    public class PacketTranslator
    {
        public Dictionary<uint, Tuple<bool, FrameSnapshot>> ConstructedSnaphots = new Dictionary<uint, Tuple<bool, FrameSnapshot>>();
        public Dictionary<uint, BaseShape> AddedShapes = new Dictionary<uint, BaseShape>();

        public PacketTranslator()
        {
        }

        public FrameSnapshot FindOrCreateSnapshotForFrame(uint frameId)
        {
            FrameSnapshot snapshot = FindSnapshotForFrame(frameId);

            if (snapshot == null)
            {
                snapshot = new FrameSnapshot();
                ConstructedSnaphots.Add(frameId, new Tuple<bool, FrameSnapshot>(false, snapshot));
            }

            return snapshot;
        }

        public FrameSnapshot FindSnapshotForFrame(uint frameId)
        {
            FrameSnapshot snapshot = null;

            // todo: Error handling - if you do packets broken up into bits, they you'll need to keep a list of the bits you've recieved, so you know if you recieve a piece twice!
            Tuple<bool, FrameSnapshot> tuple = null;

            if (ConstructedSnaphots.TryGetValue( frameId, out tuple))
            {
                snapshot = tuple.Item2;
            }
            
            return snapshot;
        }

        private void ProcessRigidBodyFrameUpdate(BasePacketHeader packet, FrameSnapshot snapshot)
        {
            RigidBodyList rigidBodyList = RigidBodyList.Parser.ParseFrom(packet.PacketBytes, packet.startOfPacketData, packet.messageHeader.DataSize);

            foreach(Physics.Telemetry.Serialised.RigidBody packetBody in rigidBodyList.RigidBodies)
            {
                FrameData.RigidBody body = new FrameData.RigidBody();

                body.CopyFromPacket(packetBody);

                // todo: error handle - what if the rigid body already exists?
                snapshot.RigidBodies.Add(body.Id, body);
            }
        }

        private void ProcessShapeAdded(BasePacketHeader packet)
        {
            ShapeCreated shapeCreatedPacket = ShapeCreated.Parser.ParseFrom(packet.PacketBytes, packet.startOfPacketData, packet.messageHeader.DataSize);

            switch (shapeCreatedPacket.ShapeType)
            {
                case Physics.Telemetry.Serialised.ShapeType.Obb:
                    OBBShape createdObbPacket = OBBShape.Parser.ParseFrom(packet.PacketBytes, packet.startOfPacketData + shapeCreatedPacket.CalculateSize(), shapeCreatedPacket.ShapeSize);

                    ObbShape createdObb = new ObbShape();
                    createdObb.CopyFromPacket(createdObbPacket);

                    AddedShapes.Add(createdObb.Id, createdObb);
                    break;
                case Physics.Telemetry.Serialised.ShapeType.Sphere:
                    break;
                case Physics.Telemetry.Serialised.ShapeType.Cone:
                    break;
                case Physics.Telemetry.Serialised.ShapeType.ConvexHull:
                    break;
                case Physics.Telemetry.Serialised.ShapeType.Tetrahedron:
                    break;
                default:
                    break;
            }
        }

        public bool TranslatePacket(BasePacketHeader packet)
        {
            bool toReturn = false;

            if (packet != null)
            {
                FrameSnapshot snapshot = FindOrCreateSnapshotForFrame(packet.messageHeader.FrameId);

                switch (packet.messageHeader.MessageType)
                {
                    case MessageHeader.Types.MessageType.RigidBodyUpdate:
                        ProcessRigidBodyFrameUpdate(packet, snapshot);
                        toReturn = true;
                        break;

                    case MessageHeader.Types.MessageType.ShapeCreated:
                        ProcessShapeAdded(packet);
                        toReturn = true;
                        break;
                    default:
                        break;
                }
            }

            return toReturn;
        }
    }
}
