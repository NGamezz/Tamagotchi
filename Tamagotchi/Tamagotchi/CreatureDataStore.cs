using Microsoft.Maui.Graphics.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Tamagotchi
{
    public class CreatureDataStore : IDataStore<Creature>
    {
        public CreatureDataStore()
        {
        }

        public Task<bool> CreateItem(Creature item, string id)
        {
            if (Preferences.ContainsKey(id))
            {
                return Task.FromResult(false);
            }

            string creatureString = JsonConvert.SerializeObject(item);
            Preferences.Set(id, creatureString);
            item.StorageKey = id;

            return Task.FromResult(Preferences.ContainsKey(id));
        }

        public Task<Creature> ReadItem(string id)
        {
            string itemtext = Preferences.Get(id, "");

            Creature creature = JsonConvert.DeserializeObject<Creature>(itemtext);
            return Task.FromResult(creature);
        }

        public bool UpdateItem(Creature item, string id)
        {
            if (Preferences.ContainsKey(id))
            {
                Preferences.Remove(id);
                string itemText = JsonConvert.SerializeObject(item);
                Preferences.Set(id, itemText);
                return true;
            }

            return false;
        }

        public bool DeleteItem(Creature item, string id)
        {
            if (Preferences.ContainsKey(id))
            {
                Preferences.Remove(id);
                return true;
            }

            return false;
        }
    }
}
