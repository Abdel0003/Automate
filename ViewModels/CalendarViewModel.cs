using Automate.Models;
using Automate.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Automate.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        private Journee _currentDate;
        
        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }
        public string CurrentMonthYear => $"{new DateTime(1, _currentDate.NumeroMois, 1).ToString("MMMM")} {_currentDate.Annee}";

        // Collection pour les jours du mois
        public ObservableCollection<CalendarDay> CalendarDays { get; set; }

        // Collection pour les éléments de légende
        public ObservableCollection<string> LegendItems { get; set; }

        // Commandes pour naviguer entre les mois
        public ICommand PreviousMonthCommand { get; }
        public ICommand NextMonthCommand { get; }
        public ICommand ViewCalendarCommand { get; }


        /// <summary>
        /// Attribut du jour sélectionné
        /// </summary>
        private Journee _jourSelect;
        public Journee JourSelect
        {
            get => _jourSelect;
            set
            {
                _jourSelect = value;
                OnPropertyChanged(nameof(JourSelect));
            }
        }

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Journee> Journees { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CalendarViewModel(string username, bool isAdmin)
        {
            Username = username;
            IsAdmin = isAdmin;

            // Initialiser la date actuelle
            _currentDate = new Journee(DateTime.Now);

            //Codage en dure pour les tests
            Journees = new ObservableCollection<Journee>{
                new Journee(new DateTime(2024, 11, 2)),
                new Journee(new DateTime(2024, 11, 6)),
                new Journee(new DateTime(2024, 11, 8)),
                new Journee(new DateTime(2024, 11, 9)),
                new Journee(new DateTime(2024, 11, 13)),
                new Journee(new DateTime(2024, 11, 14)),
            };
            Journees[0].Taches.Add(new Tache("Semis"));
            Journees[0].Taches.Add(new Tache("Rempotage"));
            Journees[1].Taches.Add(new Tache("Arrosage"));
            Journees[1].Taches.Add(new Tache("Semis"));
            Journees[2].Taches.Add(new Tache("Commandes"));
            Journees[2].Taches.Add(new Tache("Arrosage"));
            Journees[3].Taches.Add(new Tache("Semis"));
            Journees[3].Taches.Add(new Tache("Rempotage"));
            Journees[4].Taches.Add(new Tache("Arrosage"));

            // Initialiser les collections
            CalendarDays = new ObservableCollection<CalendarDay>();
            LegendItems = new ObservableCollection<string>
            {
                "Semis", "Rempotage", "Entretien", "Arrosage", "Récolte", "Commandes", "Événements spéciaux"
            };

            // Initialiser les commandes de navigation
            ViewCalendarCommand = new RelayCommand(OpenModificationWindow);
            PreviousMonthCommand = new RelayCommand(GoToPreviousMonth);
            NextMonthCommand = new RelayCommand(GoToNextMonth);

            // Charger les jours du mois actuel
            LoadCalendarDays();
        }

        // Méthode pour aller au mois précédent
        private void GoToPreviousMonth()
        {
            _currentDate.Date = _currentDate.Date.AddMonths(-1);
            OnPropertyChanged(nameof(CurrentMonthYear));
            LoadCalendarDays();
        }

        // Méthode pour aller au mois suivant
        private void GoToNextMonth()
        {
            _currentDate.Date = _currentDate.Date.AddMonths(1);
            OnPropertyChanged(nameof(CurrentMonthYear));
            LoadCalendarDays();
        }

        private void OpenModificationWindow()
        {
            var modificationWindow = new ModificationWindow(IsAdmin);
            modificationWindow.Show();
        }

        // Méthode pour charger les jours du mois actuel
        private void LoadCalendarDays()
        {
            CalendarDays.Clear();

            // Obtenez le nombre de jours dans le mois actuel
            var daysInMonth = DateTime.DaysInMonth(_currentDate.Annee, _currentDate.NumeroMois);

            // Remplissez chaque jour avec des tâches exemple
            for (int day = 1; day <= daysInMonth; day++)
            {
                ObservableCollection<Tache> OCTache = new ObservableCollection<Tache>();

                if (Journees is not null)
                {
                    Journee journee = Journees[0];
                    foreach (Journee j in Journees)
                    {
                        bool estValide = false;
                        if (j.Date.Day == day)
                        {
                            journee = j;
                            estValide = true;
                        }

                        if (estValide)
                            foreach (Tache t in journee.Taches)
                                OCTache.Add(t);
                    }

                }

                CalendarDays.Add(new CalendarDay
                {
                    Day = day,
                    Tasks = OCTache
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
        public ObservableCollection<Tache> Tasks { get; set; } // Collection de tâches pour le jour

    }

}
