using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagotchi
{
    public interface IDataStore<T>
    {
        public Task<bool> CreateItem(T item);
        public Task<T> ReadItem(string id);
        public bool UpdateItem(T item);
        public bool DeleteItem(T item);
    }
}
