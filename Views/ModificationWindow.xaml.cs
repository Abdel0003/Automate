using Automate.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Automate.Views
{
    public partial class ModificationWindow : Window
    {
        public ModificationWindow(bool isAdmin)
        {
            InitializeComponent();
            DataContext = new ModificationViewModel(isAdmin); // Utilise le ViewModel
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Logique à exécuter lors du chargement de la fenêtre (si nécessaire)
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ModificationViewModel viewModel)
            {
                // Vérifie que la date sélectionnée est valide
                if (e.AddedItems.Count > 0 && e.AddedItems[0] is DateTime selectedDate)
                {
                    viewModel.SelectedDate = selectedDate;
                }
            }
        }

        //private void txtNote_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtNote.Text) && txtNote.Text.Length > 0)
        //    {
        //        lblNote.Visibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        lblNote.Visibility = Visibility.Visible;
        //    }
        //}

        //private void txtTime_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtTime.Text) && txtTime.Text.Length > 0)
        //    {
        //        lblTime.Visibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        lblTime.Visibility = Visibility.Visible;
        //    }
        //}

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        //private void lblNote_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    txtNote.Focus();
        //}

        //private void lblTime_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    txtTime.Focus();
        //}
    }
}
