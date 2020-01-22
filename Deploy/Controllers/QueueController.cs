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

        public async Task<IActionResult> Edit(int Id)
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
                    Description = queue.Description,
                    TennantID = queue.TennantID,
                    TennantName = queue.TennantName,
                    resourcegroup = queue.resourcegroup,
                    status = queue.status,
                    Order = queue.Order
                });
            }

            return View(viewModel);
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
                    Description = queue.Description,
                    TennantID = queue.TennantID,
                    TennantName = queue.TennantName,
                    resourcegroup = queue.resourcegroup,
                    status = queue.status,
                    Order = queue.Order
                });
            }


            return View(viewModel);
        }

    

        public async Task<IActionResult> Index(int? Id, string sortOrder)
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
                        Description = queue.Description,
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
        public async Task<IActionResult> Index(QueueViewModel QueueList)
        {
            if (ModelState.IsValid)
            {
                for (var i = 0; i < QueueList.Queues.Count(); i++)
                {

                    var order = QueueList.Queues[i].Order;
                    var queueItem = _context.Queue.Where(q => q.QueueID == QueueList.Queues[i].QueueID).FirstOrDefault();
                    
                    if (queueItem != null)
                    {
                        if (order > 1)
                        {
                            var prevQueueItem = QueueList.Queues.Where(q => q.Order == (order - 1)).FirstOrDefault();
                            queueItem.Order = order;
                            queueItem.DependsOn = prevQueueItem.QueueID;
                        }
                        else
                        {
                            queueItem.Order = QueueList.Queues[i].Order;
                            queueItem.DependsOn = 0;
                        }
                    }
                    _context.Update(queueItem);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index", new { id = QueueList.Queues.FirstOrDefault().TennantID });
            }
            else
            {
                return RedirectToAction("Index", new { id = QueueList.Queues.FirstOrDefault().TennantID });
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
            int tenantID = id;
            var QueueList = await _context.Queue.Where(q => q.TennantID == id).Where(q => q.status == "New").OrderBy(q => q.Order).ToListAsync();
            if (QueueList.Count() < 1)
            {
                return RedirectToAction("Index", "Queue", new { id = tenantID });
            }
            for (var i = 0; i < QueueList.Count(); i ++)
            {
                var Id = QueueList[i].DeployTypeID;
                var TenantId = QueueList[i].TennantID.ToString();

                var result = await _service.DeployfromQueue(Id, Force);
                
            }
            return RedirectToAction("Index", "Queue", new { id = QueueList.FirstOrDefault().TennantID });
        }

    }

}