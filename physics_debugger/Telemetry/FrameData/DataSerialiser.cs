using Google.Protobuf;
using Physics.Telemetry.Serialised;
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
        public string Filename { get; set; }

        public bool OpenTelemetry(FrameData data)
        {
            bool toReturn = true;

            using (FileStream input = File.OpenRead(Filename))
            {
                FrameDataPacket packet = FrameDataPacket.Parser.ParseFrom(input);

                data.ImportFromPacket(packet);
            }

            return toReturn;
        }

        public bool SaveTelemetry(FrameData data)
        {
            bool toReturn = true;

            FrameDataPacket frameDataPack = data.ExportToPacket();

            int dataSize = frameDataPack.CalculateSize();

            using (FileStream output = File.OpenWrite(Filename))
            {
                // this "using" is crucial to fully writing out the entire buffer!
                using (CodedOutputStream outStream = new CodedOutputStream(output))
                {
                    frameDataPack.WriteTo(outStream);
                }
            }

            return toReturn;
        }
    }
}
