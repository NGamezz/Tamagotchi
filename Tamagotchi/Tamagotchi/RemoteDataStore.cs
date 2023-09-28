using Newtonsoft.Json;
using System.Text;

namespace Tamagotchi
{
    public class RemoteDataStore : IDataStore<Creature>
    {
        readonly HttpClient client = new();
        private readonly string url = "https://tamagotchi.hku.nl/api/Creatures";

        public async Task<bool> CreateItem(Creature item)
        {
            var result = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                string responseString = await result.Content.ReadAsStringAsync();

                var resultingCreature = JsonConvert.DeserializeObject<Creature>(responseString);

                Preferences.Set("CreatureId", resultingCreature.Id);

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Creature> ReadItem(string id)
        {
            var response = await client.GetAsync($"{url}{id}");

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

        public bool UpdateItem(Creature item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteItem(Creature item)
        {
            throw new NotImplementedException();
        }
    }
}
