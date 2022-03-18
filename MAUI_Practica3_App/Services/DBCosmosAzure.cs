using MAUI_Practica3_App.Interfaces;
using MAUI_Practica3_App.Models;
using MAUI_Practica3_App.Utilities;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Practica3_App.Services
{
    public class DBCosmosAzure : IDBCosmosAzure
    {
        public DBCosmosAzure()
        {
            Constants.cosmosClient = new CosmosClient(Constants.EndPointUri, Constants.PrimaryKey);
        }

        public async Task CreateDatabase()
        {
            try
            {
                Constants.database =
                    await Constants.cosmosClient.CreateDatabaseIfNotExistsAsync(Constants.databaseID);
            }
            catch (CosmosException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task CreateDocumentCollection()
        {
            try
            {
                Constants.container =
                    await Constants.database.CreateContainerIfNotExistsAsync(Constants.containerID, "/partitionKey");
            }
            catch (CosmosException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Note>> GetAllAsync()
        {
            List<Note> notes = new List<Note>();
            try
            {
                var sqlQuery = "SELECT * FROM c";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<Note> queryResultSetIterator = Constants.container.GetItemQueryIterator<Note>(queryDefinition);
                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<Note> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (var item in currentResultSet)
                    {
                        notes.Add(item);
                    }
                }
            }
            catch (CosmosException ex)
            {
                throw new Exception(ex.Message);
            }
            return notes;
        }

        public async Task<List<Note>> GetAsync(string partitionKey)
        {
            List<Note> notes = new List<Note>();
            try
            {
                var sqlQuery = "SELECT * FROM c WHERE c.partitionKey = '" + partitionKey + "'";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<Note> queryResultSetIterator = Constants.container.GetItemQueryIterator<Note>(queryDefinition);
                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<Note> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (var item in currentResultSet)
                    {
                        notes.Add(item);
                    }
                }
            }
            catch (CosmosException ex)
            {
                throw new Exception(ex.Message);
            }
            return notes;
        }

        public async Task SaveAsync(Note note, bool isNew)
        {
            try
            {
                if (isNew)
                {
                    ItemResponse<Note> noteResponse =
                        await Constants.container.CreateItemAsync<Note>(note, new PartitionKey(note.PartitionKey));
                }
                else
                {
                    ItemResponse<Note> noteResponse =
                        await Constants.container.ReadItemAsync<Note>(note.Id, new PartitionKey(note.PartitionKey));

                    var item = noteResponse.Resource;

                    noteResponse =
                        await Constants.container.ReplaceItemAsync<Note>(note, note.Id, new PartitionKey(note.PartitionKey));
                }
            }
            catch (CosmosException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(string id, string partitionKey)
        {
            try
            {
                ItemResponse<Note> noteResponse =
                    await Constants.container.DeleteItemAsync<Note>(id, new PartitionKey(partitionKey));
            }
            catch (CosmosException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
