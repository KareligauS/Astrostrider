using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows;
using Astrostrider.Classes;
using Astrostrider.Commands;
using Astrostrider.Dialogs;

namespace Astrostrider.ViewModels
{
    public class SelectionPresentationViewModel : INotifyPropertyChanged
    {
        #region "Fields"

        private SpaceObject _currentSpaceObject;
        private SpaceShip _currentSpaceShip;
        private bool _isSwitchButtonEnabled = false;
        private RelayCommand _selectSpaceObjectCommand;
        private RelayCommand _selectSpaceShipCommand;
        private RelayCommand _switchPresentationCommand;
        private RelayCommand _updateInformationPresentationContentCommand;
        private RelayCommand _addInformationPresentationItemCommand;

        #endregion

        #region "Properties"

        public SpaceObject CurrentSpaceObject
        {
            get { return _currentSpaceObject; }
            set
            {
                _currentSpaceObject = value;
                OnPropertyChanged("CurrentSpaceObject");
            }
        }
        public SpaceShip CurrentSpaceShip
        {
            get { return _currentSpaceShip; }
            set
            {
                _currentSpaceShip = value;
                OnPropertyChanged("CurrentSpaceShip");
            }
        }
        public bool IsSwitchButtonEnabled
        {
            get { return _isSwitchButtonEnabled; }
            set
            {
                _isSwitchButtonEnabled = value;
                OnPropertyChanged("IsSwitchButtonEnabled");
            }
        }

        #endregion

        #region "Commands"

        public RelayCommand SelectSpaceObjectCommand
        {
            get
            {
                return _selectSpaceObjectCommand ?? (_selectSpaceObjectCommand = new RelayCommand(obj =>
                {
                    SelectionDialog win = new SelectionDialog("spaceobject");
                    win.ShowDialog();
                    if (win.DialogResult == true)
                    {
                        CurrentSpaceObject = win.SpaceObjectViewModel.SelectedObject;
                        try
                        {
                            UpdateInformationPresentationContentCommand.Execute(CurrentSpaceObject.Name);
                            EnableSwitchButton();
                        }
                        catch (System.Exception e)
                        {
                            MessageBox.Show(e.Message, "Внимание!");
                        }
                    }
                }));
            }
        }
        public RelayCommand SelectSpaceShipCommand
        {
            get
            {
                return _selectSpaceShipCommand ?? (_selectSpaceShipCommand = new RelayCommand(obj =>
                {
                    SelectionDialog win = new SelectionDialog("spaceship");
                    win.ShowDialog();
                    if (win.DialogResult == true)
                    {
                        CurrentSpaceShip = win.SpaceShipViewModel.SelectedObject;
                        EnableSwitchButton();
                    }
                }));
            }
        }
        public RelayCommand SwitchPresentationCommand
        {
            get { return _switchPresentationCommand; }
            set
            {
                _switchPresentationCommand = value;
                OnPropertyChanged("SwitchPresentationCommand");
            }
        }
        public RelayCommand AddInformationPresenataionItemCommand { get { return _addInformationPresentationItemCommand; } set { _addInformationPresentationItemCommand = value; } }
        public RelayCommand UpdateInformationPresentationContentCommand
        {
            get { return _updateInformationPresentationContentCommand; }
            set
            {
                _updateInformationPresentationContentCommand = value;
                OnPropertyChanged("UpdateInformationPresentationContentCommand");
            }
        }

        #endregion

        public void EnableSwitchButton()
        {
            if (CurrentSpaceObject != null && CurrentSpaceShip != null)
            {
                IsSwitchButtonEnabled = true;
            }
            else
            {
                IsSwitchButtonEnabled = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}