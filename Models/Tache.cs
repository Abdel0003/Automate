using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Windows.Media;

namespace Automate.Models
{
    public class Tache : ITache
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("Nom")]
        public string Nom { get; set; }

        [BsonIgnore]
        public SolidColorBrush Legende { get; private set; }

        [BsonElement("EstCompletee")]
        public bool EstCompletee { get; set; }

        [BsonElement("EstCritique")]
        public bool EstCritique { get; set; }

        [BsonElement("DateAjout")]
        public DateTime DateAjout { get; set; }

        public Tache(string nom)
        {
            if (string.IsNullOrEmpty(nom))
                throw new ArgumentNullException(nameof(nom), "Le nom de la tâche ne peut pas être vide.");

            Nom = nom;
            EstCompletee = false;
            DateAjout = DateTime.Now;
            ObtenirCouleurLegende();
        }

        /// <summary>
        /// Assigne une couleur légende en fonction du nom de la tâche.
        /// </summary>
        public void ObtenirCouleurLegende()
        {
            Legende = Nom.ToLower() switch
            {
                "semis" => Brushes.Blue,
                "rempotage" => Brushes.Brown,
                "entretien" => Brushes.LightGreen,
                "arrosage" => Brushes.Cyan,
                "récolte" or "recolte" => Brushes.Green,
                "commande" or "commandes" => Brushes.Gold,
                "événements spéciaux" or "événements" or "speciale" => Brushes.OrangeRed,
                _ => Brushes.Gray,
            };
        }

        /// <summary>
        /// Change le statut complété de la tâche.
        /// </summary>
        /// <param name="resultat">Nouveau statut (complété ou non).</param>
        public void ChangerStatutComplete(bool resultat)
        {
            EstCompletee = resultat;
        }
    }
}
