using System;
using System.Collections.Concurrent;
using Formulatrix.RepositoryManager;
using Formulatrix.RepositoryManager.Storage.Interfaces;

namespace RepositoryManager.Implementations
{
    public class InMemoryStorage : IRepositoryStorage
    {
        private ConcurrentDictionary<string, RepositoryItem> _storage = new ConcurrentDictionary<string, RepositoryItem>();
        private bool _isInitialized = false;
        private readonly object _lock = new object();

        public void Initialize()
        {
            lock (_lock)
            {
                if (_isInitialized)
                {
                    throw new InvalidOperationException("Repository is already initialized.");
                }
                _isInitialized = true;
                _storage.Clear();
            }
        }

        public bool Add(string key, RepositoryItem item)
        {
            EnsureInitialized();
            return _storage.TryAdd(key, item);
        }

        public RepositoryItem? Get(string key)
        {
            EnsureInitialized();
            _storage.TryGetValue(key, out var item);
            return item;
        }

        public bool Remove(string key)
        {
            EnsureInitialized();
            return _storage.TryRemove(key, out _);
        }

        public bool Exists(string key)
        {
             EnsureInitialized();
             return _storage.ContainsKey(key);
        }

        private void EnsureInitialized()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Repository must be initialized before use.");
            }
        }
    }
}
