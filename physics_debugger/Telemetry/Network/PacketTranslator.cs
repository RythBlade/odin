using Physics.Telemetry.Serialised;
using System;
using System.Collections.Generic;
using Telemetry.FrameData;
using Telemetry.FrameData.Shapes;

namespace Telemetry.Network
{
    public class PacketTranslator
    {
        public class CollectedFrameShapes
        {
            public int FrameId = 0;
            public List<BaseShape> Shapes = null;
        }

        public Dictionary<int, Tuple<bool, FrameSnapshot>> ConstructedSnaphots = new Dictionary<int, Tuple<bool, FrameSnapshot>>();
        public Dictionary<int, CollectedFrameShapes> AddedShapes = new Dictionary<int, CollectedFrameShapes>();

        public PacketTranslator()
        {
        }

        public FrameSnapshot FindOrCreateSnapshotForFrame(int frameId)
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

        public FrameSnapshot FindSnapshotForFrame(int frameId)
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
                // todo: error handle - what if the rigid body already exists?
                snapshot.AddRigidBody(packetBody);
            }
        }

        private void RegisterNewlyReceivedShape(BasePacketHeader packet, BaseShape createdShape)
        {
            CollectedFrameShapes collectedFrameShapes = null;
            if (AddedShapes.TryGetValue(packet.messageHeader.FrameId, out collectedFrameShapes))
            {
                collectedFrameShapes.Shapes.Add(createdShape);
            }
            else
            {
                collectedFrameShapes = new CollectedFrameShapes();
                collectedFrameShapes.FrameId = packet.messageHeader.FrameId;
                collectedFrameShapes.Shapes = new List<BaseShape>();

                collectedFrameShapes.Shapes.Add(createdShape);
                AddedShapes.Add(collectedFrameShapes.FrameId, collectedFrameShapes);
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
                        createdObb.ImportFromPacket(createdObbPacket);

                        RegisterNewlyReceivedShape(packet, createdObb);
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
                        createdConvexHull.ImportFromPacket(createdConvexHullPacket);

                        RegisterNewlyReceivedShape(packet, createdConvexHull);

                    }
                    break;
                case ShapeTypePacket.Tetrahedron:
                    {
                        TetrahedronShapePacket createdTetrahedronPacket = TetrahedronShapePacket.Parser.ParseFrom(packet.PacketBytes, packet.startOfPacketData + shapeCreatedPacket.CalculateSize(), shapeCreatedPacket.ShapeSize);

                        TetrahedronShape createdTetrahedron = new TetrahedronShape();
                        createdTetrahedron.ImportFromPacket(createdTetrahedronPacket);

                        RegisterNewlyReceivedShape(packet, createdTetrahedron);
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
                switch (packet.messageHeader.MessageType)
                {
                    case MessageHeaderMessage.Types.MessageType.RigidBodyUpdate:
                        FrameSnapshot snapshot = FindOrCreateSnapshotForFrame(packet.messageHeader.FrameId);

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
