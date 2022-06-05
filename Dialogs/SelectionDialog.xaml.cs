using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Astrostrider.Classes;
using Astrostrider.ViewModels;
using Astrostrider.Commands;

namespace Astrostrider.Dialogs
{
    public partial class SelectionDialog : Window
    {
        public SelectionDialogViewModel<SpaceObject> SpaceObjectViewModel;
        public SelectionDialogViewModel<SpaceShip> SpaceShipViewModel;
        public SelectionDialog(string type)
        {
            InitializeComponent();

            if (type.ToLower() == "spaceobject")
            {
                SpaceObjectViewModel = new SelectionDialogViewModel<SpaceObject>();
                DataContext = SpaceObjectViewModel;

                if (SpaceObjectViewModel.CloseCommand == null)
                {
                    SpaceObjectViewModel.CloseCommand = new RelayCommand(new Action<object>(p => { this.DialogResult = true; this.Close(); }));
                }
                if (SpaceObjectViewModel.CancelCommand == null)
                {
                    SpaceObjectViewModel.CancelCommand = new RelayCommand(new Action<object>(p => { this.DialogResult = false; this.Close(); }));
                }
            }
            else if (type.ToLower() == "spaceship")
            {
                SpaceShipViewModel = new SelectionDialogViewModel<SpaceShip>();
                DataContext = SpaceShipViewModel;

                if (SpaceShipViewModel.CloseCommand == null)
                {
                    SpaceShipViewModel.CloseCommand = new RelayCommand(new Action<object>(p => { this.DialogResult = true; this.Close(); }));
                }
                if (SpaceShipViewModel.CancelCommand == null)
                {
                    SpaceShipViewModel.CancelCommand = new RelayCommand(new Action<object>(p => { this.DialogResult = false; this.Close(); }));
                }
            }
            else
            {
                this.DialogResult = false;
                this.Close();
            }
        }
    }
}
