using System;
using Formulatrix.RepositoryManager;
using Formulatrix.RepositoryManager.Storage.Interfaces;
using Moq;
using Xunit;

namespace Test;

public class RepositoryTests
{
    private readonly Mock<IRepositoryStorage> _mockStorage;
    private readonly Repository _repository;

    public RepositoryTests()
    {
        _mockStorage = new Mock<IRepositoryStorage>();
        _repository = new Repository(_mockStorage.Object);
    }

    [Fact]
    public void Initialize_ShouldCallStorageInitialize()
    {
        _repository.Initialize();
        _mockStorage.Verify(x => x.Initialize(), Times.Once);
    }

    [Fact]
    public void Register_ValidJson_ShouldAddItem()
    {
        string itemName = "item1";
        string content = "{\"key\":\"value\"}";
        int type = 1; // 1 is JSON

        _mockStorage.Setup(x => x.Exists(itemName)).Returns(false);
        _mockStorage.Setup(x => x.Add(itemName, It.IsAny<RepositoryItem>())).Returns(true);

        _repository.Register(itemName, content, type);

        _mockStorage.Verify(x => x.Add(itemName, It.Is<RepositoryItem>(i => i.Content == content && i.Type == type)), Times.Once);
    }

    [Fact]
    public void Register_ValidXml_ShouldAddItem()
    {
        string itemName = "item2";
        string content = "<root></root>";
        int type = 2; // 2 is XML

        _mockStorage.Setup(x => x.Exists(itemName)).Returns(false);
        _mockStorage.Setup(x => x.Add(itemName, It.IsAny<RepositoryItem>())).Returns(true);

        _repository.Register(itemName, content, type);

        _mockStorage.Verify(x => x.Add(itemName, It.Is<RepositoryItem>(i => i.Content == content && i.Type == type)), Times.Once);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Register_InvalidName_ShouldThrowArgumentException(string invalidName)
    {
        Assert.Throws<ArgumentException>(() => _repository.Register(invalidName, "content", 1));
    }

    [Fact]
    public void Register_ExistingItem_ShouldThrowInvalidOperationException()
    {
        string itemName = "existing";
        _mockStorage.Setup(x => x.Exists(itemName)).Returns(true);

        Assert.Throws<InvalidOperationException>(() => _repository.Register(itemName, "{}", 1));
    }

    [Fact]
    public void Register_InvalidContentForType_ShouldThrowException()
    {   
        string itemName = "badjson";
        string content = "invalid-json";
        int type = 1;
        string itemName2 = "badxml";
        string content2 = "invalid-xml";
        int type2 = 2;

        string empty = "";


        _mockStorage.Setup(x => x.Exists(itemName)).Returns(false);
        _mockStorage.Setup(x => x.Exists(itemName2)).Returns(false);
        
        // We expect the validator to throw. The Repository does not catch it.
        Assert.ThrowsAny<Exception>(() => _repository.Register(itemName, content, type));
        Assert.ThrowsAny<Exception>(() => _repository.Register(itemName, empty, type));
        Assert.ThrowsAny<Exception>(() => _repository.Register(itemName2, content2, type2));
        Assert.ThrowsAny<Exception>(() => _repository.Register(itemName2, empty, type2));


    }

    [Fact]
    public void Register_UnsupportedType_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _repository.Register("item", "content", 999));
    }
    
    [Fact]
    public void Register_StorageAddFails_ShouldThrowInvalidOperationException()
    {
        string itemName = "item";
        _mockStorage.Setup(x => x.Exists(itemName)).Returns(false);
        _mockStorage.Setup(x => x.Add(itemName, It.IsAny<RepositoryItem>())).Returns(false);

        Assert.Throws<InvalidOperationException>(() => _repository.Register(itemName, "{}", 1));
    }

    [Fact]
    public void Retreive_ExistingItem_ShouldReturnContent()
    {
        string itemName = "item";
        var item = new RepositoryItem { Content = "content", Type = 1 };
        _mockStorage.Setup(x => x.Get(itemName)).Returns(item);

        var result = _repository.Retreive(itemName);

        Assert.Equal("content", result);
    }

    [Fact]
    public void Retreive_NonExistingItem_ShouldThrowInvalidOperationException()
    {
        string itemName = "missing";
        _mockStorage.Setup(x => x.Get(itemName)).Returns((RepositoryItem?)null);

        Assert.Throws<InvalidOperationException>(() => _repository.Retreive(itemName));
    }

    [Fact]
    public void GetType_ExistingItem_ShouldReturnType()
    {
        string itemName = "item";
        var item = new RepositoryItem { Content = "content", Type = 2 };
        _mockStorage.Setup(x => x.Get(itemName)).Returns(item);

        var result = _repository.GetType(itemName);

        Assert.Equal(2, result);
    }

    [Fact]
    public void GetType_NonExistingItem_ShouldThrowInvalidOperationException()
    {
        string itemName = "missing";
        _mockStorage.Setup(x => x.Get(itemName)).Returns((RepositoryItem?)null);

        Assert.Throws<InvalidOperationException>(() => _repository.GetType(itemName));
    }

    [Fact]
    public void Deregister_ExistingItem_ShouldRemoveItem()
    {
        string itemName = "item";
        var item = new RepositoryItem { Content = "content", Type = 1 };
        _mockStorage.Setup(x => x.Get(itemName)).Returns(item);

        _repository.Deregister(itemName);

        _mockStorage.Verify(x => x.Remove(itemName), Times.Once);
    }

    [Fact]
    public void Deregister_NonExistingItem_ShouldThrowInvalidOperationException()
    {
        string itemName = "missing";
        _mockStorage.Setup(x => x.Get(itemName)).Returns((RepositoryItem?)null);

        Assert.Throws<InvalidOperationException>(() => _repository.Deregister(itemName));
    }
}
