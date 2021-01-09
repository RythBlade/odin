using System;
using System.ComponentModel;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    // in order for a Matrix4x4PropertyWrapper property to expandable in the property grid view, we need to implement the TypeConverter interface
    // and tag the Matrix4x4PropertyWrapper with this class.
    public class Matrix4x4PropertyWrapperTypeConverter : TypeConverter
    {
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(Matrix4x4PropertyWrapper));
        }
    }
}
