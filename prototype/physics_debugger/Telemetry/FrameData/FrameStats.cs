using Physics.Telemetry.Serialised;

namespace Telemetry.FrameData
{
    public class FrameStats
    {
        public int FrameId { get; set; } = 0;
        public float ConsumedFrameTime { get; set; } = 0.0f;
        public float FrameProcessingTime { get; set; } = 0.0f;

        public FrameStats()
        {
        }

        public void ImportFromPacket(FrameStatsMessage packetStats)
        {
            if (packetStats != null)
            {
                FrameId = packetStats.FrameId;
                ConsumedFrameTime = packetStats.ConsumedFrameTime;
                FrameProcessingTime = packetStats.FrameProcessingTime;
            }
        }

        public FrameStatsMessage ExportToPacket()
        {
            FrameStatsMessage packet = new FrameStatsMessage();

            ExportToPacket(packet);

            return packet;
        }

        public void ExportToPacket(FrameStatsMessage packet)
        {
            if (packet != null)
            {
                packet.FrameId = FrameId;
                packet.ConsumedFrameTime = ConsumedFrameTime;
                packet.FrameProcessingTime = FrameProcessingTime;
            }
        }
    }
}
