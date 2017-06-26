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
            _service = new TenantParameters(_context, _storageConfig);
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
        public ActionResult Delete(int id)
        {
            var queue = _context.Queue.FirstOrDefault(m => m.QueueID == id);
            if (queue == null)
            {
                return NotFound();
            }

            _context.Queue.Remove(queue);
            _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = queue.TennantID });
        }

        public async Task<IActionResult> DeployToAzure(int id, bool Force)
        {
            var QueueList = await _context.Queue.Where(q => q.TennantID == id).ToListAsync();
            for (var i = 0; i < QueueList.Count(); i ++)
            {
                var Id = QueueList[i].DeployTypeID;
                //var deployTypes = _context.DeployTypes.Include(d => d.Tennants).Where(d => d.DeployTypeID == Id).FirstOrDefault();
                string[] results = await _service.QueueDeployment(Id, Force);
                if (results[0] == "DeployExists")
                {
                    QueueList[i].status = "Deploy Exists";
                    _context.Update(QueueList[i]);
                    await _context.SaveChangesAsync();
                }
                else if (results[1] == "TemplateInvalid")
                {
                    QueueList[i].status = "Template Invalid";
                    _context.Update(QueueList[i]);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    QueueList[i].status = "OK to Deploy";
                    _context.Update(QueueList[i]);
                    await _context.SaveChangesAsync();
                }

            }
            return RedirectToAction("Edit", "Queue", new { id = QueueList.FirstOrDefault().TennantID });
        }

    }

}