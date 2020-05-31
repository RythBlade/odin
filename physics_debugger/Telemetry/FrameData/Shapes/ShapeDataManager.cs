using Telemetry.FrameData.Shapes;
using System.Collections.Generic;
using System;
using Physics.Telemetry.Serialised;

namespace Telemetry.FrameData.Shapes
{
    public class ShapeFrameIdPair : IComparable<ShapeFrameIdPair>
    {
        public uint FrameId = 0;
        public BaseShape Shape = null;

        public ShapeFrameIdPair(uint frameId, BaseShape shape)
        {
            FrameId = frameId;
            Shape = shape;
        }

        public int CompareTo(ShapeFrameIdPair other)
        {
            return other.FrameId < FrameId ? -1 : other.FrameId > FrameId ? 1 : 0;
        }

        public ShapeFrameIterationPacket ExportToPacket()
        {
            ShapeFrameIterationPacket packet = new ShapeFrameIterationPacket();

            ExportToPacket(packet);

            return packet;
        }

        public void ExportToPacket(ShapeFrameIterationPacket packet)
        {
            if (packet != null)
            {
                packet.FrameId = FrameId;

                switch (Shape.ShapeType)
                {
                    case ShapeType.eObb:
                        packet.ShapeType = ShapeTypePacket.Obb;
                        packet.ObbShape = ((ObbShape)Shape).ExportToPacket();
                        break;
                    case ShapeType.eSphere:
                        //packet.ShapeType = ShapeTypePacket.;
                        break;
                    case ShapeType.eCone:
                        //packet.ShapeType = ShapeTypePacket.Cone;
                        break;
                    case ShapeType.eConvexHull:
                        packet.ShapeType = ShapeTypePacket.ConvexHull;
                        packet.ConvexHullShape = ((ConvexHullShape)Shape).ExportToPacket();
                        break;
                    case ShapeType.eTetrahedron:
                        packet.ShapeType = ShapeTypePacket.Tetrahedron;
                        packet.TetrahedronShape = ((TetrahedronShape)Shape).ExportToPacket();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class ShapeIterations
    {
        // frame ID that generated the iteration
        public List<ShapeFrameIdPair> Iterations = new List<ShapeFrameIdPair>();

        public ShapeDataPacket ExportToPacket()
        {
            ShapeDataPacket packet = new ShapeDataPacket();

            ExportToPacket(packet);

            return packet;
        }

        public void ExportToPacket(ShapeDataPacket packet)
        {
            if(packet != null)
            {
                foreach(ShapeFrameIdPair iteration in Iterations)
                {
                    packet.Shapes.Add(iteration.ExportToPacket());
                }
            }
        }
    }

    public class ShapeDataManager
    {
        // shape id, set of iterations
        public Dictionary<uint, ShapeIterations> ShapeData = new Dictionary<uint, ShapeIterations>();

        public ShapeFrameIdPair AddNewShape(uint frameId, BaseShape addedShape)
        {
            ShapeIterations iterations = new ShapeIterations();

            ShapeFrameIdPair newPair = new ShapeFrameIdPair(frameId, addedShape);

            iterations.Iterations.Add(newPair);
            iterations.Iterations.Sort();

            ShapeData.Add(addedShape.Id, iterations);

            return newPair;
        }

        public ShapeFrameIdPair RetrieveShapeForFrame(uint shapeId, uint frameId)
        {
            ShapeFrameIdPair shapePairToReturn = null;

            ShapeIterations iterations = null;

            if (ShapeData.TryGetValue(shapeId, out iterations))
            {
                // find the most recent shape that was used for this frame id
                for (int i = 0; i < iterations.Iterations.Count; ++i)
                {
                    if (iterations.Iterations[i].FrameId == frameId)
                    {
                        shapePairToReturn = iterations.Iterations[i];

                        break;
                    }
                    else if (iterations.Iterations[i].FrameId < frameId)
                    {
                        shapePairToReturn = iterations.Iterations[i];
                    }
                    else
                    {
                        // the frame is now higher than the target frame id so we should've found it by now as the list is sorted!
                        break;
                    }
                }
            }

            return shapePairToReturn;
        }

        public void ImportFromPacket(ShapeDataPacket packet)
        {
            if (packet != null)
            {
                foreach(ShapeFrameIterationPacket iterationPacket in packet.Shapes)
                {
                    switch (iterationPacket.ShapeType)
                    {
                        case ShapeTypePacket.Obb:
                            ObbShape createdObb = new ObbShape();
                            createdObb.ImportFromPacket(iterationPacket.ObbShape);

                            AddNewShape(iterationPacket.FrameId, createdObb);
                            break;
                        case ShapeTypePacket.Sphere:
                            break;
                        case ShapeTypePacket.Cone:
                            break;
                        case ShapeTypePacket.ConvexHull:
                            ConvexHullShape createdConvexHull = new ConvexHullShape();
                            createdConvexHull.ImportFromPacket(iterationPacket.ConvexHullShape);

                            AddNewShape(iterationPacket.FrameId, createdConvexHull);
                            break;
                        case ShapeTypePacket.Tetrahedron:
                            TetrahedronShape createdTetrahedron = new TetrahedronShape();
                            createdTetrahedron.ImportFromPacket(iterationPacket.TetrahedronShape);

                            AddNewShape(iterationPacket.FrameId, createdTetrahedron);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public ShapeDataPacket ExportToPacket()
        {
            ShapeDataPacket packet = new ShapeDataPacket();

            ExportToPacket(packet);

            return packet;
        }

        public void ExportToPacket(ShapeDataPacket packet)
        {
            if( packet != null)
            {
                foreach(ShapeIterations shapeIterations in ShapeData.Values)
                {
                    shapeIterations.ExportToPacket(packet);
                }
            }
        }
    }
}
