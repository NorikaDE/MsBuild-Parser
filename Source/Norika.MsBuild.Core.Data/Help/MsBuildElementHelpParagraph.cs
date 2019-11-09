using System.Diagnostics.CodeAnalysis;
using Norika.MsBuild.Model.Interfaces;

namespace Norika.MsBuild.Core.Data.Help
{
    [ExcludeFromCodeCoverage]
    public class MsBuildElementHelpParagraph : IMsBuildElementHelpParagraph
    {
        public MsBuildElementHelpParagraph(string name, string content, string additional)
        {
            Additional = additional;
            Content = content;
            Name = name;
        }

        public string Name { get; private set; }
        public string Content { get; private set; }
        public string Additional { get; private set; }
    }
}