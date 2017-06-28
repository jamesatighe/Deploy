using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Deploy.DAL;
using Deploy.Service;
using Deploy.Models;
using Deploy.ViewModel;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Hangfire;

namespace Deploy.Controllers
{
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
                    TennantID = queue.TennantID,
                    TennantName = queue.TennantName,
                    status = queue.status,
                    Order = queue.Order
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
            var QueueList = await _context.Queue.Where(q => q.TennantID == id).OrderBy(q => q.Order).ToListAsync();
            for (var i = 0; i < QueueList.Count(); i ++)
            {
                var Id = QueueList[i].DeployTypeID;

                BackgroundJob.Enqueue(() => _service.DeployfromQueue(Id, Force));
            
            }
            return RedirectToAction("Edit", "Queue", new { id = QueueList.FirstOrDefault().TennantID });
        }

    }

}