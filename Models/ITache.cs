using System;
using System.Windows.Media;

namespace Automate.Models
{
    public interface ITache
    {
        /// <summary>
        /// Identifiant unique de la tâche (compatible avec MongoDB).
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Nom ou description de la tâche.
        /// </summary>
        string Nom { get; set; }

        /// <summary>
        /// Indique si la tâche est complétée.
        /// </summary>
        bool EstCompletee { get; set; }

        /// <summary>
        /// Indique si la tâche est complétée.
        /// </summary>
        bool EstCritique { get; set; }

        /// <summary>
        /// Date d'ajout de la tâche.
        /// </summary>
        DateTime DateAjout { get; set; }

        /// <summary>
        /// Couleur associée à la tâche (non stockée dans MongoDB).
        /// </summary>
        SolidColorBrush Legende { get; }

        /// <summary>
        /// Définit la couleur légende en fonction du nom de la tâche.
        /// </summary>
        void ObtenirCouleurLegende();

        /// <summary>
        /// Change l'état de complétion de la tâche.
        /// </summary>
        /// <param name="resultat">Nouveau statut (complété ou non).</param>
        void ChangerStatutComplete(bool resultat);
    }
}
