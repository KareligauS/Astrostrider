using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Astrostrider.Classes
{
    public class BaseSpaceClass : INotifyPropertyChanged
    {
        private string _name;

        private string _description;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public BaseSpaceClass() { }
        public BaseSpaceClass(string name)
        {
            Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
