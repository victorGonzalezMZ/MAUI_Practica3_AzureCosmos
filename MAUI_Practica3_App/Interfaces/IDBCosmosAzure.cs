using MAUI_Practica3_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Practica3_App.Interfaces
{
    public interface IDBCosmosAzure
    {
        Task CreateDatabase();
        Task CreateDocumentCollection();
        Task<List<Note>> GetAllAsync();
        Task<List<Note>> GetAsync(string partitionKey);
        Task SaveAsync(Note note, bool isNew);
        Task DeleteAsync(string id, string partitionKey);
    }
}
