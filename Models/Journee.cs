using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Models
{
    public class Journee : IJournee
    {
		const int MAX_MOIS = 12;
		const int MIN_MOIS = 1;
		const int MIN_JOUR = 1;
		const int MAX_JOUR = 31;
		const string DATE_FONDATION = "2018-06-01";
		//Date, attributs central pour l'objet
		private DateTime _date;

		public DateTime Date
		{
			get { return _date; }
			set {
				if (value < DateTime.Parse(DATE_FONDATION))
					throw new ArgumentOutOfRangeException($"Une date ne peut pas être ultérieure à {DATE_FONDATION}");

				_date = value;}
        }

		//Nom de la journee
		private DayOfWeek _jour;

		public DayOfWeek Jour
		{
			get { return _jour; }
			set { _jour = value; }
		}

		//Numéro de la journée
		private int _numeroJour;

		public int NumeroJour
		{
			get { return _numeroJour; }
			set {
				if (value > MAX_JOUR || value < MIN_JOUR)
					throw new ArgumentOutOfRangeException("le numéro de jour doit se retrouver entre 1 et 31");

				_numeroJour = value; }
		}

		//Numéro du mois de la Journée
		private int _numeroMois;

		public int NumeroMois
		{
			get { return _numeroMois; }
			set {
				if (value < MIN_MOIS || value > MAX_MOIS)
					throw new ArgumentOutOfRangeException("Le numéro de mois dois se trouver entre 1 et 12");
				
				_numeroMois = value; }
		}

		//Année de la journée
		private int _annee;

		public int Annee
		{
			get { return _annee; }
			set {
				if (value == 0)
					throw new ArgumentNullException("L'année ne peut pas être null");
                
				_annee = value;
			; }
		}

		//Liste des tâches de la journée
		private List<Tache> _taches;

		public List<Tache> Taches
		{
			get { return _taches; }
			set { 
				if(value is null)
					throw new ArgumentNullException("La liste ne peut pas recevoir de valeurs null.");

				_taches = value; }
		}

        DateTime IJournee.Journee { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        //On BESOIN de la date, sinon on initialise une nouvelle liste de tâches
        public Journee(DateTime date)
        {
            Date = date;
			ObtenirAnnee();
			ObtenirJourSemaine();
			ObtenirNumeroJour();
			ObtenirNumeroMois();
			Taches = new List<Tache>();
        }

		//Obtention du jour
        private void ObtenirNumeroJour()
		{
			NumeroJour = Date.Day;
        }

		//Obtention du nom du jour
		private void ObtenirJourSemaine()
		{
			Jour = Date.DayOfWeek;
		}

		//Obtention du mois
		private void ObtenirNumeroMois()
		{
			NumeroMois = Date.Month;
		}

		//Obtention de l'année
		private void ObtenirAnnee()
		{
			Annee = Date.Year;
		}
	}
}
