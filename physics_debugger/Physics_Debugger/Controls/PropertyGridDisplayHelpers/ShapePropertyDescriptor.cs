using System;
using System.ComponentModel;
using Telemetry.FrameData.Shapes;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    public class ShapePropertyDescriptor : PropertyDescriptor
    {
        [BrowsableAttribute(false)]
        public BaseShapePropertyWrapper ShapdeToDescribe { get; }

        public override AttributeCollection Attributes { get { return new AttributeCollection(null); } }
        
        public override Type ComponentType { get { return ShapdeToDescribe.GetType(); } }

        public override string DisplayName { get; }

        public override string Description { get { return string.Empty; } }

        public override bool IsReadOnly { get { return true; } }

        public override string Name { get { return "ShapeName"; } }

        public override Type PropertyType { get { return ShapdeToDescribe.GetType(); } }

        public ShapePropertyDescriptor(BaseShape shapdeToDescribe, string displayName)
            : base("Shape", null)
        {
            ShapdeToDescribe = new BaseShapePropertyWrapper(shapdeToDescribe);
            DisplayName = displayName;
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override object GetValue(object component)
        {
            return ShapdeToDescribe;
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override void SetValue(object component, object value)
        {
        }
    }
}
