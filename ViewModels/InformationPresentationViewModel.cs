using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Astrostrider.Commands;
using Astrostrider.Classes;

namespace Astrostrider.ViewModels
{
    public class InformationPresentationViewModel : INotifyPropertyChanged
    {
        #region "Fields"

        private ObservableCollection<InformationNavigationViewItem> _menuItems;
        private Dictionary<string, object> _navigationItemsDictionary;
        private RelayCommand _backCommand;

        #endregion

        #region "Properties"

        public ObservableCollection<InformationNavigationViewItem> MenuItems
        {
            get
            {
                if (_menuItems is null)
                {
                    //_menuItems = new ObservableCollection<InformationNavigationViewItem>();
                }
                return _menuItems;
            }
            set
            {
                _menuItems = value;
                OnPropertyChanged("MenuItems");
            }
        }
        public Dictionary<string, object> NavigationItemsDictionary
        {
            get { return _navigationItemsDictionary; }
            set
            {
                _navigationItemsDictionary = value;
                OnPropertyChanged("NavigationItemsDictionary");
            }
        }
        public RelayCommand BackCommand
        {
            get { return _backCommand; }
            set
            {
                _backCommand = value;
                OnPropertyChanged("BackCommand");
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
