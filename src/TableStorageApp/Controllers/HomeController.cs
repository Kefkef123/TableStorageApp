using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace TableStorageApp
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            _tableClient = _storageAccount.CreateCloudTableClient();
            _table = _tableClient.GetTableReference("people");
            _table.CreateIfNotExists();
        }

        public async Task<string> AddCustomer()
        {
            var customer = new CustomerEntity("Harp", "Walter")
            {
                Email = "Henk@vanHal.com",
                PhoneNumber = "06-12345678"
            };

            await _table.ExecuteAsync(TableOperation.Insert(customer));

            return "aap";
        }

        private readonly CloudStorageAccount _storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
        private readonly CloudTableClient _tableClient;
        private readonly CloudTable _table;
    }
}