using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    public class ShapePropertyDescriptor : PropertyDescriptor
    {
        [BrowsableAttribute(false)]
        public uint ShapdeIdToDescribe { get; }

        public override AttributeCollection Attributes { get { return new AttributeCollection(null); } }
        
        public override Type ComponentType { get { return ShapdeIdToDescribe.GetType(); } }

        public override string DisplayName { get; }

        public override string Description { get { return string.Empty; } }

        public override bool IsReadOnly { get { return true; } }

        public override string Name { get { return "ShapeName"; } }

        public override Type PropertyType { get { return ShapdeIdToDescribe.GetType(); } }

        public ShapePropertyDescriptor(uint shapeIdToDescribe, string displayName)
            : base("Shape", null)
        {
            ShapdeIdToDescribe = shapeIdToDescribe;
            DisplayName = displayName;
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override object GetValue(object component)
        {
            return ShapdeIdToDescribe;
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
