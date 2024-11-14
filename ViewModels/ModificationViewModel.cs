using System;
using System.Collections.ObjectModel;
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
        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Tache> Taches { get; set; } = new ObservableCollection<Tache>();
        public event PropertyChangedEventHandler PropertyChanged;

        public ModificationViewModel(bool isAdmin)
        {
            _isAdmin = isAdmin;
            SelectedDate = DateTime.Now; // Initialise avec la date actuelle
            LoadTasks();
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

        private void LoadTasks()
        {
            Taches.Clear();

            // Simulation de récupération des tâches depuis la base de données pour la date sélectionnée
            var tasksFromDb = GetTasksFromDatabase(SelectedDate);

            if (tasksFromDb.Count > 0)
            {
                foreach (var tache in tasksFromDb)
                {
                    Taches.Add(tache);
                }
                StatusMessage = $"{Taches.Count} tâches pour aujourd'hui";
            }
            else
            {
                StatusMessage = "Aucune tâche pour aujourd'hui";
            }

            OnPropertyChanged(nameof(Taches));
        }

        private ObservableCollection<Tache> GetTasksFromDatabase(DateTime date)
        {
            // Simuler une récupération de tâches en fonction de la date
            // Remplacez cette section par un appel réel à la base de données
            if (date.Day == DateTime.Now.Day) // Supposons qu'il y ait des tâches pour la date d'aujourd'hui
            {
                return new ObservableCollection<Tache>
                {
                    new Tache("Semis"),
                    new Tache("Arrosage"),
                    new Tache("Récolte")
                };
            }
            return new ObservableCollection<Tache>(); // Retourne une liste vide pour les autres jours
        }
    }
}
