using Automate.ViewModels;
using System.Windows;

namespace Automate.Views
{
    /// <summary>
    /// Fenêtre d'accueil.
    /// Permet à l'utilisateur de consulter et d'interagir avec le calendrier.
    /// </summary>
    public partial class AccueilWindow : Window
    {
        /// <summary>
        /// Constructeur par défaut de la fenêtre d'accueil.
        /// Initialise les composants de la fenêtre.
        /// </summary>
        public AccueilWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructeur de la fenêtre d'accueil avec un ViewModel.
        /// Initialise le DataContext avec le ViewModel fourni.
        /// </summary>
        /// <param name="viewModel">ViewModel du calendrier.</param>
        public AccueilWindow(AccueilViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }

        /// <summary>
        /// Gestionnaire de l'événement déclenché lors du chargement de la fenêtre.
        /// Peut être utilisé pour des initialisations supplémentaires si nécessaire.
        /// </summary>
        /// <param name="sender">Élément source de l'événement.</param>
        /// <param name="e">Arguments de l'événement Loaded.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Logique supplémentaire à exécuter lors du chargement de la fenêtre.
        }
    }
}
