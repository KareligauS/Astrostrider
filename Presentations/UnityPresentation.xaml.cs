using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Astrostrider.UnityIntegration;
using System.Windows.Input;

namespace Astrostrider.Presentations
{
    /// <summary>
    /// Логика взаимодействия для UnityControl.xaml
    /// </summary>
    public partial class UnityPresentation : System.Windows.Controls.UserControl
    {
        #region "Fields"

        public bool IsUnityLoaded = false;
        private WindowsFormsHost _host;

        #endregion

        #region "Properties"

        public WindowsFormsHost Host
        {
            get
            {
                if (_host is null)
                {
                    _host = new WindowsFormsHost();
                }
                return _host;
            }
        }
        public UnityForm UnityForm
        {
            get { return (UnityForm)GetValue(UnityFormProperty); }
            set { SetValue(UnityFormProperty, value); }
        }
        public string ExecutableFilePath
        {
            get { return (string)GetValue(ExecutableFilePathProperty); }
            set { SetValue(ExecutableFilePathProperty, value); }
        }
        public ICommand SwitchPresentationCommand
        {
            get { return (ICommand)GetValue(SwitchPresentationCommandProperty); }
            set { SetValue(SwitchPresentationCommandProperty, value); }
        }

        #endregion

        #region "Dependency Injection"

        public static readonly DependencyProperty UnityFormProperty =
            DependencyProperty.Register("UnityForm", typeof(UnityForm), typeof(UnityPresentation), new UIPropertyMetadata(null));
        public static readonly DependencyProperty ExecutableFilePathProperty =
            DependencyProperty.Register("ExecutableFilePath", typeof(string), typeof(UnityPresentation), new UIPropertyMetadata(null));
        public static readonly DependencyProperty SwitchPresentationCommandProperty =
            DependencyProperty.Register("SwitchPresentationCommand", typeof(ICommand), typeof(UnityPresentation), new UIPropertyMetadata(null));
        
        #endregion

        public UnityPresentation()
        {
            InitializeComponent();
        }

        private void On_Load(object sender, RoutedEventArgs e)
        {
            if (!IsUnityLoaded)
            {
                UnityForm = new UnityForm(ExecutableFilePath);
                Host.Child = UnityForm;
                Grid.SetRow(Host, 0);
                Container.Children.Add(Host);
                Host.Focus();
                IsUnityLoaded = true;
            }
        }
    }
}
