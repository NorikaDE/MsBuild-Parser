using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Norika.MsBuild.Core.Data.Types;
using Norika.MsBuild.Core.Data.Utilities;
using Norika.MsBuild.Model.Interfaces;

namespace Norika.MsBuild.Core.Data.Nodes
{
    public class MsBuildXmlProjectImplementation : MsBuildXmlNode, IMsBuildProject
    {
        private readonly IList<IMsBuildElement> _elements;

        private XmlElement _element;

        public MsBuildXmlProjectImplementation(XmlDocument document) : base(document.DocumentElement)
        {
            _elements = new List<IMsBuildElement>();
            _element = document.DocumentElement;

            InitializeAttributes();
            MsBuildElementFactory factory = new MsBuildElementFactory();

            InitializeContent<IMsBuildTarget>(factory);
            InitializeContent<IMsBuildPropertyGroup>(factory);
        }

        private void InitializeContent<T>(MsBuildElementFactory factory) where T : IMsBuildElement
        {
            foreach (XmlElement childElement in _element.ChildNodes.OfType<XmlElement>())
            {
                T target = factory.Create<T>(childElement);

                if (target != null)
                    _elements.Add(target);
            }
        }

        public override IList<T> GetChildren<T>()
        {
            if (typeof(T) == typeof(IMsBuildProperty))
            {
                List<T> properties = new List<T>();

                foreach (var propertyGroup in _elements.OfType<IMsBuildPropertyGroup>())
                {
                    properties.AddRange(propertyGroup.GetChildren<T>());
                }

                return properties;
            }

            return _elements.OfType<T>().ToList();
        }

        private void InitializeAttributes()
        {
            DefaultTargets = new List<string>();
            InitialTargets = new List<string>();

            MsBuildVersion = (string.IsNullOrWhiteSpace(XmlElement.GetAttribute("ToolsVersion"))
                ? null
                : XmlElement.GetAttribute("ToolsVersion"));
            DefaultTargets = XmlElement.GetAttribute("DefaultTargets")?.SplitByDefaultSeparator();
            InitialTargets = XmlElement.GetAttribute("InitialTargets")?.SplitByDefaultSeparator();
        }

        public IEnumerator<IMsBuildElement> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IMsBuildElement item)
        {
            _elements.Add(item);
        }

        public void Clear()
        {
            _elements.Clear();
        }

        public bool Contains(IMsBuildElement item)
        {
            return _elements.Contains(item);
        }

        public void CopyTo(IMsBuildElement[] array, int arrayIndex)
        {
            _elements.CopyTo(array, arrayIndex);
        }

        public bool Remove(IMsBuildElement item)
        {
            return _elements.Remove(item);
        }

        public int Count => _elements.Count;
        public bool IsReadOnly => _elements.IsReadOnly;

        public int IndexOf(IMsBuildElement item)
        {
            return _elements.IndexOf(item);
        }

        public void Insert(int index, IMsBuildElement item)
        {
            _elements.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _elements.RemoveAt(index);
        }

        public IMsBuildElement this[int index]
        {
            get => _elements[index];
            set => _elements[index] = value;
        }

        public string MsBuildVersion { get; set; }
        public IList<string> InitialTargets { get; set; }
        public IList<string> DefaultTargets { get; set; }
    }
}