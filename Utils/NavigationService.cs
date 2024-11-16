using Automate.ViewModels;
using Automate.Views;
using System;
using System.Windows;

namespace Automate.Utils
{
    /// <summary>
    /// Service de navigation pour gérer l'ouverture et la fermeture des fenêtres.
    /// </summary>
    public class NavigationService
    {
        /// <summary>
        /// Ouvre une nouvelle fenêtre spécifiée.
        /// </summary>
        /// <typeparam name="T">Type de la fenêtre à ouvrir (doit hériter de Window).</typeparam>
        /// <param name="viewModel">ViewModel optionnel à associer à la fenêtre.</param>
        public void NavigateTo<T>(object viewModel = null) where T : Window, new()
        {
            // Vérifie si la fenêtre cible est AccueilWindow et si un ViewModel valide est fourni
            if (typeof(T) == typeof(AccueilWindow) && viewModel is AccueilViewModel accueilViewModel)
            {
                OpenAccueilWindow(accueilViewModel);
            }
            else
            {
                OpenGenericWindow<T>(viewModel);
            }
        }

        /// <summary>
        /// Ferme la fenêtre spécifiée.
        /// </summary>
        /// <param name="window">Instance de la fenêtre à fermer.</param>
        public void Close(Window window)
        {
            window?.Close();
        }

        /// <summary>
        /// Ouvre une instance d'AccueilWindow avec un ViewModel spécifique.
        /// </summary>
        /// <param name="viewModel">Instance de AccueilViewModel à associer à la fenêtre.</param>
        private void OpenAccueilWindow(AccueilViewModel viewModel)
        {
            var accueilWindow = new AccueilWindow(viewModel)
            {
                DataContext = viewModel // Associe le ViewModel à la fenêtre
            };
            accueilWindow.Show();
        }

        /// <summary>
        /// Ouvre une fenêtre générique avec un ViewModel optionnel.
        /// </summary>
        /// <typeparam name="T">Type de la fenêtre à ouvrir.</typeparam>
        /// <param name="viewModel">ViewModel optionnel à associer à la fenêtre.</param>
        private void OpenGenericWindow<T>(object viewModel) where T : Window, new()
        {
            var window = new T();

            if (viewModel != null)
            {
                window.DataContext = viewModel; // Associe le ViewModel si fourni
            }

            window.Show();
        }
    }
}
