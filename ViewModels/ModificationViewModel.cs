using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Automate.Models;

namespace Automate.ViewModels
{
    public class ModificationViewModel : INotifyPropertyChanged
    {
        private DateTime _selectedDate;
        private bool _isAdmin;


        public event PropertyChangedEventHandler PropertyChanged;

        public ModificationViewModel(bool isAdmin)
        {
            _isAdmin = isAdmin;
            SelectedDate = DateTime.Now; // Initialise avec la date actuelle
        }

        // Propriétés pour le jour, le mois, et le jour de la semaine
        public string Day => SelectedDate.Day.ToString();
        public string Month => SelectedDate.ToString("MMMM", new CultureInfo("fr-FR"));
        public string DayOfWeek => SelectedDate.ToString("dddd", new CultureInfo("fr-FR"));

        // Propriété de la date sélectionnée
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                    OnPropertyChanged(nameof(Day));
                    OnPropertyChanged(nameof(Month));
                    OnPropertyChanged(nameof(DayOfWeek));
                }
            }
        }

        // Propriété indiquant si l'utilisateur est administrateur
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                if (_isAdmin != value)
                {
                    _isAdmin = value;
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
