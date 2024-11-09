using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Automate.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        private DateTime _currentDate;
        public string CurrentMonthYear => _currentDate.ToString("MMMM yyyy");

        // Collection pour les jours du mois
        public ObservableCollection<CalendarDay> CalendarDays { get; set; }

        // Collection pour les éléments de légende
        public ObservableCollection<string> LegendItems { get; set; }

        // Commandes pour naviguer entre les mois
        public ICommand PreviousMonthCommand { get; }
        public ICommand NextMonthCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CalendarViewModel()
        {
            // Initialiser la date actuelle
            _currentDate = DateTime.Now;

            // Initialiser les collections
            CalendarDays = new ObservableCollection<CalendarDay>();
            LegendItems = new ObservableCollection<string>
            {
                "Semis", "Rempotage", "Entretien", "Arrosage", "Récolte", "Commandes", "Événements spéciaux"
            };

            // Initialiser les commandes de navigation
            PreviousMonthCommand = new RelayCommand(GoToPreviousMonth);
            NextMonthCommand = new RelayCommand(GoToNextMonth);

            // Charger les jours du mois actuel
            LoadCalendarDays();
        }

        // Méthode pour aller au mois précédent
        private void GoToPreviousMonth()
        {
            _currentDate = _currentDate.AddMonths(-1);
            OnPropertyChanged(nameof(CurrentMonthYear));
            LoadCalendarDays();
        }

        // Méthode pour aller au mois suivant
        private void GoToNextMonth()
        {
            _currentDate = _currentDate.AddMonths(1);
            OnPropertyChanged(nameof(CurrentMonthYear));
            LoadCalendarDays();
        }

        // Méthode pour charger les jours du mois actuel
        private void LoadCalendarDays()
        {
            CalendarDays.Clear();

            // Obtenez le nombre de jours dans le mois actuel
            var daysInMonth = DateTime.DaysInMonth(_currentDate.Year, _currentDate.Month);

            // Remplissez chaque jour avec des tâches exemple
            for (int day = 1; day <= daysInMonth; day++)
            {
                CalendarDays.Add(new CalendarDay
                {
                    Day = day,
                    Tasks = new ObservableCollection<string> { "Tâche 1", "Tâche 2" } // Remplacez par des tâches réelles si disponible
                });
            }
        }

        // Notifier la vue des changements de propriété
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Classe pour représenter un jour dans le calendrier
    public class CalendarDay
    {
        public int Day { get; set; } // Jour du mois
        public ObservableCollection<string> Tasks { get; set; } // Collection de tâches pour le jour
    }
}
