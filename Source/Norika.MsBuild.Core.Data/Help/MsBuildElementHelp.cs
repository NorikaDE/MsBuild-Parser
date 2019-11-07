using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Norika.MsBuild.Model.Interfaces;

namespace Norika.MsBuild.Core.Data.Help
{
    public class MsBuildElementHelp : IMsBuildElementHelp
    {
        private readonly IList<IMsBuildElementHelpParagraph> _paragraphs;

        public MsBuildElementHelp()
        {
            _paragraphs = new List<IMsBuildElementHelpParagraph>();
        }
        
        public IEnumerator<IMsBuildElementHelpParagraph> GetEnumerator()
        {
            return _paragraphs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IMsBuildElementHelpParagraph item)
        {
            _paragraphs.Add(item);
        }

        public void Clear()
        {
            _paragraphs.Clear();
        }

        public bool Contains(IMsBuildElementHelpParagraph item)
        {
            return _paragraphs.Contains(item);
        }

        public void CopyTo(IMsBuildElementHelpParagraph[] array, int arrayIndex)
        {
            _paragraphs.CopyTo(array, arrayIndex);
        }
            
            
        public bool Remove(IMsBuildElementHelpParagraph item)
        {
            return _paragraphs.Remove(item);
        }

        public int Count => _paragraphs.Count;
        public bool IsReadOnly => _paragraphs.IsReadOnly;
            
        public int IndexOf(IMsBuildElementHelpParagraph item)
        {
            return _paragraphs.IndexOf(item);
        }

        public void Insert(int index, IMsBuildElementHelpParagraph item)
        {
            _paragraphs.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _paragraphs.RemoveAt(index);
        }

        public IMsBuildElementHelpParagraph this[int index]
        {
            get => _paragraphs[index];
            set => _paragraphs[index] = value;
        }

        public IList<IMsBuildElementHelpParagraph> LookUp(string paragraphName)
        {
            return _paragraphs.Where(p => p.Name.Equals(paragraphName)).ToList();
        }

        public IList<IMsBuildElementHelpParagraph> LookUp(string paragraphName, StringComparison comparison)
        {
            return _paragraphs.Where(p => p.Name.Equals(paragraphName, comparison)).ToList();
        }

        public bool Remove(string paragraphName, bool distinctOnly)
        {
            IList<IMsBuildElementHelpParagraph> removeItems =
                _paragraphs.Where(x => x.Name.Equals(paragraphName)).ToList();

            return removeItems.Aggregate(true, (current, paragraph) => current && _paragraphs.Remove(paragraph));
        }

        public bool ContainsSection(string sectionName, StringComparison stringComparison)
        {
            return _paragraphs.Any(p => p.Name.Equals(sectionName, stringComparison));
        }

        public string GetSectionContent(string sectionName, StringComparison stringComparison)
        {
            return LookUp(sectionName, stringComparison).FirstOrDefault()?.Content;
        }
    }
}