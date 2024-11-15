using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Automate.Models
{
    public interface ITache
    {
        string Nom { get; set; }
        SolidColorBrush Legende { get; set; }
        bool EstCompletee { get; set; }
        DateTime DateAjout { get; set; }

        void ObtenirCouleurLegende();
        void ChangerStatutComplete(bool resultat);
    }
}
