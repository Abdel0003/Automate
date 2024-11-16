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
    /// <summary>
    /// ViewModel pour la gestion des tâches dans la fenêtre de modification.
    /// </summary>
    public class ModificationViewModel : INotifyPropertyChanged
    {
        private readonly MongoDBService _mongoDBService;

        // Collection observable des tâches affichées
        public ObservableCollection<Tache> Taches { get; private set; } = new ObservableCollection<Tache>();

        // Commandes disponibles pour l'utilisateur
        public ICommand AjouterTacheCommand { get; }
        public ICommand RefreshTasksCommand { get; }

        // Champs privés
        private DateTime _selectedDate;
        private bool _isAdmin;
        private string _statusMessage;
        private string _nouveauNomTache;
        private string _selectedTaskType;
        private bool _isTaskCritical;

        // Propriétés publiques
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public string NouveauNomTache
        {
            get => _nouveauNomTache;
            set
            {
                _nouveauNomTache = value;
                OnPropertyChanged();
            }
        }

        public string SelectedTaskType
        {
            get => _selectedTaskType;
            set
            {
                _selectedTaskType = value;
                OnPropertyChanged();
            }
        }

        public bool IsTaskCritical
        {
            get => _isTaskCritical;
            set
            {
                _isTaskCritical = value;
                OnPropertyChanged();
            }
        }

        public string Day => SelectedDate.Day.ToString();
        public string Month => SelectedDate.ToString("MMMM", new CultureInfo("fr-FR"));
        public string DayOfWeek => SelectedDate.ToString("dddd", new CultureInfo("fr-FR"));

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
                    _ = LoadTasksAsync(); // Recharger les tâches pour la date sélectionnée
                }
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Constructeur du ViewModel.
        /// </summary>
        /// <param name="isAdmin">Indique si l'utilisateur est administrateur.</param>
        public ModificationViewModel(bool isAdmin)
        {
            _isAdmin = isAdmin;
            _mongoDBService = new MongoDBService("AutomateDB");

            // Initialisation des commandes
            AjouterTacheCommand = new RelayCommand(async () => await AjouterTacheAsync());
            RefreshTasksCommand = new RelayCommand(async () => await LoadTasksAsync());

            SelectedDate = DateTime.Now;
            _ = LoadTasksAsync(); // Chargement initial des tâches
        }

        /// <summary>
        /// Ajoute une nouvelle tâche à la base de données et à la collection locale.
        /// </summary>
        private async Task AjouterTacheAsync()
        {
            if (string.IsNullOrWhiteSpace(SelectedTaskType))
            {
                MessageBox.Show("Veuillez sélectionner un type de tâche.");
                return;
            }

            string tacheNom = ExtractTaskName(SelectedTaskType);

            var nouvelleTache = new Tache(tacheNom)
            {
                DateAjout = SelectedDate,
                EstCritique = IsTaskCritical
            };

            await _mongoDBService.AjouterTacheAsync(nouvelleTache);
            Taches.Add(nouvelleTache);

            ResetTaskFields();

            StatusMessage = $"{Taches.Count} tâche(s) pour cette date.";
            MessageBox.Show("Tâche ajoutée avec succès.");
        }

        /// <summary>
        /// Charge les tâches pour la date sélectionnée depuis la base de données.
        /// </summary>
        private async Task LoadTasksAsync()
        {
            Taches.Clear();
            var tasksFromDb = _mongoDBService.GetTasksByDate(SelectedDate);

            foreach (var tache in tasksFromDb)
            {
                Taches.Add(tache);
            }

            StatusMessage = Taches.Count > 0
                ? $"{Taches.Count} tâche(s) pour cette date."
                : "Aucune tâche pour cette date.";
        }

        /// <summary>
        /// Extrait le nom de la tâche à partir de la valeur brute sélectionnée.
        /// </summary>
        /// <param name="rawSelectedTaskType">Valeur brute sélectionnée.</param>
        /// <returns>Nom de la tâche extrait.</returns>
        private string ExtractTaskName(string rawSelectedTaskType)
        {
            return rawSelectedTaskType.Contains(":")
                ? rawSelectedTaskType.Split(':')[1].Trim()
                : rawSelectedTaskType;
        }

        /// <summary>
        /// Réinitialise les champs liés à l'ajout de tâches.
        /// </summary>
        private void ResetTaskFields()
        {
            SelectedTaskType = null;
            IsTaskCritical = false;
            NouveauNomTache = string.Empty;
        }

        /// <summary>
        /// Notifie les changements de propriété pour l'interface utilisateur.
        /// </summary>
        /// <param name="propertyName">Nom de la propriété modifiée.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
