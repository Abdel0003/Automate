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

        public DateTime DateAjout { get; set; }

        // Dictionnaire des tâches avec variantes de nom
        private static readonly Dictionary<string, EnumTache> TacheMapping = new()
        {
            { "Semis", EnumTache.Semis },
            { "Rempotage", EnumTache.Rempotage },
            { "Entretien", EnumTache.Entretien },
            { "Désherbage", EnumTache.Entretien },
            { "Taille", EnumTache.Entretien },
            { "Fertilisation", EnumTache.Entretien },
            { "Arrosage", EnumTache.Arrosage },
            { "Recolte", EnumTache.Recolte },
            { "Récolte", EnumTache.Recolte },
            { "Commande", EnumTache.Commande },
            { "Commandes", EnumTache.Commande },
            { "Événements spéciaux", EnumTache.Speciale },
            { "Visites", EnumTache.Speciale },
            { "Formations", EnumTache.Speciale }
        };


        public Tache(string nom)
        {
            this.Nom = nom;
			this.EstCompletee = false;
            DateAjout = DateTime.Now;
            ObtenirCouleurLegende();
        }
		//"Semis", "Rempotage", "Entretien", "Arrosage", "Récolte", "Commandes", "Événements spéciaux"

		public void ObtenirCouleurLegende()
		{
			if (string.IsNullOrEmpty(Nom))
				throw new ArgumentNullException("Le nom est vide");

            // Vérifie si le nom existe dans le dictionnaire de correspondances
            if (!TacheMapping.TryGetValue(Nom, out var tacheEnum))
                throw new ArgumentException("Le Nom ne fait pas partie de l'énumération des tâches.");
            // Associe la couleur en fonction de l'énumération
            Legende = tacheEnum switch
            {
                EnumTache.Semis => Brushes.Blue,
                EnumTache.Rempotage => Brushes.Brown,
                EnumTache.Entretien => Brushes.LightGreen,
                EnumTache.Arrosage => Brushes.Cyan,
                EnumTache.Recolte => Brushes.Green,
                EnumTache.Commande => Brushes.Gold,
                EnumTache.Speciale => Brushes.OrangeRed,
                _ => Brushes.Orange
            };
        }


        /// <summary>
        /// On change le statut complété ou non de la tâche
        /// </summary>
        /// <param name="resultat">Résultat récupéré du calendrier</param>
        public void ChangerStatutComplete(bool resultat)
        {
            EstCompletee = resultat;
        }
    }
}
