using System.Runtime.CompilerServices;
using System.ComponentModel;
using Astrostrider.Commands;
using Astrostrider.UnityIntegration;

namespace Astrostrider.ViewModels
{
    public class UnityPresentationViewModel : INotifyPropertyChanged
    {
        #region "Fields"

        private UnityForm _form;
        private string _unityExeFilePath;
        private RelayCommand _switchPresentationCommand;

        #endregion

        #region "Properties"

        public UnityForm Form {
            get { return _form; }
            set
            {
                _form = value;
                OnPropertyChanged("Form");
            }
        }
        public string UnityExeFilePath
        {
            get { return _unityExeFilePath; }
            set
            {
                _unityExeFilePath = value;
                OnPropertyChanged("UnityExeFilePath");
            }
        }

        #endregion

        #region "Commands"

        public RelayCommand SwitchPresentationCommand
        {
            get { return _switchPresentationCommand; }
            set
            {
                _switchPresentationCommand = value;
                OnPropertyChanged("SwitchPresentationCommand");
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
