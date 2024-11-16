using Automate.Models;
using Automate.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Automate.ViewModels
{
    /// <summary>
    /// ViewModel pour la gestion de l'accueil et du calendrier.
    /// </summary>
    public class AccueilViewModel : INotifyPropertyChanged
    {
        // Attributs privés
        private Journee _currentDate;
        private bool _isAdmin;
        private string _username;
        private Journee _jourSelect;

        // Propriétés publiques
        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public Journee JourSelect
        {
            get => _jourSelect;
            set
            {
                _jourSelect = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Formatage du mois et de l'année courants.
        /// </summary>
        public string CurrentMonthYear => $"{new DateTime(1, _currentDate.NumeroMois, 1):MMMM} {_currentDate.Annee}";

        public ObservableCollection<CalendarDay> CalendarDays { get; set; } // Jours du calendrier
        public ObservableCollection<string> LegendItems { get; set; } // Éléments de légende
        public ObservableCollection<Journee> Journees { get; set; } // Liste des journées

        // Commandes
        public ICommand PreviousMonthCommand { get; }
        public ICommand NextMonthCommand { get; }
        public ICommand ViewCalendarCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructeur du ViewModel.
        /// </summary>
        public AccueilViewModel(string username, bool isAdmin)
        {
            Username = username;
            IsAdmin = isAdmin;

            _currentDate = new Journee(DateTime.Now);

            // Initialisation des collections
            CalendarDays = new ObservableCollection<CalendarDay>();
            LegendItems = new ObservableCollection<string>
            {
                "Semis", "Rempotage", "Entretien", "Arrosage", "Récolte", "Commandes", "Événements spéciaux"
            };
            Journees = InitializeSampleData();

            // Initialisation des commandes
            ViewCalendarCommand = new RelayCommand(OpenModificationWindow);
            PreviousMonthCommand = new RelayCommand(GoToPreviousMonth);
            NextMonthCommand = new RelayCommand(GoToNextMonth);

            // Charger les jours du mois actuel
            LoadCalendarDays();
        }

        /// <summary>
        /// Méthode pour passer au mois précédent.
        /// </summary>
        private void GoToPreviousMonth()
        {
            _currentDate.Date = _currentDate.Date.AddMonths(-1);
            OnPropertyChanged(nameof(CurrentMonthYear));
            LoadCalendarDays();
        }

        /// <summary>
        /// Méthode pour passer au mois suivant.
        /// </summary>
        private void GoToNextMonth()
        {
            _currentDate.Date = _currentDate.Date.AddMonths(1);
            OnPropertyChanged(nameof(CurrentMonthYear));
            LoadCalendarDays();
        }

        /// <summary>
        /// Ouvrir la fenêtre de modification.
        /// </summary>
        private void OpenModificationWindow()
        {
            var modificationWindow = new ModificationWindow(IsAdmin);
            modificationWindow.Show();
        }

        /// <summary>
        /// Charger les jours du mois actuel avec les tâches associées.
        /// </summary>
        private void LoadCalendarDays()
        {
            CalendarDays.Clear();

            var daysInMonth = DateTime.DaysInMonth(_currentDate.Annee, _currentDate.NumeroMois);

            for (int day = 1; day <= daysInMonth; day++)
            {
                var tasks = GetTasksForDay(day);
                CalendarDays.Add(new CalendarDay
                {
                    Day = day,
                    Tasks = tasks
                });
            }
        }

        /// <summary>
        /// Récupérer les tâches pour un jour donné.
        /// </summary>
        private ObservableCollection<Tache> GetTasksForDay(int day)
        {
            var tasks = new ObservableCollection<Tache>();
            foreach (var journee in Journees)
            {
                if (journee.Date.Day == day)
                {
                    foreach (var tache in journee.Taches)
                    {
                        tasks.Add(tache);
                    }
                }
            }
            return tasks;
        }

        /// <summary>
        /// Initialisation de données exemple pour les tests.
        /// </summary>
        private ObservableCollection<Journee> InitializeSampleData()
        {
            var sampleDays = new ObservableCollection<Journee>
            {
                new Journee(new DateTime(2024, 11, 2)),
                new Journee(new DateTime(2024, 11, 6)),
                new Journee(new DateTime(2024, 11, 8)),
                new Journee(new DateTime(2024, 11, 9)),
                new Journee(new DateTime(2024, 11, 13)),
                new Journee(new DateTime(2024, 11, 14)),
            };

            sampleDays[0].Taches.Add(new Tache("Semis"));
            sampleDays[0].Taches.Add(new Tache("Rempotage"));
            sampleDays[1].Taches.Add(new Tache("Arrosage"));
            sampleDays[1].Taches.Add(new Tache("Semis"));
            sampleDays[2].Taches.Add(new Tache("Commandes"));
            sampleDays[2].Taches.Add(new Tache("Arrosage"));
            sampleDays[3].Taches.Add(new Tache("Semis"));
            sampleDays[3].Taches.Add(new Tache("Rempotage"));
            sampleDays[4].Taches.Add(new Tache("Arrosage"));

            return sampleDays;
        }

        /// <summary>
        /// Notifier la vue des changements de propriété.
        /// </summary>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Classe pour représenter un jour dans le calendrier.
    /// </summary>
    public class CalendarDay
    {
        public int Day { get; set; } // Jour du mois
        public ObservableCollection<Tache> Tasks { get; set; } // Tâches associées
    }
}
