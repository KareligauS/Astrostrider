using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using ModernWpf.Controls;

namespace Astrostrider.Classes
{
    public class InformationNavigationViewItem : INotifyPropertyChanged
    {
        #region "Fields"

        private string _name;
        private string _tooltip;
        private Symbol _glyph;
        private object _page;
        private ObservableCollection<InformationNavigationViewItem> _menuItems;
        private bool _isSelectable = true;

        #endregion

        #region "Properties"

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Tooltip
        {
            get { return _tooltip; }
            set
            {
                _tooltip = value;
                OnPropertyChanged("Tooltip");
            }
        }
        public Symbol Glyph
        {
            get { return _glyph; }
            set
            {
                _glyph = value;
                OnPropertyChanged("Glyph");
            }
        }
        public object Page
        {
            get { return _page; }
            set
            {
                _page = value;
                OnPropertyChanged("Page");
            }
        }
        public ObservableCollection<InformationNavigationViewItem> MenuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                OnPropertyChanged("MenuItems");
            }
        }
        public bool IsSelectable
        {
            get { return _isSelectable; }
            set
            {
                _isSelectable = value;
                OnPropertyChanged("IsSelectable");
            }
        }

        #endregion

        #region "Constructors"

        public InformationNavigationViewItem() { }
        public InformationNavigationViewItem(string name)
        {
            Name = name;
        }

        #endregion

        public void AddRange(params InformationNavigationViewItem[] items)
        {
            List<InformationNavigationViewItem> tempCollection = new List<InformationNavigationViewItem>();
            tempCollection.AddRange(items);
            MenuItems = new ObservableCollection<InformationNavigationViewItem>(tempCollection);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
