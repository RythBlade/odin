using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telemetry.FrameData.Shapes;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    // tag the class with it's type converter so it can be expanded in a property grid view
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ShapeListWrapper : ICustomTypeDescriptor
    {
        [BrowsableAttribute(false)]
        public List<uint> WrappedShapeIds { get; }

        [BrowsableAttribute(false)]
        public ShapeDataManager ShapeDataManager { get; }

        [BrowsableAttribute(false)]
        public int FrameId { get; }

        public ShapeListWrapper(List<uint> shapeIds, ShapeDataManager shapeDataManager, int frameId)
        {
            WrappedShapeIds = shapeIds;
            ShapeDataManager = shapeDataManager;
            FrameId = frameId;
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
                ShapeFrameIdPair pair = ShapeDataManager.RetrieveShapeForFrame(WrappedShapeIds[i], (uint)FrameId);

                ShapePropertyDescriptor pd = new ShapePropertyDescriptor(pair.Shape, $"[{i}]");
                pds.Add(pd);
            }
            return pds;
        }
        #endregion
    }
}
