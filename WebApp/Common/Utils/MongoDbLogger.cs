using Common.Interfaces;
using Common.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Common.Utils
{
    public class MongoDbLogger<T> : ILogger<T>
    {
        private readonly MongoDbLogger _logger;

        public MongoDbLogger()
        {
            _logger = new MongoDbLogger(typeof(T));
        }

        public ErrorLogModel GetErrorLog(string objectId) => _logger.GetErrorLog(objectId);

        public IEnumerable<ErrorLogModel> ErrorLogs() => _logger.ErrorLogs();

        public IEnumerable<InfoLogModel> InfoLogs() => _logger.InfoLogs();

        public void LogError(string message, Exception ex, DateTime dateTime)
            => _logger.LogError(message, ex, dateTime);

        public void LogError(string message, Exception ex) => _logger.LogError(message, ex);

        public void LogInfo(string message, DateTime dateTime) => _logger.LogInfo(message, dateTime);

        public void LogInfo(string message) => _logger.LogInfo(message);

        public void LogInfo(string message, DateTime dateTime, object args) => _logger.LogInfo(message, dateTime, args);

        public void LogInfo(string message, object args) => _logger.LogInfo(message, args);

        public void RemoveInfoLog(string objetId) => _logger.RemoveInfoLog(objetId);

        public void RemoveErrorLog(string objectId) => _logger.RemoveErrorLog(objectId);

        public void ClearAllInfoLogs() => _logger.ClearAllInfoLogs();

        public void ClearAllErrorLogs() => _logger.ClearAllErrorLogs();

        public InfoLogModel GetInfoLog(string objectId) => _logger.GetInfoLog(objectId);
    }

    public class MongoDbLogger : ILogger
    {
        private readonly string _className;
        private readonly string _connectionString;

        public MongoDbLogger(Type loggerType)
        {
            _connectionString = "mongodb://localhost:27017";
            _className = loggerType.ToString().Split(new[] { '.' }).Last();
        }

        public ErrorLogModel GetErrorLog(string objectId)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("logs");
            var errors = db.GetCollection<BsonDocument>("errors");

            return errors.AsQueryable().AsEnumerable().Select(bson => new ErrorLogModel(
                bson["_id"].AsObjectId.ToString(),
                bson["dateTime"].ToUniversalTime(),
                bson["message"].AsString,
                bson["className"].AsString,
                string.Join(Environment.NewLine, bson["stackTrace"].AsBsonArray.Select(x => x.AsString))))
               .FirstOrDefault(x => x.ObjectId.Equals(objectId));
        }

        public IEnumerable<ErrorLogModel> ErrorLogs()
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("logs");
            var errors = db.GetCollection<BsonDocument>("errors");

            return errors.AsQueryable().AsEnumerable().Select(bson => new ErrorLogModel(
                bson["_id"].AsObjectId.ToString(),
                bson["dateTime"].ToUniversalTime(),
                bson["message"].AsString,
                bson["className"].AsString,
                string.Join(Environment.NewLine, bson["stackTrace"].AsBsonArray.Select(x => x.AsString))))
               .OrderByDescending(e => e.DateTime);
        }

        public IEnumerable<InfoLogModel> InfoLogs()
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("logs");
            var infos = db.GetCollection<BsonDocument>("infos");

            return infos.AsQueryable().AsEnumerable().Select(bson => new InfoLogModel(
                bson["_id"].AsObjectId.ToString(),
                bson["dateTime"].ToUniversalTime(),
                bson["message"].AsString,
                bson["className"].AsString,
                string.Join(Environment.NewLine, bson["args"].AsBsonArray.Select(x => x.AsString))))
                .OrderByDescending(e => e.DateTime);
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
                ["stackTrace"] = new BsonArray(ex.StackTrace.Split(
                    new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)),
                ["dateTime"] = dateTime
            };

            errors.InsertOne(document);
        }

        public void LogError(string message, Exception ex) => LogError(message, ex, DateTime.Now);

        public void LogInfo(string message, DateTime dateTime) => LogInfo(message, dateTime, null);

        public void LogInfo(string message) => LogInfo(message, DateTime.Now);

        public void LogInfo(string message, DateTime dateTime, object arg)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("logs");
            var infos = db.GetCollection<BsonDocument>("infos");

            if (arg == null) arg = "";

            var serializedArgs = JsonConvert.SerializeObject(arg, Formatting.Indented).Split(
                new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var document = new BsonDocument
            {
                ["className"] = _className,
                ["message"] = message,
                ["dateTime"] = dateTime,
                ["args"] = new BsonArray(serializedArgs)
            };

            infos.InsertOne(document);
        }

        public void LogInfo(string message, object args) => LogInfo(message, DateTime.Now, args);

        public void RemoveInfoLog(string objectId)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("logs");
            var infos = db.GetCollection<BsonDocument>("infos");

            infos.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(objectId)));
        }

        public void RemoveErrorLog(string objectId)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("logs");
            var errors = db.GetCollection<BsonDocument>("errors");

            errors.DeleteOne(Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(objectId)));
        }

        public void ClearAllInfoLogs()
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("logs");
            var infos = db.GetCollection<BsonDocument>("infos");

            infos.DeleteMany(a => true);
        }

        public void ClearAllErrorLogs()
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("logs");
            var errors = db.GetCollection<BsonDocument>("errors");

            errors.DeleteMany(a => true);
        }

        public InfoLogModel GetInfoLog(string objectId)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase("logs");
            var infos = db.GetCollection<BsonDocument>("infos");

            return infos.AsQueryable().AsEnumerable().Select(bson => new InfoLogModel(
                bson["_id"].AsObjectId.ToString(),
                bson["dateTime"].ToUniversalTime(),
                bson["message"].AsString,
                bson["className"].AsString,
                string.Join(Environment.NewLine, bson["args"].AsBsonArray.Select(x => x.AsString))))
               .FirstOrDefault(x => x.ObjectId.Equals(objectId));
        }
    }
}
