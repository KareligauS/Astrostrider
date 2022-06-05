using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ModernWpf.Controls;
using Astrostrider.Classes;

namespace Astrostrider.Presentations
{
    /// <summary>
    /// Логика взаимодействия для InformationPresentation.xaml
    /// </summary>
    public partial class InformationPresentation : UserControl
    {
        #region "Properties"

        public ObservableCollection<InformationNavigationViewItem> MenuItemsSource
        {
            get { return (ObservableCollection<InformationNavigationViewItem>)GetValue(MenuItemsSourceProperty); }
            set { SetValue(MenuItemsSourceProperty, value); }
        }
        public Dictionary<string, object> NavigationItemsDictionary
        {
            get { return (Dictionary<string, object>)GetValue(NavigationItemsDictionaryProperty); }
            set { SetValue(NavigationItemsDictionaryProperty, value); }
        }
        public ICommand BackCommand
        {
            get { return (ICommand)GetValue(BackCommandProperty); }
            set { SetValue(BackCommandProperty, value); }
        }

        #endregion

        #region "Dependecy Injection"

        public static readonly DependencyProperty MenuItemsSourceProperty =
            DependencyProperty.Register("MenuItemsSource", typeof(ObservableCollection<InformationNavigationViewItem>), typeof(InformationPresentation), new UIPropertyMetadata(null));
        public static readonly DependencyProperty NavigationItemsDictionaryProperty =
            DependencyProperty.Register("NavigationItemsDictionary", typeof(Dictionary<string, object>), typeof(InformationPresentation), new UIPropertyMetadata(null));
        public static readonly DependencyProperty BackCommandProperty =
            DependencyProperty.Register("BackCommand", typeof(ICommand), typeof(InformationPresentation), new UIPropertyMetadata(null));

        #endregion

        public InformationPresentation()
        {
            InitializeComponent();

            Loaded += delegate
            {
                try
                {
                    InformationNavigationView.SelectedItem = MenuItemsSource[0];
                }
                catch (Exception e)
                {
                    // NOT IMPLEMENTED
                }
            };
        }

        private void OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                //MainFrame.Navigate(typeof(SampleSettingsPage));
            }
            else
            {
                InformationNavigationViewItem selectedItem = (InformationNavigationViewItem)args.SelectedItem;
                MainFrame.Navigate(selectedItem.Page);
            }
        }
    }
}
