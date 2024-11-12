using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Models
{
    public interface IJournee
    {
        DateTime Journee {  get; set; }
        DayOfWeek Jour { get; set; }

        int NumeroJour {  get; set; }

        int NumeroMois { get; set; }

        int Annee { get; set; }
    }
}
