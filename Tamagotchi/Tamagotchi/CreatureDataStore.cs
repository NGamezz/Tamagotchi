using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tamagotchi
{
    public class CreatureDataStore : IDataStore<Creature>
    {
        private string storageKey = string.Empty;

        public CreatureDataStore(string _storageKey)
        {
            storageKey = _storageKey;
        }

        public Task<bool> CreateItem(Creature item)
        {
            if (Preferences.ContainsKey(storageKey))
            {
                return Task.FromResult(false);
            }

            string creatureString = JsonConvert.SerializeObject(item);
            Preferences.Set(storageKey, creatureString);

            return Task.FromResult(Preferences.ContainsKey(storageKey));
        }

        public Task<Creature> ReadItem(string id)
        {
            string itemtext = Preferences.Get(id.ToString(), "");

            Creature creature = JsonConvert.DeserializeObject<Creature>(itemtext);
            return Task.FromResult(creature);
        }

        public bool UpdateItem(Creature item)
        {
            return false;
        }

        public bool DeleteItem(Creature item)
        {
            return false;
        }
    }
}
