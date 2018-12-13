using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Common.Utils
{
    public class MongoDbLogger<T> : ILogger<T>
    {
        private readonly MongoDbLogger _logger;

        public MongoDbLogger()
        {
            _logger = new MongoDbLogger(typeof(T));
        }

        public void LogError(string message, Exception ex, DateTime dateTime)
        {
            _logger.LogError(message, ex, dateTime);
        }

        public void LogError(string message, Exception ex)
        {
            _logger.LogError(message, ex);
        }

        public void LogInfo(string message, DateTime dateTime)
        {
            _logger.LogInfo(message, dateTime);
        }

        public void LogInfo(string message)
        {
            _logger.LogInfo(message);
        }
    }

    public class MongoDbLogger : ILogger
    {
        private readonly string _className;
        private readonly string _connectionString;

        public MongoDbLogger(Type loggerType)
        {
            _connectionString = "mongodb://localhost:27017";
            _className = loggerType.ToString().Split(new []{'.'}).Last();
        }

        public void LogError(string message, Exception ex, DateTime dateTime)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("logs");
            var errors = db.GetCollection<BsonDocument>("errors");

            var document = new BsonDocument
            {
                ["className"] = _className,
                ["message"] = message,
                ["stackTrace"] = ex.StackTrace,
                ["dateTime"] = dateTime
            };

            errors.InsertOne(document);
        }

        public void LogError(string message, Exception ex)
        {
            LogError(message, ex, DateTime.Now);
        }

        public void LogInfo(string message, DateTime dateTime)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("logs");
            var infos = db.GetCollection<BsonDocument>("infos");

            var document = new BsonDocument
            {
                ["className"] = _className,
                ["message"] = message,
                ["dateTime"] = dateTime
            };

            infos.InsertOne(document);
        }

        public void LogInfo(string message)
        {
            LogInfo(message, DateTime.Now);
        }
    }
}
