using MongoDB.Driver;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Automate.Models;
using Automate.Utils;

namespace Automate.ViewModels
{
    public class ModificationViewModel : INotifyPropertyChanged
    {
        private readonly MongoDBService _mongoDBService;
        public ObservableCollection<Tache> Taches { get; set; } = new ObservableCollection<Tache>();

        public ICommand AjouterTacheCommand { get; }

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

        // Champs pour les nouvelles tâches
        private string _nouveauNomTache;
        public string NouveauNomTache
        {
            get => _nouveauNomTache;
            set
            {
                _nouveauNomTache = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ModificationViewModel(bool isAdmin)
        {
            _isAdmin = isAdmin;
            _mongoDBService = new MongoDBService("AutomateDB");
            AjouterTacheCommand = new RelayCommand(async () => await AjouterTacheAsync());

            SelectedDate = DateTime.Now; // Initialise avec la date actuelle
            LoadTasks(); // Charger les tâches pour la date actuelle
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
                    LoadTasks(); // Recharger les tâches pour la nouvelle date
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

        private async Task AjouterTacheAsync()
        {
            if (string.IsNullOrWhiteSpace(NouveauNomTache))
            {
                MessageBox.Show("Veuillez entrer un nom de tâche.");
                return;
            }

            // Créez et insérez la tâche avec la date sélectionnée
            var nouvelleTache = new Tache(NouveauNomTache)
            {
                DateAjout = SelectedDate // Utiliser la date sélectionnée
            };

            await _mongoDBService.AjouterTacheAsync(nouvelleTache);

            // Ajoutez la tâche à la collection Observable pour mettre à jour l'affichage
            Taches.Add(nouvelleTache);
            NouveauNomTache = string.Empty; // Réinitialise le champ de texte
            StatusMessage = $"{Taches.Count} tâches pour cette date"; // Mettre à jour le message de statut

            MessageBox.Show("La tâche a été ajoutée avec succès.");
        }

        private void LoadTasks()
        {
            Taches.Clear();

            // Récupérer les tâches depuis MongoDB pour la date sélectionnée
            var tasksFromDb = _mongoDBService.GetTasksByDate(SelectedDate);

            if (tasksFromDb.Count > 0)
            {
                foreach (var tache in tasksFromDb)
                {
                    Taches.Add(tache);
                }
                StatusMessage = $"{Taches.Count} tâches pour cette date";
            }
            else
            {
                StatusMessage = "Aucune tâche pour cette date";
            }

            OnPropertyChanged(nameof(Taches));
        }
    }
}
