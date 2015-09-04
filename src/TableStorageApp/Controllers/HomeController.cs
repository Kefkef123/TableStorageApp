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

        public ActionResult Index()
        {
            var customers = _table.ExecuteQuery(new TableQuery<CustomerEntity>());
            
            return View(customers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CustomerForm form)
        {
            var customer = _modelFactory.Create(form);

            await _table.ExecuteAsync(TableOperation.Insert(customer));

            return RedirectToAction("Index");
        }

        private readonly CloudStorageAccount _storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
        private readonly CloudTableClient _tableClient;
        private readonly CloudTable _table;
        private readonly ModelFactory _modelFactory = new ModelFactory();
    }
}