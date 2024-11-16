using Automate.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automate.Utils
{
    /// <summary>
    /// Service pour interagir avec la base de données MongoDB.
    /// </summary>
    public class MongoDBService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<UserModel> _users;
        private readonly IMongoCollection<Tache> _tacheCollection;

        /// <summary>
        /// Constructeur pour initialiser les collections MongoDB.
        /// </summary>
        /// <param name="databaseName">Nom de la base de données.</param>
        public MongoDBService(string databaseName)
        {
            var client = new MongoClient("mongodb+srv://atounaok:June7600@cluster0.rekox.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
            _database = client.GetDatabase(databaseName);
            _users = _database.GetCollection<UserModel>("Users");
            _tacheCollection = _database.GetCollection<Tache>("Taches");
        }

        /// <summary>
        /// Récupère une collection générique depuis la base de données.
        /// </summary>
        /// <typeparam name="T">Type des documents dans la collection.</typeparam>
        /// <param name="collectionName">Nom de la collection.</param>
        /// <returns>Collection MongoDB.</returns>
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// Authentifie un utilisateur en vérifiant son nom d'utilisateur et son mot de passe.
        /// </summary>
        /// <param name="username">Nom d'utilisateur.</param>
        /// <param name="password">Mot de passe.</param>
        /// <returns>Modèle d'utilisateur si trouvé, sinon null.</returns>
        public UserModel Authenticate(string? username, string? password)
        {
            return _users.Find(u => u.Username == username && u.Password == password).FirstOrDefault();
        }

        /// <summary>
        /// Enregistre un nouvel utilisateur dans la base de données.
        /// </summary>
        /// <param name="user">Modèle d'utilisateur à enregistrer.</param>
        public void RegisterUser(UserModel user)
        {
            _users.InsertOne(user);
        }

        /// <summary>
        /// Ajoute une nouvelle tâche dans la collection des tâches.
        /// </summary>
        /// <param name="tache">Modèle de la tâche à ajouter.</param>
        /// <returns>Tâche asynchrone représentant l'opération.</returns>
        public async Task AjouterTacheAsync(Tache tache)
        {
            await _tacheCollection.InsertOneAsync(tache);
        }

        /// <summary>
        /// Récupère les tâches pour une date spécifique.
        /// </summary>
        /// <param name="date">Date pour laquelle récupérer les tâches.</param>
        /// <returns>Liste des tâches associées à la date donnée.</returns>
        public List<Tache> GetTasksByDate(DateTime date)
        {
            var filter = Builders<Tache>.Filter.Eq("DateAjout", date.Date);
            return _tacheCollection.Find(filter).ToList();
        }
    }
}
