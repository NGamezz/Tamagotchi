using System.ComponentModel;

namespace Tamagotchi
{
    public class Creature : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Hunger { get; set; }
        public float Thirst { get; set; }
        public float Boredom { get; set; }
        public float Loneliness { get; set; }
        public float Stimulated { get; set; }
        public float Tired { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
