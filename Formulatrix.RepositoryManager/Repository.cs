using System;
using System.Collections.Generic;
using Formulatrix.RepositoryManager.Content;
using Formulatrix.RepositoryManager.Content.Interfaces;
using Formulatrix.RepositoryManager.Storage.Interfaces;
using RepositoryManager.Implementations;

namespace Formulatrix.RepositoryManager;

public class Repository
{
    private readonly IRepositoryStorage _storage;
    private readonly Dictionary<int, IContentValidator> _validator;

    public Repository(IRepositoryStorage? storage=null)
    {
        _storage = storage?? new InMemoryStorage();
        _validator = new Dictionary<int, IContentValidator>
        {
          {1, new JsonValidator()},
          {2, new XmlValidator()}  
        };
    }

    public void Initialize()
    {
        _storage.Initialize();
    }

    public void Register(string itemName, string itemContent, int itemType)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            throw new ArgumentException("Item name cannot be null or empty.", nameof(itemName));
        }

        if (_storage.Exists(itemName))
        {
            throw new InvalidOperationException($"Item with name '{itemName}' already exists. Overwriting is not allowed.");
        }

        ValidateContent(itemContent, itemType);

        var item = new RepositoryItem
        {
            Content = itemContent,
            Type = itemType
        };

        if (!_storage.Add(itemName, item))
        {
            throw new InvalidOperationException($"Item with name '{itemName}' already exists.");
        }
    }

    public string Retreive(string itemName)
    {
        var item = _storage.Get(itemName);

        if(item is null)
            throw new InvalidOperationException($"Item {itemName} Not Fould");
    
        return item.Content;
    }

    public int GetType(string itemName)
    {
        var item = _storage.Get(itemName);

        if(item is null)
            throw new InvalidOperationException($"Item {itemName} Not Fould");
        
        return item.Type;
    }

    public void Deregister(string itemName)
    {
        var item = _storage.Get(itemName);

        if (item is null)
            throw new InvalidOperationException($"Item {itemName} Not Fould");
        
        _storage.Remove(itemName);
    }

    private void ValidateContent(string content, int type)
    {
        if (_validator.TryGetValue(type, out var validator))
            validator.Validate(content);
        else
            throw new ArgumentException($"Invalid item type '{type}'. No validator registered for this type.");
    }

}
