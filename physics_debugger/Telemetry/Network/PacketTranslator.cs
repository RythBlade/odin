using Telemetry.FrameData;
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
        public Dictionary<int, BaseShape> AddedShapes = new Dictionary<int, BaseShape>();

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

        /*private void ProcessShapeAdded(BasePacketHeader1 packet)
        {
            // -- shape packet layout --
            //      int         shape ID
            //      uint        shape type
            //      uint        has local matrix
            //      float[16]   local matrix
            //      -- Obb -- 
            //          float[3]    half extents
            //      
            //      -- Convex hull --
            //          uint                number of vertices
            //          uint                number of faces
            //          float[3]float[3]    vertices
            //          int[3]              faces

            BaseShape baseShape = null;

            int byteIndex = BasePacketHeader1.StartOfPacketData;

            int shapeId = BitConverter.ToInt32(packet.PacketBytes, byteIndex); byteIndex += 4;
            uint shapeType = BitConverter.ToUInt32(packet.PacketBytes, byteIndex); byteIndex += 4;
            uint hasLocalMatrix = BitConverter.ToUInt32(packet.PacketBytes, byteIndex); byteIndex += 4;

            System.Numerics.Matrix4x4 localMatrix = new System.Numerics.Matrix4x4();

            localMatrix.M11 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M12 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M13 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M14 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M21 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M22 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M23 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M24 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M31 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M32 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M33 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M34 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M41 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M42 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M43 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
            localMatrix.M44 = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;

            switch(shapeType)
            {
                case (uint)ShapeType.eObb:
                    break;

                case (uint)ShapeType.eConvexHull:
                    ConvexHull convexHull = new ConvexHull();
                    baseShape = convexHull;

                    convexHull.Id = shapeId;
                    convexHull.HasLocalMatrix = hasLocalMatrix != 0;
                    convexHull.LocalMatrix = localMatrix;
                    convexHull.ShapeType = ShapeType.eConvexHull;

                    uint numberOfVertices = BitConverter.ToUInt32(packet.PacketBytes, byteIndex); byteIndex += 4;
                    uint numberOfFaces = BitConverter.ToUInt32(packet.PacketBytes, byteIndex); byteIndex += 4;

                    for(uint i = 0; i < numberOfVertices; ++i)
                    {
                        ConvexHull.Vertex newVertex = new ConvexHull.Vertex();

                        newVertex.Point.X = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                        newVertex.Point.Y = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                        newVertex.Point.Z = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                        newVertex.Point.W = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;

                        newVertex.Normal.X = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                        newVertex.Normal.Y = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                        newVertex.Normal.Z = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;
                        newVertex.Normal.W = BitConverter.ToSingle(packet.PacketBytes, byteIndex); byteIndex += 4;

                        convexHull.Vertices.Add(newVertex);
                    }

                    for (uint i = 0; i < numberOfFaces; ++i)
                    {
                        ConvexHull.Face newFace = new ConvexHull.Face();

                        newFace.Index[0] = BitConverter.ToInt32(packet.PacketBytes, byteIndex); byteIndex += 4;
                        newFace.Index[1] = BitConverter.ToInt32(packet.PacketBytes, byteIndex); byteIndex += 4;
                        newFace.Index[2] = BitConverter.ToInt32(packet.PacketBytes, byteIndex); byteIndex += 4;

                        convexHull.Faces.Add(newFace);
                    }
                    break;
            }

            AddedShapes.Add(baseShape.Id, baseShape);
        }*/

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
                    default:
                        break;
                }

                /*
                switch(packet.messageHeader.MessageType)
                {
                    case (uint)PacketType.eRigidBodyFrameUpdate:
                        ProcessRigidBodyFrameUpdate(packet, snapshot);
                        toReturn = true;
                        break;

                    case (uint)PacketType.eShapeAdded:
                        ProcessShapeAdded(packet);
                        toReturn = true;
                        break;

                    default:
                        break;
                }*/
            }

            return toReturn;
        }
    }
}
