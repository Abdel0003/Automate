using Automate.Utils;
using Automate.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Automate.ViewModels
{
    /// <summary>
    /// ViewModel pour la gestion de l'authentification.
    /// </summary>
    public class LoginViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        // Services utilisés pour l'authentification et la navigation
        private readonly MongoDBService _mongoService;
        private readonly NavigationService _navigationService;

        // Référence à la fenêtre associée
        private readonly Window _window;

        // Propriétés privées
        private string? _username;
        private string? _password;
        private readonly Dictionary<string, List<string>> _errors = new();

        // Commandes
        public ICommand AuthenticateCommand { get; }

        // Événements
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        // Constructeur
        public LoginViewModel(Window openedWindow)
        {
            _mongoService = new MongoDBService("AutomateDB");
            _navigationService = new NavigationService();
            _window = openedWindow;

            AuthenticateCommand = new RelayCommand(Authenticate);
        }

        // Propriétés publiques
        public string? Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                ValidateProperty(nameof(Username));
            }
        }

        public string? Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                ValidateProperty(nameof(Password));
            }
        }

        public string ErrorMessages
        {
            get
            {
                // Concatène toutes les erreurs en une seule chaîne avec des sauts de ligne
                return string.Join("\n", _errors.Values.SelectMany(e => e).Where(e => !string.IsNullOrWhiteSpace(e)));
            }
        }

        public bool HasErrors => _errors.Count > 0;
        public bool HasPasswordErrors => _errors.ContainsKey(nameof(Password)) && _errors[nameof(Password)].Any();

        // Méthodes de validation
        private void ValidateProperty(string? propertyName)
        {
            switch (propertyName)
            {
                case nameof(Username):
                    if (string.IsNullOrEmpty(Username))
                        AddError(nameof(Username), "Le nom d'utilisateur ne peut pas être vide.");
                    else
                        RemoveError(nameof(Username));
                    break;

                case nameof(Password):
                    if (string.IsNullOrEmpty(Password))
                        AddError(nameof(Password), "Le mot de passe ne peut pas être vide.");
                    else
                        RemoveError(nameof(Password));
                    break;
            }
        }

        private void AddError(string propertyName, string errorMessage)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors[propertyName] = new List<string>();

            if (!_errors[propertyName].Contains(errorMessage))
            {
                _errors[propertyName].Add(errorMessage);
                NotifyErrorChanged(propertyName);
            }
        }

        private void RemoveError(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                NotifyErrorChanged(propertyName);
            }
        }

        private void NotifyErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(ErrorMessages));
            OnPropertyChanged(nameof(HasPasswordErrors));
        }

        public IEnumerable GetErrors(string? propertyName)
        {
            return string.IsNullOrEmpty(propertyName) || !_errors.ContainsKey(propertyName)
                ? Enumerable.Empty<string>()
                : _errors[propertyName];
        }

        // Méthodes principales
        public void Authenticate()
        {
            ValidateProperty(nameof(Username));
            ValidateProperty(nameof(Password));

            if (HasErrors) return;

            var user = _mongoService.Authenticate(Username, Password);
            if (user == null)
            {
                AddError(nameof(Username), "Nom d'utilisateur ou mot de passe invalide.");
                AddError(nameof(Password), string.Empty);
                return;
            }

            bool isAdmin = user.Role == "Administrator";
            var calendarViewModel = new AccueilViewModel(Username, isAdmin);

            // Navigue vers la fenêtre d'accueil
            _navigationService.NavigateTo<AccueilWindow>(calendarViewModel);
            _navigationService.Close(_window);
        }

        // Notification de changement de propriété
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
