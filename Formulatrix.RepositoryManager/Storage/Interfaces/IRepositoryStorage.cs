namespace Formulatrix.RepositoryManager.Storage.Interfaces;
public interface IRepositoryStorage
{
    void Initialize();
    bool Add(string key, RepositoryItem item);
    RepositoryItem? Get(string key);
    bool Remove(string key);
    bool Exists(string key);
}