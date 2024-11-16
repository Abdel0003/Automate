using Automate.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Automate.Views
{
    public partial class ModificationWindow : Window
    {
        /// <summary>
        /// Constructeur de la fenêtre de modification.
        /// Initialise le DataContext avec le ViewModel.
        /// </summary>
        /// <param name="isAdmin">Indique si l'utilisateur est administrateur.</param>
        public ModificationWindow(bool isAdmin)
        {
            InitializeComponent();
            DataContext = new ModificationViewModel(isAdmin);
        }

        /// <summary>
        /// Méthode appelée lorsque la fenêtre est chargée.
        /// Peut contenir une logique d'initialisation supplémentaire.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Logique d'initialisation à ajouter si nécessaire.
        }

        /// <summary>
        /// Gestionnaire d'événement pour la sélection de dates dans le calendrier.
        /// Met à jour la date sélectionnée dans le ViewModel.
        /// </summary>
        /// <param name="sender">Élément source de l'événement.</param>
        /// <param name="e">Arguments de l'événement contenant la nouvelle date sélectionnée.</param>
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Vérifie que le DataContext est bien un ModificationViewModel
            if (DataContext is not ModificationViewModel viewModel)
                return;

            // Met à jour la date sélectionnée si elle est valide
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is DateTime selectedDate)
            {
                viewModel.SelectedDate = selectedDate;
            }
        }

        /// <summary>
        /// Permet de déplacer la fenêtre en maintenant le clic gauche sur la bordure.
        /// </summary>
        /// <param name="sender">Élément source de l'événement.</param>
        /// <param name="e">Arguments de l'événement indiquant le bouton de la souris.</param>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
