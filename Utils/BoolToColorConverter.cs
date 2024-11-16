using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Automate.Utils
{
    /// <summary>
    /// Convertisseur qui change une valeur booléenne en une couleur spécifique.
    /// </summary>
    public class BoolToColorConverter : IValueConverter
    {
        private static readonly SolidColorBrush RedBrush =
            new BrushConverter().ConvertFrom("#c50500") as SolidColorBrush ?? Brushes.Red;

        /// <summary>
        /// Convertit un booléen en une couleur.
        /// </summary>
        /// <param name="value">Valeur booléenne à convertir.</param>
        /// <param name="targetType">Type cible de la conversion (non utilisé).</param>
        /// <param name="parameter">Paramètre supplémentaire (non utilisé).</param>
        /// <param name="culture">Culture utilisée pour la conversion (non utilisé).</param>
        /// <returns>Une couleur rouge si true, transparent sinon.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? RedBrush : Brushes.Transparent;
            }

            // Retourne une couleur par défaut si la valeur n'est pas un booléen
            return Brushes.Transparent;
        }

        /// <summary>
        /// ConvertBack n'est pas implémenté car cette conversion n'est pas utilisée.
        /// </summary>
        /// <param name="value">Valeur à convertir en booléen (non utilisé).</param>
        /// <param name="targetType">Type cible de la conversion (non utilisé).</param>
        /// <param name="parameter">Paramètre supplémentaire (non utilisé).</param>
        /// <param name="culture">Culture utilisée pour la conversion (non utilisé).</param>
        /// <returns>Exception NotImplementedException toujours levée.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("La conversion inverse n'est pas prise en charge.");
        }
    }
}
