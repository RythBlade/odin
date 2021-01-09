using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using Telemetry.FrameData.Shapes;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    // tag the class with it's type converter so it can be expanded in a property grid view
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ObbPropertyWrapper : BaseShapePropertyWrapper
    {
        [BrowsableAttribute(false)]
        public ObbShape WrappedObb { get; }

        [Category("Obb")]
        public Vector4 HalfExtents { get { return WrappedObb.HalfExtents; } }

        public ObbPropertyWrapper(ObbShape obb)
            : base(obb)
        {
            WrappedObb = obb;
        }
    }
}
