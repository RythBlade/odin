using System.Collections.Generic;
using System.ComponentModel;
using Telemetry.FrameData.Shapes;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    // tag the class with it's type converter so it can be expanded in a property grid view
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ConvexHullPropertyWrapper : BaseShapePropertyWrapper
    {
        [BrowsableAttribute(false)]
        public ConvexHullShape WrappedConvexHull { get; }

        [Category("Convex Hull")]
        public List<ConvexHullShape.Vertex> Vertices { get { return WrappedConvexHull.Vertices; } }

        public ConvexHullPropertyWrapper(ConvexHullShape convexHull)
            : base(convexHull)
        {
            WrappedConvexHull = convexHull;
        }
    }
}
