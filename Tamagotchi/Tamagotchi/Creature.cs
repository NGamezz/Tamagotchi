using System.ComponentModel;

namespace Tamagotchi
{
    public class Creature : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string StorageKey { get; set; }
        public string Name { get; set; }
        public float Hunger { get; set; }
        public float Thirst { get; set; }
        public float Boredom { get; set; }
        public float Loneliness { get; set; }
        public float Stimulated { get; set; }
        public float Tired { get; set; }
        public bool IsAsleep { get; set; }

        public void Sleeping()
        {
            if (IsAsleep)
            {
                Tired -= 0.1f;
            }
            if (Tired <= 0)
            {
                IsAsleep = false;
            }
            if (Tired < 1.0f)
            {
                Tired += 0.01f;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
