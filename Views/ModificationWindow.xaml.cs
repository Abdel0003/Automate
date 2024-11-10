using Automate.Models;
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
using System.Windows.Shapes;

namespace Automate.Views
{
    /// <summary>
    /// Logique d'interaction pour ModificationWindow.xaml
    /// </summary>
    public partial class ModificationWindow : Window
    {

        private Journee _jour;

        public Journee Jour
        {
            get { return _jour; }
            set { _jour = value; }
        }

        public ModificationWindow()
        {
            InitializeComponent();
            Jour = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Jour is null)
                AjouterJournee();

            else
                ModifierJournee();

        }

        private void ModifierJournee()
        {

        }

        private void AjouterJournee()
        {

        }
    }
}
