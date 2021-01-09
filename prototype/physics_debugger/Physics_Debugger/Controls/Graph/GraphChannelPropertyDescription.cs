using System;
using System.ComponentModel;

namespace physics_debugger.Controls.Graph
{
    public class GraphChannelPropertyDescription : PropertyDescriptor
    {
        public override Type ComponentType { get { return Channel.GetType(); } }

        public override bool IsReadOnly { get { return false; } }

        public override Type PropertyType { get { return Channel.DisplayAxis.GetType(); } }

        public GraphChannel Channel { get; set; }

        public override string DisplayName { get { return Channel.Name; } }

        public override string Name { get { return Channel.Name; } }

        public GraphChannelPropertyDescription(GraphChannel channel)
            : base("Desc", null)
        {
            Channel = channel;
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override object GetValue(object component)
        {
            return Channel.DisplayAxis;
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            Channel.DisplayAxis = (GraphChannel.Axis)value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
    }
}
