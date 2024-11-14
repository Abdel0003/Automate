using Automate.ViewModels;
using Automate.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Automate.Utils
{
    public class NavigationService
    {
        // Méthode pour ouvrir une nouvelle vue
        public void NavigateTo<T>(object viewModel = null) where T : Window, new()
        {
            if (typeof(T) == typeof(AccueilWindow) && viewModel is CalendarViewModel)
            {
                // Instanciez AccueilWindow avec le ViewModel passé
                var accueilWindow = new AccueilWindow((CalendarViewModel)viewModel);
                accueilWindow.Show();
            }
            else
            {
                // Si pas de ViewModel ou type différent, utilisez la construction par défaut
                var window = new T();
                if (viewModel != null)
                {
                    window.DataContext = viewModel;
                }
                window.Show();
            }
        }

        // Méthode pour fermer la vue actuelle
        public void Close(Window window)
        {
            window.Close();
        }
    }

}
