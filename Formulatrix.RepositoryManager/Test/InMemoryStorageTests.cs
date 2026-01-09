using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Formulatrix.RepositoryManager;
using RepositoryManager.Implementations;
using Xunit;

namespace Test;

public class InMemoryStorageTests
{
    private readonly InMemoryStorage _storage;

    public InMemoryStorageTests()
    {
        _storage = new InMemoryStorage();
    }

    [Fact]
    public void Initialize_ShouldSetIsInitialized()
    {
        _storage.Initialize();
        // We can verify initialization indirecty by checking if Add/Get works without throwing
        _storage.Add("test", new RepositoryItem { Content = "c", Type = 1 });
    }

    [Fact]
    public void Initialize_CalledTwice_ShouldThrowInvalidOperationException()
    {
        _storage.Initialize();
        Assert.Throws<InvalidOperationException>(() => _storage.Initialize());
    }

    [Fact]
    public void Add_NotInitialized_ShouldThrowInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => _storage.Add("test", new RepositoryItem { Content = "c", Type = 1 }));
    }

    [Fact]
    public void Add_NewItem_ShouldReturnTrue()
    {
        _storage.Initialize();
        var result = _storage.Add("item", new RepositoryItem { Content = "content", Type = 1 });
        Assert.True(result);
    }

    [Fact]
    public void Add_ExistingItem_ShouldReturnFalse()
    {
        _storage.Initialize();
        _storage.Add("item", new RepositoryItem { Content = "content", Type = 1 });
        var result = _storage.Add("item", new RepositoryItem { Content = "content", Type = 1 });
        Assert.False(result);
    }

    [Fact]
    public void Get_ExistingItem_ShouldReturnItem()
    {
        _storage.Initialize();
        var item = new RepositoryItem { Content = "content", Type = 1 };
        _storage.Add("item", item);
        
        var retrieved = _storage.Get("item");
        Assert.Equal(item, retrieved);
    }

    [Fact]
    public void Get_NonExistingItem_ShouldReturnNull()
    {
        _storage.Initialize();
        var retrieved = _storage.Get("missing");
        Assert.Null(retrieved);
    }

    [Fact]
    public void Remove_ExistingItem_ShouldReturnTrue()
    {
        _storage.Initialize();
        _storage.Add("item", new RepositoryItem { Content = "content", Type = 1 });
        
        var result = _storage.Remove("item");
        Assert.True(result);
        Assert.Null(_storage.Get("item"));
    }

    [Fact]
    public void Remove_NonExistingItem_ShouldReturnFalse()
    {
        _storage.Initialize();
        var result = _storage.Remove("missing");
        Assert.False(result);
    }

    [Fact]
    public void Exists_ExistingItem_ShouldReturnTrue()
    {
        _storage.Initialize();
        _storage.Add("item", new RepositoryItem { Content = "content", Type = 1 });
        Assert.True(_storage.Exists("item"));
    }

    [Fact]
    public void Exists_NonExistingItem_ShouldReturnFalse()
    {
        _storage.Initialize();
        Assert.False(_storage.Exists("missing"));
    }

    [Fact]
    public async Task Concurrency_Add_MultipleThreads_ShouldHandleCorrectly()
    {
        _storage.Initialize();
        int numberOfThreads = 100;
        var tasks = new Task[numberOfThreads];
        
        for (int i = 0; i < numberOfThreads; i++)
        {
            int index = i;
            tasks[i] = Task.Run(() => 
            {
                _storage.Add($"item-{index}", new RepositoryItem { Content = $"content-{index}", Type = 1 });
            });
        }

        await Task.WhenAll(tasks);

        for (int i = 0; i < numberOfThreads; i++)
        {
            Assert.True(_storage.Exists($"item-{i}"));
        }
    }
}
