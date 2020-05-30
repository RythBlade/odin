using Physics.Telemetry.Serialised;
using System;
using System.Collections.Generic;
using Telemetry.FrameData;
using Telemetry.FrameData.Shapes;

namespace Telemetry.Network
{
    public class PacketTranslator
    {
        public Dictionary<uint, Tuple<bool, FrameSnapshot>> ConstructedSnaphots = new Dictionary<uint, Tuple<bool, FrameSnapshot>>();
        public Dictionary<uint, List<BaseShape>> AddedShapes = new Dictionary<uint, List<BaseShape>>();

        public PacketTranslator()
        {
        }

        public FrameSnapshot FindOrCreateSnapshotForFrame(uint frameId)
        {
            FrameSnapshot snapshot = FindSnapshotForFrame(frameId);

            if (snapshot == null)
            {
                snapshot = new FrameSnapshot();
                snapshot.FrameId = frameId;
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
            RigidBodyListPacket rigidBodyList = RigidBodyListPacket.Parser.ParseFrom(packet.PacketBytes, packet.startOfPacketData, packet.messageHeader.DataSize);

            foreach(RigidBodyPacket packetBody in rigidBodyList.RigidBodies)
            {
                FrameData.RigidBody body = new FrameData.RigidBody();

                body.CopyFromPacket(packetBody);

                // todo: error handle - what if the rigid body already exists?
                snapshot.RigidBodies.Add(body.Id, body);
            }
        }

        private void ProcessShapeAdded(BasePacketHeader packet)
        {
            ShapeCreatedMessage shapeCreatedPacket = ShapeCreatedMessage.Parser.ParseFrom(packet.PacketBytes, packet.startOfPacketData, packet.messageHeader.DataSize);

            switch (shapeCreatedPacket.ShapeType)
            {
                case ShapeTypePacket.Obb:
                    {
                        ObbShapePacket createdObbPacket = ObbShapePacket.Parser.ParseFrom(packet.PacketBytes, packet.startOfPacketData + shapeCreatedPacket.CalculateSize(), shapeCreatedPacket.ShapeSize);

                        ObbShape createdObb = new ObbShape();
                        createdObb.CopyFromPacket(createdObbPacket);

                        List<BaseShape> frameShapeList = null;
                        if (AddedShapes.TryGetValue(packet.messageHeader.FrameId, out frameShapeList))
                        {
                            frameShapeList.Add(createdObb);
                        }
                        else
                        {
                            frameShapeList = new List<BaseShape>();
                            frameShapeList.Add(createdObb);
                            AddedShapes.Add(packet.messageHeader.FrameId, frameShapeList);
                        }
                    }
                    break;
                case ShapeTypePacket.Sphere:
                    break;
                case ShapeTypePacket.Cone:
                    break;
                case ShapeTypePacket.ConvexHull:
                    {
                        ConvexHullShapePacket createdConvexHullPacket = ConvexHullShapePacket.Parser.ParseFrom(packet.PacketBytes, packet.startOfPacketData + shapeCreatedPacket.CalculateSize(), shapeCreatedPacket.ShapeSize);

                        ConvexHullShape createdConvexHull = new ConvexHullShape();
                        createdConvexHull.CopyFromPacket(createdConvexHullPacket);

                        List<BaseShape> frameShapeList = null;
                        if (AddedShapes.TryGetValue(packet.messageHeader.FrameId, out frameShapeList))
                        {
                            frameShapeList.Add(createdConvexHull);
                        }
                        else
                        {
                            frameShapeList = new List<BaseShape>();
                            frameShapeList.Add(createdConvexHull);
                            AddedShapes.Add(packet.messageHeader.FrameId, frameShapeList);
                        }
                    }
                    break;
                case ShapeTypePacket.Tetrahedron:
                    {
                        TetrahedronShapePacket createdTetrahedronPacket = TetrahedronShapePacket.Parser.ParseFrom(packet.PacketBytes, packet.startOfPacketData + shapeCreatedPacket.CalculateSize(), shapeCreatedPacket.ShapeSize);

                        TetrahedronShape createdTetrahedron = new TetrahedronShape();
                        createdTetrahedron.CopyFromPacket(createdTetrahedronPacket);

                        List<BaseShape> frameShapeList = null;
                        if (AddedShapes.TryGetValue(packet.messageHeader.FrameId, out frameShapeList))
                        {
                            frameShapeList.Add(createdTetrahedron);
                        }
                        else
                        {
                            frameShapeList = new List<BaseShape>();
                            frameShapeList.Add(createdTetrahedron);
                            AddedShapes.Add(packet.messageHeader.FrameId, frameShapeList);
                        }
                    }
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
                    case MessageHeaderMessage.Types.MessageType.RigidBodyUpdate:
                        ProcessRigidBodyFrameUpdate(packet, snapshot);
                        toReturn = true;
                        break;

                    case MessageHeaderMessage.Types.MessageType.ShapeCreated:
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
