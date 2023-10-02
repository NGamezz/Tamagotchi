using System.Timers;

namespace Tamagotchi
{
    public class GameManager
    {
        public Creature MyCreature { get; set; } = new Creature();

        public System.Timers.Timer timer = new System.Timers.Timer()
        {
            Interval = 5000.0f,
            AutoReset = true
        };

        public GameManager()
        {
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        public void UpdateCreature(Creature creatureToUpdate)
        {
            var dataStore = DependencyService.Get<IDataStore<Creature>>();
            dataStore.UpdateItem(creatureToUpdate, creatureToUpdate.StorageKey);
        }

        public bool FirstRun { get; private set; } = true;

        #region UpdateValues
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            MyCreature.Sleeping();
            if (MyCreature.Thirst < 1.0f)
            {
                MyCreature.Thirst += 0.01f;
            }
            if (MyCreature.Hunger < 1.0f)
            {
                MyCreature.Hunger += 0.01f;
            }
            if (MyCreature.Stimulated > 0f)
            {
                MyCreature.Stimulated -= 0.01f;
            }
            if (MyCreature.Loneliness < 1.0f)
            {
                MyCreature.Loneliness += 0.01f;
            }
            if (MyCreature.Boredom < 1.0f)
            {
                MyCreature.Boredom += 0.01f;
            }
            if (!MyCreature.IsAsleep && MyCreature.Tired < 1.0f)
            {
                MyCreature.Tired += 0.01f;
            }
        }
        #endregion

        public async void Setup(string creatureName)
        {
            if (FirstRun == false) { return; }

            var dataStore = DependencyService.Get<IDataStore<Creature>>();

            if (Preferences.ContainsKey(creatureName))
            {
                //string id = Preferences.Get("CreatureId", string.Empty);
                MyCreature = await dataStore.ReadItem(creatureName);
            }
            else
            {
                MyCreature = new Creature()
                {
                    Name = creatureName,
                    Loneliness = 1.0f,
                    Boredom = 1.0f,
                    Tired = 1.0f,
                    Stimulated = .0f,
                    Hunger = 1.0f,
                    Thirst = 1.0f,
                    StorageKey = creatureName
                };

                await dataStore.CreateItem(MyCreature, creatureName);
            }

            FirstRun = false;
        }
    }

}