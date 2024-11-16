using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    // Attributs pour stocker les actions et les conditions d'exécution
    private readonly Action<object> _executeWithParam;         // Action à exécuter avec paramètre
    private readonly Action _executeWithoutParam;             // Action à exécuter sans paramètre
    private readonly Func<object, bool> _canExecuteWithParam; // Condition pour exécuter avec paramètre
    private readonly Func<bool> _canExecuteWithoutParam;      // Condition pour exécuter sans paramètre

    // Événement déclenché lorsque la capacité d'exécution change
    public event EventHandler CanExecuteChanged;

    /// <summary>
    /// Constructeur pour les commandes avec paramètres.
    /// </summary>
    /// <param name="execute">Action à exécuter.</param>
    /// <param name="canExecute">Condition pour déterminer si l'action peut s'exécuter (facultatif).</param>
    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _executeWithParam = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecuteWithParam = canExecute;
    }

    /// <summary>
    /// Constructeur pour les commandes sans paramètres.
    /// </summary>
    /// <param name="execute">Action à exécuter.</param>
    /// <param name="canExecute">Condition pour déterminer si l'action peut s'exécuter (facultatif).</param>
    public RelayCommand(Action execute, Func<bool> canExecute = null)
    {
        _executeWithoutParam = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecuteWithoutParam = canExecute;
    }

    /// <summary>
    /// Détermine si la commande peut s'exécuter.
    /// </summary>
    /// <param name="parameter">Paramètre optionnel passé à la commande.</param>
    /// <returns>True si la commande peut s'exécuter, sinon False.</returns>
    public bool CanExecute(object parameter)
    {
        if (_canExecuteWithParam != null)
        {
            return _canExecuteWithParam(parameter); // Vérifie avec condition pour paramètres
        }

        return _canExecuteWithoutParam == null || _canExecuteWithoutParam(); // Vérifie sans paramètres
    }

    /// <summary>
    /// Exécute la commande.
    /// </summary>
    /// <param name="parameter">Paramètre optionnel passé à la commande.</param>
    public void Execute(object parameter)
    {
        if (_executeWithParam != null)
        {
            _executeWithParam(parameter); // Exécute avec paramètre
        }
        else
        {
            _executeWithoutParam?.Invoke(); // Exécute sans paramètre
        }
    }

    /// <summary>
    /// Notifie que l'état de la commande a changé.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
