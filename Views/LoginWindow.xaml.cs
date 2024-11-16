using Automate.ViewModels;
using Automate.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Automate
{
    /// <summary>
    /// Fenêtre de connexion.
    /// Gère l'authentification de l'utilisateur.
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// Constructeur de la fenêtre de connexion.
        /// Initialise le DataContext avec le ViewModel associé.
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
        }

        /// <summary>
        /// Gestionnaire de l'événement PasswordChanged pour le PasswordBox.
        /// Met à jour la propriété Password du ViewModel.
        /// </summary>
        /// <param name="sender">Élément source de l'événement.</param>
        /// <param name="e">Arguments de l'événement PasswordChanged.</param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Vérifie que l'élément source est un PasswordBox
            if (sender is not PasswordBox passwordBox)
                return;

            // Met à jour la propriété Password dans le ViewModel si valide
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = passwordBox.Password;
            }
        }
    }
}
