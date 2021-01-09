using System.ComponentModel;
using Telemetry.FrameData;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    // tag the class with it's type converter so it can be expanded in a property grid view
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FrameStatsPropertyWrapper
    {
        [BrowsableAttribute(false)]
        public FrameStats WrappedFrameStats { get; }

        [Category("Timing")]
        public float ConsumedFrameTimeMs { get { return WrappedFrameStats.ConsumedFrameTime; } }

        [Category("Timing")]
        public float FrameProcessingTimeMicroSeconds { get { return WrappedFrameStats.FrameProcessingTime; } }

        public FrameStatsPropertyWrapper(FrameStats stats)
        {
            WrappedFrameStats = stats;
        }
    }
}
