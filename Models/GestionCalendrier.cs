using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Models
{
    public class GestionCalendrier
    {
		private List<Journee> _journees;

		public List<Journee> Journees
		{
			get { return _journees; }
			set { _journees = value; }
		}

		private List<int> _annee;

		public List<int> Annee
		{
			get { return _annee; }
			set { _annee = value; }
		}

		

	}
}
