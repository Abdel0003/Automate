using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Automate.Models
{
    public class Tache :ITache
    {
		private string _nom;

		public string Nom
		{
			get { return _nom; }
			set { _nom = value; }
		}

		private SolidColorBrush _legende;

		public SolidColorBrush Legende
		{
			get { return _legende; }
			set { _legende= value; }
		}

		private bool _estCompletee;

		public bool EstCompletee
		{
			get { return _estCompletee; }
			set { _estCompletee = value; }
		}


		public Tache(string nom)
        {
            this.Nom = nom;
			this.EstCompletee = false;
            ObtenirCouleurLegende();
        }
		//"Semis", "Rempotage", "Entretien", "Arrosage", "Récolte", "Commandes", "Événements spéciaux"

		public void ObtenirCouleurLegende()
		{
			if (string.IsNullOrEmpty(Nom))
				throw new ArgumentNullException("Le nom est vide");
			if (!Enum.IsDefined(typeof(EnumTache), Nom))
				throw new ArgumentException("Le Nom ne fait pas partie de l'enumération des tâches.");

			if (Nom == "Semis")
				Legende = Brushes.Blue;

			else if (Nom == "Rempotage")
				Legende = Brushes.Brown;

			else if (Nom == "Arrosage")
				Legende = Brushes.Cyan;

			else if (Nom == "Recolte")
				Legende = Brushes.Green;

			else if (Nom == "Commande")
				Legende = Brushes.Gold;
			
			else if (Nom == "Speciale")
				Legende = Brushes.OrangeRed;

			else
				Legende= Brushes.Orange;
		}
		

		/// <summary>
		/// On change le statut complété ou non de la tâche
		/// </summary>
		/// <param name="resultat">Résultat récupéré du calendrier</param>
		public void ChangerStatutComplete(bool resultat)
		{
			if (resultat)
				_estCompletee = true;
			else
				_estCompletee = false;
		}
    }
}
