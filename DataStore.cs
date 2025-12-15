using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MiniDataManager.Services
{
    // Generisk datalagringsklass
    public class DataStore<T>
    {
        private readonly string _file = "data.json";
        private List<T> _items = new List<T>();

        public DataStore()
        {
            Load();
        }

        public List<T> GetAll()
        {
            return _items;
        }

        public void Add(T item)
        {
            _items.Add(item);
            Save();
        }

        public void Remove(Predicate<T> match)
        {
            _items.RemoveAll(match);
            Save();
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(
                _items,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(_file, json);
        }

        private void Load()
        {
            try
            {
                if (File.Exists(_file))
                {
                    var json = File.ReadAllText(_file);
                    _items = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
                }
            }
            catch
            {
                _items = new List<T>();
            }
        }
    }
}


