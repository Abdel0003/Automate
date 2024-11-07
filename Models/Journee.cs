using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Models
{
    public class Journee
    {


		private DateTime _date;

		public DateTime Date
		{
			get { return _date; }
			set { _date = value;}
        }
		private DayOfWeek _jour;

		public DayOfWeek Jour
		{
			get { return _jour; }
			set { ObtenirJourSemaine(); }
		}
		private int _numeroJour;

		public int NumeroJour
		{
			get { return _numeroJour; }
			set { ObtenirNumeroJour(); }
		}
		private int _numeroMois;

		public int NumeroMois
		{
			get { return _numeroMois; }
			set { ObtenirNumeroMois(); }
		}

		private int _annee;

		public int Annee
		{
			get { return _annee; }
			set { ObtenirAnnee(); }
		}

		private List<string> _taches;

		public List<string> Taches
		{
			get { return _taches; }
			set { _taches = value; }
		}


		public Journee(DateTime date)
        {
            Date = date;
			Taches = new List<string>();
        }

        private void ObtenirNumeroJour()
		{
			NumeroJour = Date.Day;
        }

		private void ObtenirJourSemaine()
		{
			Jour = Date.DayOfWeek;
		}

		private void ObtenirNumeroMois()
		{
			NumeroMois = Date.Month;
		}

		private void ObtenirAnnee()
		{
			Annee = Date.Year;
		}
	}
}
