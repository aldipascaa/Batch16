using System.Xml;
using System.Xml.Linq;
using Formulatrix.RepositoryManager.Content.Interfaces;

namespace Formulatrix.RepositoryManager.Content;

public class XmlValidator : IContentValidator
{
    public void Validate(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
                throw new ArgumentException("Content cannot be empty.");
        }
        try
        {
            XDocument.Parse(content);
        }
        catch (XmlException ex)
        {
            throw new ArgumentException("Invalid XML content.", ex);
        }
    }
}