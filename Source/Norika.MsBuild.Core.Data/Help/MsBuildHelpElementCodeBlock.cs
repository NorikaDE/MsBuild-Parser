using System.Diagnostics.CodeAnalysis;

namespace Norika.MsBuild.Core.Data.Help
{
    [ExcludeFromCodeCoverage]
    public struct MsBuildHelpElementCodeBlock
    {
        public MsBuildHelpElementCodeBlock(MsBuildHelpCodeBlockLanguage language, string content)
        {
            Language = language;
            Content = content;
        }

        public MsBuildHelpCodeBlockLanguage Language { get; }
        public string Content { get; }
    }
}