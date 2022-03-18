using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_Practica3_App.Utilities
{
    public class Constants
    {
        public static readonly string EndPointUri = "https://vgmmtwdm2022.documents.azure.com:443/";
        public static readonly string PrimaryKey = "x23GgVkucf6JgTp3AafyEfwRVaTnmmjfnQMrSNlqzQBT635Ndt8JRdKRmLzR9ZTvAXZzJfMXKkVqSFrGShtggA==";
        public static CosmosClient cosmosClient;
        public static string databaseID = "mtwdm2022";
        public static string containerID = "Notes";
        public static Database database;
        public static Container container;
    }
}
