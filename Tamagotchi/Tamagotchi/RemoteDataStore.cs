using Newtonsoft.Json;
using System.Text;

namespace Tamagotchi
{
    public class RemoteDataStore : IDataStore<Creature>
    {
        readonly HttpClient client = new();
        private readonly string url = "https://tamagotchi.hku.nl/api/Creatures";

        public async Task<bool> CreateItem(Creature item, string storageKey)
        {
            var result = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                string responseString = await result.Content.ReadAsStringAsync();

                var resultingCreature = JsonConvert.DeserializeObject<Creature>(responseString);

                Preferences.Set(storageKey, resultingCreature.Id.ToString());

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Creature> ReadItem(string id)
        {
            var response = await client.GetAsync($"{url}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string resultContents = await response.Content.ReadAsStringAsync();

                var creature = JsonConvert.DeserializeObject<Creature>(resultContents);

                return creature;
            }

            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {

                }

                return null;
            }
        }

        public bool UpdateItem(Creature item, string id)
        {
            if (Preferences.ContainsKey(id))
            {
                string itemText = JsonConvert.SerializeObject(item);
                client.PutAsync($"{url}/{item.Id}", new StringContent(itemText, Encoding.UTF8, "application/json"));
                return true;
            }

            return false;
        }

        public bool DeleteItem(Creature item, string id)
        {
            if (Preferences.ContainsKey(id))
            {
                client.DeleteAsync($"{url}/{item.Id}");
                return true;
            }

            return false;
        }
    }
}
