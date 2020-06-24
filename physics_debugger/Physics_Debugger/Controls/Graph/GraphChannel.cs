using System.ComponentModel;

namespace physics_debugger.Controls.Graph
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GraphChannel
    {
        public enum Axis
        {
            Hidden
            , PrimaryAxis
            , SecondaryAxis
        }

        public string Name { get; set; } = string.Empty;
        
        public Axis DisplayAxis { get; set; }

        public GraphChannel(string name, Axis displayAxis)
        {
            Name = name;
            DisplayAxis = displayAxis;
        }
    }
}
