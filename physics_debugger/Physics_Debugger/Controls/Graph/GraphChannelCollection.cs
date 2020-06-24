using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace physics_debugger.Controls.Graph
{
    public class GraphChannelCollection : ICustomTypeDescriptor
    {
        public List<GraphChannel> Channels { get; set; } = new List<GraphChannel>();

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
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

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            // Create a new collection object PropertyDescriptorCollection
            PropertyDescriptorCollection descriptionCollection = new PropertyDescriptorCollection(null);

            // Iterate the list of channels
            for (int i = 0; i < Channels.Count; i++)
            {
                // Create a property descriptor for each channel so we can customise it's appearence in the property grid view
                GraphChannelPropertyDescription description = new GraphChannelPropertyDescription(Channels[i]);
                descriptionCollection.Add(description);
            }
            return descriptionCollection;
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }
    }
}
