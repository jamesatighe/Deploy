using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Deploy.DAL;
using Deploy.Service;
using Deploy.ViewModel;
using System.Net;
using Microsoft.Extensions.Options;
using Hangfire;
using Microsoft.AspNetCore.Authorization;

namespace Deploy.Controllers
{
    [Authorize(Policy = "Admins")]
    public class QueueController : Controller
    {

        private readonly DeployDBContext _context;
        private readonly TenantParameters _service;
        private readonly AzureStorageConfig _storageConfig;

        public QueueController(DeployDBContext context, IOptions<AzureStorageConfig> config)
        {
            _context = context;
            _storageConfig = config.Value;
            _service = new TenantParameters(_context, config);
        }

        public async Task<IActionResult> IndexSelected(int Id)
        {
            var queues = await _context.Queue.Where(d => d.TennantID == Id).ToListAsync();

            var viewModel = new Deploy.ViewModel.QueueViewModel();
            viewModel.Queues = new List<Queue>();
            foreach (var queue in queues)
            {
                viewModel.Queues.Add(new Queue()
                {
                    QueueID = queue.QueueID,
                    DeployTypeID = queue.DeployTypeID,
                    DeployName = queue.DeployName,
                    Description = queue.DeployTypes.Description,
                    TennantID = queue.TennantID,
                    TennantName = queue.TennantName,
                    status = queue.status,
                    Order = queue.QueueID
                });
            }


            return View(viewModel);
        }


        public async Task<IActionResult> Edit(int? Id, string sortOrder)
        {

            ViewBag.OrderSortParm = string.IsNullOrEmpty(sortOrder) ? "Order" : "";
            if (Id == null)
            {
                return NotFound();
            }

            var queues = await _context.Queue.Where(d => d.TennantID == Id).ToListAsync();
            var Tenant = _context.Tennants.Where(t => t.TennantID == Id).FirstOrDefault();
            if (queues.Count() < 1)
            {
                var viewModel = new Deploy.ViewModel.QueueViewModel();
                viewModel.Queues = new List<Queue>();
                viewModel.Queues.Add(new Queue()
                {
                    TennantName = Tenant.TennantName,
                    TennantID = Tenant.TennantID,
                    DeployTypeID = 0,
                });
                Console.WriteLine("Test");
                return View(viewModel);
            }
            else
            {
                var viewModel = new Deploy.ViewModel.QueueViewModel();
                viewModel.Queues = new List<Queue>();
                foreach (var queue in queues)
                {
                    viewModel.Queues.Add(new Queue()
                    {
                        QueueID = queue.QueueID,
                        DeployTypeID = queue.DeployTypeID,
                        DeployName = queue.DeployName,
                        TennantID = queue.TennantID,
                        TennantName = queue.TennantName,
                        resourcegroup = queue.resourcegroup,
                        status = queue.status,
                        Order = queue.Order
                    });
                }

                switch (sortOrder)
                {
                    case "Order":
                        viewModel.Queues = viewModel.Queues.OrderBy(o => o.Order).ToList();
                        break;
                    default:
                        viewModel.Queues = viewModel.Queues.OrderBy(o => o.Order).ToList();
                        break;
                }
                return View(viewModel);
            }
        

        }

        [HttpPost]
        public async Task<IActionResult> Edit(QueueViewModel QueueList)
        {
            if (ModelState.IsValid)
            {
                for (var i = 0; i < QueueList.Queues.Count(); i++)
                {
                    var queueItem = _context.Queue.Where(q => q.QueueID == QueueList.Queues[i].QueueID).FirstOrDefault();
                    if (queueItem != null)
                    {
                        queueItem.Order = QueueList.Queues[i].Order;
                    }

                    _context.Update(queueItem);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Edit", new { id = QueueList.Queues.FirstOrDefault().TennantID });
            }
            else
            {
                return RedirectToAction("Edit", new { id = QueueList.Queues.FirstOrDefault().TennantID });
            }
        }


        // POST: Deploy/Delete/5
        [HttpPost]
        public HttpStatusCode Delete(int id)
        {
            var queue = _context.Queue.FirstOrDefault(m => m.QueueID == id);
            if (queue == null)
            {
                return HttpStatusCode.NotFound;
            }

            _context.Queue.Remove(queue);
            _context.SaveChanges();
            return HttpStatusCode.OK;
        }


        public async Task<IActionResult> DeployfromQueue(int id, bool Force)
        {
            var QueueList = await _context.Queue.Where(q => q.TennantID == id).Where(q => q.status != "Completed").OrderBy(q => q.Order).ToListAsync();
            var CobwebVars = await _context.Tennants.Where(t => t.TennantName == "Cobweb Solutions Ltd - IaaS").SingleOrDefaultAsync();
            for (var i = 0; i < QueueList.Count(); i ++)
            {
                //var Id = QueueList[i].DeployTypeID.ToString();
                var Id = QueueList[i].DeployTypeID;
                var TenantId = QueueList[i].TennantID.ToString();

                ////Get Access Token for REST Call
                //var results = RESTApi.PostAction(CobwebVars.AzureTennantID, CobwebVars.AzureClientID, CobwebVars.AzureClientSecret);
                //RESTApi.AccessToken AccessToken = JsonConvert.DeserializeObject<RESTApi.AccessToken>(results.Result);
                //string accesstoken = AccessToken.access_token;

                //StringBuilder tempjson = new StringBuilder();
                //tempjson.AppendLine("{");
                //tempjson.AppendLine("\t\"parameters\": {");
                //tempjson.AppendLine("\t\t\"namespaceName\": \"cobwebdeployment\",");
                //tempjson.AppendLine("\t\t\"resourceGroupName\": \"Deployment\",");
                //tempjson.AppendLine("\t\t\"queueName\": \"" + TenantId + "\",");
                //tempjson.AppendLine("\t\t\"api-version\": \"2015-08-01\",");
                //tempjson.AppendLine("\t\t\"subscriptionId\": \"" + CobwebVars.AzureSubscriptionID + "\",");
                //tempjson.AppendLine("\t\t\"parameters\": {");
                //tempjson.AppendLine("\t\t\t\"properties\": {");
                //tempjson.AppendLine("\t\t\t\t\"enableExpress\": false,");
                //tempjson.AppendLine("\t\t\t\t\"enablePartitioning\": false");
                //tempjson.AppendLine("\t\t\t},");
                //tempjson.AppendLine("\t\t\t\"location\": \"North Europe\"");
                //tempjson.AppendLine("\t\t}");
                //tempjson.AppendLine("\t}");
                //tempjson.AppendLine("}");

                //var jsonfull = tempjson.ToString();

                //var putcontent = RESTApi.PutAsyncSB(TenantId, accesstoken, jsonfull);

                //var connectionString = _storageConfig.SBConnectionString;
                //var queueName = TenantId;
                //var client = new QueueClient(connectionString, queueName, ReceiveMode.PeekLock);
                //Message message = new Message(Encoding.UTF8.GetBytes(Id));
                //await client.SendAsync(message);
                
                
                //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConfig.SBConnectionString);
                //CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
                //CloudQueue messageQueue = queueClient.GetQueueReference("messagequeue");

                //await messageQueue.CreateIfNotExistsAsync();

                //CloudQueueMessage message = new CloudQueueMessage(Id);
                //await messageQueue.AddMessageAsync(message);

                BackgroundJob.Enqueue(() => _service.DeployfromQueue(Id, Force));

            }
            return RedirectToAction("Edit", "Queue", new { id = QueueList.FirstOrDefault().TennantID });
        }

    }

}