using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry.FrameData
{
    public class DataSerialiser
    {
        private const uint startOfFrameMarker = 0xfffffffe;
        private const uint endOfFrameMarker = 0xffffffff;

        public string Filename { get; set; }

        public bool OpenTelemetry(FrameData data)
        {
            bool toReturn = true;

            using (BinaryReader reader = new BinaryReader(File.Open(Filename, FileMode.Open)))
            {
                while(reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    uint startMarker = reader.ReadUInt32();

                    if(startMarker != startOfFrameMarker)
                    {
                        // the file is corrupt in some way - bail out with what we've got and don't add this frame
                        toReturn = false;
                        break;
                    }

                    FrameSnapshot readFrame = new FrameSnapshot();

                    readFrame.FrameId = reader.ReadUInt32();

                    int numberOfRigidBodies = reader.ReadInt32();

                    for(int i = 0; i < numberOfRigidBodies; ++i)
                    {
                        RigidBody readBody = new RigidBody();

                        readBody.Id = reader.ReadUInt32();

                        readBody.WorldMatrix.M11 = reader.ReadSingle();
                        readBody.WorldMatrix.M12 = reader.ReadSingle();
                        readBody.WorldMatrix.M13 = reader.ReadSingle();
                        readBody.WorldMatrix.M14 = reader.ReadSingle();

                        readBody.WorldMatrix.M21 = reader.ReadSingle();
                        readBody.WorldMatrix.M22 = reader.ReadSingle();
                        readBody.WorldMatrix.M23 = reader.ReadSingle();
                        readBody.WorldMatrix.M24 = reader.ReadSingle();

                        readBody.WorldMatrix.M31 = reader.ReadSingle();
                        readBody.WorldMatrix.M32 = reader.ReadSingle();
                        readBody.WorldMatrix.M33 = reader.ReadSingle();
                        readBody.WorldMatrix.M34 = reader.ReadSingle();

                        readBody.WorldMatrix.M41 = reader.ReadSingle();
                        readBody.WorldMatrix.M42 = reader.ReadSingle();
                        readBody.WorldMatrix.M43 = reader.ReadSingle();
                        readBody.WorldMatrix.M44 = reader.ReadSingle();

                        readFrame.RigidBodies.Add(readBody.Id, readBody);
                    }

                    uint endMarker = reader.ReadUInt32();

                    if (endMarker != endOfFrameMarker)
                    {
                        // the file is corrupt in some way - bail out with what we've got and don't add this frame
                        toReturn = false;
                        break;
                    }

                    data.Frames.Add(readFrame);
                }
            }

            return toReturn;
        }

        public bool SaveTelemetry(FrameData data)
        {
            bool toReturn = true;

            using (BinaryWriter writer = new BinaryWriter(File.Open(Filename, FileMode.OpenOrCreate)))
            {
                foreach(FrameSnapshot frame in data.Frames)
                {
                    writer.Write(startOfFrameMarker);

                    writer.Write(frame.FrameId);

                    writer.Write(frame.RigidBodies.Values.Count);

                    foreach(RigidBody body in frame.RigidBodies.Values)
                    {
                        writer.Write(body.Id);

                        writer.Write(body.WorldMatrix.M11);
                        writer.Write(body.WorldMatrix.M12);
                        writer.Write(body.WorldMatrix.M13);
                        writer.Write(body.WorldMatrix.M14);

                        writer.Write(body.WorldMatrix.M21);
                        writer.Write(body.WorldMatrix.M22);
                        writer.Write(body.WorldMatrix.M23);
                        writer.Write(body.WorldMatrix.M24);

                        writer.Write(body.WorldMatrix.M31);
                        writer.Write(body.WorldMatrix.M32);
                        writer.Write(body.WorldMatrix.M33);
                        writer.Write(body.WorldMatrix.M34);

                        writer.Write(body.WorldMatrix.M41);
                        writer.Write(body.WorldMatrix.M42);
                        writer.Write(body.WorldMatrix.M43);
                        writer.Write(body.WorldMatrix.M44);
                    }

                    writer.Write(endOfFrameMarker);
                }
            }

            return toReturn;
        }
    }
}
