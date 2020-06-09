using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    // tag the class with it's type converter so it can be expanded in a property grid view
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ShapeListWrapper : ICustomTypeDescriptor
    {
        public List<uint> WrappedShapeIds { get; }

        public ShapeListWrapper(List<uint> shapeIds)
        {
            WrappedShapeIds = shapeIds;
        }

        #region Custom type descriptor
        // implement the Custom Type Descriptor so that the shape list appears as an expandable list of sub objects, rather than appear as a
        // list edit dialog. Can also override it so it shows the actual shape instead of just the IDs.

        #region Require boiler plate
        public String GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public String GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
        #endregion
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            // Create a new collection object PropertyDescriptorCollection
            PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

            // Iterate the list of employees
            for (int i = 0; i < WrappedShapeIds.Count; i++)
            {
                // For each employee create a property descriptor 
                // and add it to the 
                // PropertyDescriptorCollection instance
                //ShapePropertyDescriptor pd = new ShapePropertyDescriptor(WrappedShapeIds, i);
                ShapePropertyDescriptor pd = new ShapePropertyDescriptor(WrappedShapeIds[i], $"[{i}]");
                pds.Add(pd);
            }
            return pds;
        }
        #endregion
    }
}
