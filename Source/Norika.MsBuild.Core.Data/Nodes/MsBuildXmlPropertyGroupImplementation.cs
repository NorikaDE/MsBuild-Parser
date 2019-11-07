using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Norika.MsBuild.Core.Data.Elements;
using Norika.MsBuild.Core.Data.Types;
using Norika.MsBuild.Model.Interfaces;

namespace Norika.MsBuild.Core.Data.Nodes
{
    public class MsBuildXmlPropertyGroupImplementation : MsBuildXmlNode ,IMsBuildPropertyGroup
    {
        public new static string XmlElementName = "PropertyGroup";
        
        private readonly IList<IMsBuildProperty> _properties;

        private readonly XmlElement _element;

        private MsBuildXmlPropertyGroupImplementation(XmlElement element, IList<IMsBuildProperty> properties) : base(element)
        {
            _element = element ?? throw new ArgumentNullException(nameof(element), "The xml element implementation should not be null!");
            _properties = properties;

            InitializeProperties();
            
        }

        private void InitializeProperties()
        {
            foreach (XmlElement property in _element.ChildNodes.OfType<XmlElement>())
            {
                if(property.NodeType != XmlNodeType.Comment)
                    _properties.Add(new MsBuildXmlPropertyImplementation(property));
            }
        }

        public MsBuildXmlPropertyGroupImplementation(XmlElement element) : this(element, new List<IMsBuildProperty>()) { }

        public IEnumerator<IMsBuildProperty> GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IMsBuildProperty item)
        {
            _properties.Add(item);
        }

        public void Clear()
        {
            _properties.Clear();
        }

        public bool Contains(IMsBuildProperty item)
        {
            return _properties.Contains(item);
        }

        public void CopyTo(IMsBuildProperty[] array, int arrayIndex)
        {
            _properties.CopyTo(array, arrayIndex);
        }

        public bool Remove(IMsBuildProperty item)
        {
            return _properties.Remove(item);
        }

        public int Count => _properties.Count;

        public bool IsReadOnly => _properties.IsReadOnly;
        
        public int IndexOf(IMsBuildProperty item)
        {
            return _properties.IndexOf(item);
        }

        public void Insert(int index, IMsBuildProperty item)
        {
            _properties.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _properties.RemoveAt(index);
        }

        public IMsBuildProperty this[int index]
        {
            get => _properties[index];
            set => _properties[index] = value;
        }
        
        public new IList<T> GetChildren<T>() where T : class, IMsBuildElement
        {
            if (typeof(T) == typeof(IMsBuildProperty))
            {
                return (IList<T>) _properties;
            }
            return new List<T>();
        }
    }
}