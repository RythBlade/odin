using System.ComponentModel;
using Telemetry.FrameData.Shapes;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    // tag the class with it's type converter so it can be expanded in a property grid view
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TetrahedronPropertyWrapper : BaseShapePropertyWrapper
    {
        [BrowsableAttribute(false)]
        public TetrahedronShape WrappedTetrahedron { get; }

        public TetrahedronPropertyWrapper(TetrahedronShape tetrahedron)
            : base(tetrahedron)
        {
            WrappedTetrahedron = tetrahedron;
        }
    }
}
