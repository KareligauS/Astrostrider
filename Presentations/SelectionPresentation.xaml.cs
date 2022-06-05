using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Astrostrider.Presentations
{
    /// <summary>
    /// Логика взаимодействия для MainWindowSelectionPresentation.xaml
    /// </summary>
    public partial class SelectionPresentation : UserControl
    {
        #region "Properties"

        public string SpaceObjectSelectionButtonContent
        {
            get { return (string)GetValue(SpaceObjectSelectionButtonContentProperty); }
            set { SetValue(SpaceObjectSelectionButtonContentProperty, value); }
        }
        public string SpaceObjectDescriptionText
        {
            get { return (string)GetValue(SpaceObjectDescriptionTextProperty); }
            set { SetValue(SpaceObjectDescriptionTextProperty, value); }
        }
        public bool IsSwitchButtonEnabled
        {
            get { return (bool)GetValue(IsSwitchButtonEnabledProperty); }
            set { SetValue(IsSwitchButtonEnabledProperty, value); }
        }
        public string SpaceObjectImageSource
        {
            get { return (string)GetValue(SpaceObjectImageSourceProperty); }
            set { SetValue(SpaceObjectImageSourceProperty, value); }
        }
        public ICommand SpaceObjectSelectCommand
        {
            get { return (ICommand)GetValue(SpaceObjectSelectCommandProperty); }
            set { SetValue(SpaceObjectSelectCommandProperty, value); }
        }
        public string SpaceShipSelectionButtonContent
        {
            get { return (string)GetValue(SpaceShipSelectionButtonContentProperty); }
            set { SetValue(SpaceShipSelectionButtonContentProperty, value); }
        }
        public string SpaceShipDescriptionText
        {
            get { return (string)GetValue(SpaceShipDescriptionTextProperty); }
            set { SetValue(SpaceShipDescriptionTextProperty, value); }
        }
        public string SpaceShipImageSource
        {
            get { return (string)GetValue(SpaceShipImageSourceProperty); }
            set { SetValue(SpaceShipImageSourceProperty, value); }
        }
        public ICommand SpaceShipSelectCommand
        {
            get { return (ICommand)GetValue(SpaceShipSelectCommandProperty); }
            set { SetValue(SpaceShipSelectCommandProperty, value); }
        }
        public ICommand SwitchPresentationCommand
        {
            get { return (ICommand)GetValue(SwitchPresentationCommandProperty); }
            set { SetValue(SwitchPresentationCommandProperty, value); }
        }

        #endregion

        #region "Dependecy Injection"
        
        public static readonly DependencyProperty SpaceObjectSelectionButtonContentProperty =
            DependencyProperty.Register("SpaceObjectSelectionButtonContent", typeof(string), typeof(SelectionPresentation), new FrameworkPropertyMetadata("Выберите Корабль"));
        public static readonly DependencyProperty SpaceObjectDescriptionTextProperty =
            DependencyProperty.Register("SpaceObjectDescriptionText", typeof(string), typeof(SelectionPresentation), new FrameworkPropertyMetadata());
        public static readonly DependencyProperty SpaceObjectImageSourceProperty =
            DependencyProperty.Register("SpaceObjectImageSource", typeof(string), typeof(SelectionPresentation), new FrameworkPropertyMetadata());
        public static readonly DependencyProperty SpaceObjectSelectCommandProperty =
            DependencyProperty.Register("SpaceObjectSelectCommand", typeof(ICommand), typeof(SelectionPresentation), new UIPropertyMetadata(null));

        public static readonly DependencyProperty SpaceShipSelectionButtonContentProperty =
            DependencyProperty.Register("SpaceShipSelectionButtonContent", typeof(string), typeof(SelectionPresentation), new FrameworkPropertyMetadata("Выберите Объект"));
        public static readonly DependencyProperty SpaceShipDescriptionTextProperty =
            DependencyProperty.Register("SpaceShipDescriptionText", typeof(string), typeof(SelectionPresentation), new FrameworkPropertyMetadata());
        public static readonly DependencyProperty SpaceShipImageSourceProperty =
            DependencyProperty.Register("SpaceShipImageSource", typeof(string), typeof(SelectionPresentation), new FrameworkPropertyMetadata());
        public static readonly DependencyProperty SpaceShipSelectCommandProperty =
            DependencyProperty.Register("SpaceShipSelectCommand", typeof(ICommand), typeof(SelectionPresentation), new UIPropertyMetadata(null));

        public static readonly DependencyProperty IsSwitchButtonEnabledProperty =
            DependencyProperty.Register("IsSwitchButtonEnabled", typeof(bool), typeof(SelectionPresentation), new FrameworkPropertyMetadata());
        public static readonly DependencyProperty SwitchPresentationCommandProperty =
            DependencyProperty.Register("SwitchPresentationCommand", typeof(ICommand), typeof(SelectionPresentation), new UIPropertyMetadata(null));

        #endregion

        public SelectionPresentation()
        {
            InitializeComponent();
        }
    }
}
