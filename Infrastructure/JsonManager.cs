using Domain;
using Domain.EntityArea;
using Domain.TransactionArea;
using System.Text.Json;

namespace Infrastructure
{
    public class JsonManager : IJsonManager
    {
        public string TransactionsFolderPath { get; set; }
        public string EntitiesFolderPath { get; set; }
        public JsonManager() { }
        public JsonManager(string transactionsFolderPath = "", string entitiesFolderPath = "")
        {
            TransactionsFolderPath = transactionsFolderPath;
            EntitiesFolderPath = entitiesFolderPath;
        }

        public void SaveTransaction(Transaction transaction)
        {
            if (!Directory.Exists(TransactionsFolderPath))
            {
                Directory.CreateDirectory(TransactionsFolderPath);
            }
            if (File.Exists(Path.Combine(TransactionsFolderPath, transaction.Id + ".json")))
            {
                throw new Exception($"A transaction with {nameof(transaction.Id)} - '{transaction.Id}' already exists, try again");
            }

            string objString = JsonSerializer.Serialize(transaction);
            string filePath = Path.Combine(TransactionsFolderPath, transaction.Id + ".json");
            File.WriteAllText(filePath, objString);
        }

        public void SaveEntity(Entity entity)
        {
            if (!Directory.Exists(EntitiesFolderPath))
            {
                Directory.CreateDirectory(EntitiesFolderPath);
            }
            if (File.Exists(Path.Combine(EntitiesFolderPath, entity.Id + ".json")))
            {
                throw new Exception($"An entity with {nameof(entity.Id)} - '{entity.Id}' already exists, try again");
            }

            string objString = JsonSerializer.Serialize(entity);
            string filePath = Path.Combine(EntitiesFolderPath, entity.Id + ".json");
            File.WriteAllText(filePath, objString);
        }

        public string GetTransactionStr(int id)
        {
            if (!File.Exists(Path.Combine(TransactionsFolderPath, id + ".json")))
            {
                throw new Exception($"A transaction with {nameof(id)} - '{id}' does not exist, try again");
            }

            return File.ReadAllText(Path.Combine(TransactionsFolderPath, id + ".json"));
        }

        public Entity GetEntity(Guid id)
        {
            if (!File.Exists(Path.Combine(EntitiesFolderPath, id + ".json")))
            {
                throw new Exception($"An entity with {nameof(id)} - '{id}' does not exist, try again");
            }

            var entitySrt = File.ReadAllText(Path.Combine(EntitiesFolderPath, id + ".json"));
            Entity entity = JsonSerializer.Deserialize<Entity>(entitySrt);

            return entity;
        }
    }
}
