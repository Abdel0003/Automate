using Automate.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automate.Utils
{
    public class MongoDBService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<UserModel> _users;
        private readonly IMongoCollection<Tache> _tacheCollection;

        //public MongoDBService()
        //{
        //    var client = new MongoClient("mongodb+srv://atounaok:June7600@cluster0.rekox.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"); // URL du serveur MongoDB
        //    var database = client.GetDatabase("VotreNomDeBaseDeDonnees");
        //    _tacheCollection = database.GetCollection<Tache>("Taches");
        //}

        public MongoDBService(string databaseName)
        {
            var client = new MongoClient("mongodb+srv://atounaok:June7600@cluster0.rekox.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0"); // URL du serveur MongoDB
            _database = client.GetDatabase(databaseName);
            _users = _database.GetCollection<UserModel>("Users");
            _tacheCollection = _database.GetCollection<Tache>("Taches");
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName) 
        {
            return _database.GetCollection<T>(collectionName);
        }

        public UserModel Authenticate(string? username, string? password)
        {
            var user = _users.Find(u => u.Username == username && u.Password == password).FirstOrDefault();
            return user;
        }
        public void RegisterUser(UserModel user)
        {
            _users.InsertOne(user);
        }

        public async Task AjouterTacheAsync(Tache tache)
        {
            await _tacheCollection.InsertOneAsync(tache);
        }

        public List<Tache> GetTasksByDate(DateTime date)
        {
            // Filtrer par date pour obtenir les tâches du jour sélectionné
            var filter = Builders<Tache>.Filter.Eq("DateAjout", date.Date);
            return _tacheCollection.Find(filter).ToList();
        }

    }

}
