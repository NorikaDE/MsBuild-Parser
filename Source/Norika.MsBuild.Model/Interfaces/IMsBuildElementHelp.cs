using System;
using System.Collections.Generic;

namespace Norika.MsBuild.Model.Interfaces
{
    public interface IMsBuildElementHelp : IList<IMsBuildElementHelpParagraph>
    {
        IList<IMsBuildElementHelpParagraph> LookUp(string paragraphName);
            
        IList<IMsBuildElementHelpParagraph> LookUp(string paragraphName, StringComparison comparison);

        bool Remove(string paragraphName, bool distinctOnly);

        bool ContainsSection(string sectionName, StringComparison stringComparison);
        
        string GetSectionContent(string sectionName, StringComparison stringComparison);
    }
}