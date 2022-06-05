using System;
using System.Windows;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Astrostrider.Commands;
using Astrostrider.Classes;
using Astrostrider.Pages;
using Astrostrider.UnityConnection;
using ModernWpf.Controls;
using RiptideNetworking;

namespace Astrostrider.ViewModels
{
    public class MainWindowRootViewModel : INotifyPropertyChanged
    {
        #region "Fields"
        private object _currentContentVM;
        private SelectionPresentationViewModel _selectionPresentationVM;
        private InformationPresentationViewModel _informationPresentationVM;
        private UnityPresentationViewModel _unityPresentationVM;
        private Dictionary<string, ObservableCollection<InformationNavigationViewItem>> _informationPresentationArchive;
        private RelayCommand _updateInformationPresentationContentCommand;
        private RelayCommand _switchPresentationCommand;
        private RelayCommand _switchToSelectionPresentationCommand;
        private RelayCommand _switchToUnityPresentationCommand;
        private RelayCommand _switchToInformationPresentationCommand;
        private RelayCommand _addInformationPresentationItemCommand;
        private UnityServer _server;
        #endregion

        #region "Properties"
        public object CurrentContentVM
        {
            get { return _currentContentVM; }
            set
            {
                _currentContentVM = value;
                OnPropertyChanged("CurrentContentVM");
            }
        }
        public SelectionPresentationViewModel SelectionPresentationVM
        {
            get { return _selectionPresentationVM; }
            set
            {
                _selectionPresentationVM = value;
                OnPropertyChanged("SelectionPresentationVM");
            }
        }
        public InformationPresentationViewModel InformationPresentationVM
        {
            get { return _informationPresentationVM; }
            set
            {
                _informationPresentationVM = value;
                OnPropertyChanged("InformationPresentationVM");
            }
        }
        public UnityPresentationViewModel UnityPresentationVM
        {
            get { return _unityPresentationVM; }
            set
            {
                _unityPresentationVM = value;
                OnPropertyChanged("UnityPresentationVM");
            }
        }
        public Dictionary<string, ObservableCollection<InformationNavigationViewItem>> InformationPresentationArchive
        {
            get
            {
                if (_informationPresentationArchive is null)
                {
                    _informationPresentationArchive = new Dictionary<string, ObservableCollection<InformationNavigationViewItem>>();
                }
                return _informationPresentationArchive;
            }
            set
            {
                _informationPresentationArchive = value;
                OnPropertyChanged("InformationPresentationArchive");
            }
        }
        public UnityServer Server
        {
            get { return _server; }
            set
            {
                _server = value;
                OnPropertyChanged("Server");
            }
        }

        #endregion

        #region "Commands"

        public RelayCommand UpdateInformationPresentationContentCommand
        {
            get
            {
                return _updateInformationPresentationContentCommand ?? (_updateInformationPresentationContentCommand = new RelayCommand(obj =>
                {
                    string objectName = obj as string;
                    if (InformationPresentationArchive.ContainsKey(objectName))
                    {
                        ObservableCollection<InformationNavigationViewItem> tempCollection = new ObservableCollection<InformationNavigationViewItem>();
                        InformationPresentationArchive.TryGetValue(objectName, out tempCollection);
                        InformationPresentationVM.MenuItems = tempCollection;
                    }
                    else
                    {
                        throw new Exception("Внимание: Контент отсутствует!");
                    }
                }));
            }
        }
        public RelayCommand SwitchPresentationCommand
        {
            get
            {
                return _switchPresentationCommand ?? (_switchPresentationCommand = new RelayCommand(obj =>
                {
                    CurrentContentVM = obj;
                }));
            }
        }
        public RelayCommand SwitchToSelectionPresentationCommand
        {
            get
            {
                return _switchToSelectionPresentationCommand ?? (_switchToSelectionPresentationCommand = new RelayCommand(obj =>
                {
                    SwitchPresentationCommand.Execute(SelectionPresentationVM);
                }));
            }
        }
        public RelayCommand SwitchToUnityPresentationCommand
        {
            get
            {
                return _switchToUnityPresentationCommand ?? (_switchToUnityPresentationCommand = new RelayCommand(obj =>
                {
                    Server.Start();
                    Server.Server.ClientDisconnected += (object sender, ClientDisconnectedEventArgs e) => { UnityPresentationVM.Form.Close(); UnityPresentationVM.SwitchPresentationCommand.Execute("Next"); Server.Stop(); };
                    SwitchPresentationCommand.Execute(UnityPresentationVM);
                }));
            }
        }
        public RelayCommand SwitchToInformationPresentationCommand
        {
            get
            {
                return _switchToInformationPresentationCommand ?? (_switchToInformationPresentationCommand = new RelayCommand(obj =>
                {
                    SwitchPresentationCommand.Execute(InformationPresentationVM);
                }));
            }
        }
        public RelayCommand AddInformationPresentationItemCommand
        {
            get
            {
                return _addInformationPresentationItemCommand ?? (_addInformationPresentationItemCommand = new RelayCommand(obj =>
                {
                    Dictionary<string, ObservableCollection<InformationNavigationViewItem>> dictionary = obj as Dictionary<string, ObservableCollection<InformationNavigationViewItem>>;
                    InformationPresentationArchive.Union(dictionary);
                }));
            }
        }

        #endregion

        public MainWindowRootViewModel()
        {
            Server = new UnityServer(7777, 1);
            SelectionPresentationVM = new SelectionPresentationViewModel();
            SelectionPresentationVM.SwitchPresentationCommand = SwitchToUnityPresentationCommand;
            SelectionPresentationVM.UpdateInformationPresentationContentCommand = UpdateInformationPresentationContentCommand;
            InformationPresentationVM = new InformationPresentationViewModel();
            InformationPresentationVM.BackCommand = SwitchToSelectionPresentationCommand;
            UnityPresentationVM = new UnityPresentationViewModel();
            UnityPresentationVM.UnityExeFilePath = @"Resources\UnityGame\UnityGame.exe";
            UnityPresentationVM.SwitchPresentationCommand = SwitchToInformationPresentationCommand;

            #region "Information Presentation Items Initialization"

            ObservableCollection<InformationNavigationViewItem> tempCollection = new ObservableCollection<InformationNavigationViewItem>();
            tempCollection.Add(new InformationNavigationViewItem() { Name = "Intro", Glyph = Symbol.Like, Page = new SunIntro(), Tooltip = "This is Intro" });
            tempCollection.Add(new InformationNavigationViewItem() { Name = "Info", Glyph = Symbol.List, Page = new SunInfoHead(), Tooltip = "This is Hierarchy", IsSelectable = false });
            tempCollection[1].AddRange(
                new InformationNavigationViewItem() { Name = "Sub1", Glyph = Symbol.Important, Page = new SunInfoSub1(), Tooltip = "This is Item" },
                new InformationNavigationViewItem() { Name = "Sub2", Glyph = Symbol.UnPin, Page = new SunInfoSub2(), Tooltip = "This is Item" }
            );
            tempCollection.Add(new InformationNavigationViewItem() { Name = "Outro", Glyph = Symbol.LikeDislike, Page = new SunOutro(), Tooltip = "This is Outro" });
            InformationPresentationArchive.Add("Солнце", tempCollection);

            tempCollection = new ObservableCollection<InformationNavigationViewItem>();
            tempCollection.Add(new InformationNavigationViewItem() { Name = "Intro", Glyph = Symbol.Like, Page = new SunIntro(), Tooltip = "This is Intro" });
            tempCollection.Add(new InformationNavigationViewItem() { Name = "Info", Glyph = Symbol.List, Page = new SunInfoHead(), Tooltip = "This is Hierarchy", IsSelectable = false });
            tempCollection[1].AddRange(
                new InformationNavigationViewItem() { Name = "Sub1", Glyph = Symbol.Important, Page = new SunInfoSub1(), Tooltip = "This is Item" },
                new InformationNavigationViewItem() { Name = "Sub2", Glyph = Symbol.UnPin, Page = new SunInfoSub2(), Tooltip = "This is Item" }
            );
            tempCollection.Add(new InformationNavigationViewItem() { Name = "Outro", Glyph = Symbol.LikeDislike, Page = new SunOutro(), Tooltip = "This is Outro" });
            InformationPresentationArchive.Add("Луна", tempCollection);

            #endregion

            CurrentContentVM = SelectionPresentationVM;
        }

        public void OnWindowExit(object sender, EventArgs e)
        {
            try
            {
                if (Server.Server.IsRunning)
                {
                    Server.CloseUnityApplication();
                }
            }
            catch (Exception exc)
            {

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