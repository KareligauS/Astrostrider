using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Astrostrider.Managers;
using Astrostrider.Classes;
using Astrostrider.Commands;

namespace Astrostrider.ViewModels
{
    public class SelectionDialogViewModel<T> : INotifyPropertyChanged where T : BaseSpaceClass
    {
        #region "Fields"

        private IValueConverter _objectGroupKeyConverter;
        private T _selectedObject;
        private ObservableCollection<T> _filteredObjects;
        private ObservableCollection<T> _defaultObjects;
        private RelayCommand _closeCommand;
        private RelayCommand _filterCommand;
        private RelayCommand _updateCommand;
        private RelayCommand _cancelCommand;

        #endregion

        #region "Properties"

        public ObservableCollection<T> FilteredObjects
        {
            get
            {
                if (_filteredObjects is null)
                {
                    _filteredObjects = new ObservableCollection<T>();
                }
                return _filteredObjects;
            }
            set
            {
                _filteredObjects = value;
                OnPropertyChanged("FilteredObjects");
            }
        }
        public ObservableCollection<T> DefaultObjects
        {
            get
            {
                if (_defaultObjects is null)
                {
                    _defaultObjects = new ObservableCollection<T>();
                }
                return _defaultObjects;
            }
            set
            {
                _defaultObjects = value;
                OnPropertyChanged("DefaultObjects");
            }
        }
        public T SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                OnPropertyChanged("SelectedObject");
            }
        }
        public IValueConverter ObjectGroupKeyConvertor
        {
            get { return _objectGroupKeyConverter; }
            set
            {
                _objectGroupKeyConverter = value;
                OnPropertyChanged("ObjectGroupKeyConvertor");
            }
        }

        #endregion

        #region "Commands"

        public RelayCommand CloseCommand { get { return _closeCommand; } set { _closeCommand = value; } }
        public RelayCommand CancelCommand { get { return _cancelCommand; } set { _cancelCommand = value; } }
        public RelayCommand FilterCommand
        {
            get
            {
                return _filterCommand ?? (_filterCommand = new RelayCommand(obj =>
                {
                    string filterText = obj as string;
                    var filtered = DefaultObjects.Where(objectToFilter => Filter(objectToFilter, filterText));
                    RemoveNonMatching(filtered);
                    AddBack(filtered);
                }));
            }
        }
        public RelayCommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new RelayCommand(obj =>
                {
                    if (typeof(T) == typeof(SpaceObject))
                    {
                        List<T> tempFilteredCollection = new List<T>();
                        tempFilteredCollection.AddRange(JsonFileManager.Deserialize<ObservableCollection<T>>(JsonFileManager.DefaultSpaceObjectDataJsonPath));
                        tempFilteredCollection.AddRange(JsonFileManager.Deserialize<ObservableCollection<T>>(JsonFileManager.UserSpaceObjectDataJsonPath));
                        FilteredObjects = new ObservableCollection<T>(tempFilteredCollection);

                        List<T> tempDefaultCollection = new List<T>();
                        tempDefaultCollection.AddRange(JsonFileManager.Deserialize<ObservableCollection<T>>(JsonFileManager.DefaultSpaceObjectDataJsonPath));
                        tempDefaultCollection.AddRange(JsonFileManager.Deserialize<ObservableCollection<T>>(JsonFileManager.UserSpaceObjectDataJsonPath));
                        DefaultObjects = new ObservableCollection<T>(tempDefaultCollection);

                        ObjectGroupKeyConvertor = new SpaceObjectGroupKeyConverter();
                    }
                    else if (typeof(T) == typeof(SpaceShip))
                    {
                        List<T> tempFilteredCollection = new List<T>();
                        tempFilteredCollection.AddRange(JsonFileManager.Deserialize<ObservableCollection<T>>(JsonFileManager.DefaultSpaceShipDataJsonPath));
                        tempFilteredCollection.AddRange(JsonFileManager.Deserialize<ObservableCollection<T>>(JsonFileManager.UserSpaceShipDataJsonPath));
                        FilteredObjects = new ObservableCollection<T>(tempFilteredCollection);

                        List<T> tempDefaultCollection = new List<T>();
                        tempDefaultCollection.AddRange(JsonFileManager.Deserialize<ObservableCollection<T>>(JsonFileManager.DefaultSpaceShipDataJsonPath));
                        tempDefaultCollection.AddRange(JsonFileManager.Deserialize<ObservableCollection<T>>(JsonFileManager.UserSpaceShipDataJsonPath));
                        DefaultObjects = new ObservableCollection<T>(tempDefaultCollection);

                        ObjectGroupKeyConvertor = new SpaceShipGroupKeyConverter();
                    }
                }));
            }
        }

        #endregion

        #region "Methods"

        private void RemoveNonMatching(IEnumerable<T> filteredData)
        {
            for (int i = FilteredObjects.Count - 1; i >= 0; i--)
            {
                var item = FilteredObjects[i];
                if (!filteredData.Contains(item))
                {
                    FilteredObjects.Remove(item);
                }
            }
        }

        private void AddBack(IEnumerable<T> filteredData)
        {
            foreach (var item in filteredData)
            {
                if (!FilteredObjects.Contains(item))
                {
                    FilteredObjects.Add(item);
                }
            }
        }

        private bool Filter(T objectToFilter, string filterText)
        {
            return objectToFilter.Name.IndexOf(filterText, StringComparison.InvariantCultureIgnoreCase) > -1;
        }

        #endregion

        public SelectionDialogViewModel()
        {
            UpdateCommand.Execute("Update");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
