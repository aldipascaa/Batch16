
using System.Text.Json;
using Formulatrix.RepositoryManager.Content.Interfaces;

namespace Formulatrix.RepositoryManager.Content;

    public class JsonValidator : IContentValidator
    {
        public void Validate(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                 throw new ArgumentException("Content cannot be empty.");
            }
            try
            {
                JsonDocument.Parse(content);
            }
            catch (JsonException ex)
            {
                throw new ArgumentException("Invalid JSON content.", ex);
            }
        }
    }