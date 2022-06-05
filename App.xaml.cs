using System;
using System.Windows;
using Astrostrider.ViewModels;

namespace Astrostrider
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _mainWindow;
        private MainWindowRootViewModel _rootViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _rootViewModel = new MainWindowRootViewModel();
            _mainWindow = new MainWindow() { DataContext = _rootViewModel };
            _mainWindow.Closed += OnMainWindowClosed;
            _mainWindow.Closing += _rootViewModel.OnWindowExit;
            _mainWindow.Show();
        }

        private void OnMainWindowClosed(object sender, EventArgs e)
        {
            Shutdown();
        }
    }
}
