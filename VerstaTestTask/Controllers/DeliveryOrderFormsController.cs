using DatabaseWorker;
using DatabaseWorker.Models;
using Microsoft.AspNetCore.Mvc;

namespace VerstaTestTask.Controllers
{
    public class DeliveryOrderFormsController : Controller
    {
        private readonly IVerstaTestTaskRepository dbRepository;

        public DeliveryOrderFormsController(IVerstaTestTaskRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }

        // GET: DeliveryOrderForms
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await dbRepository.GetDeliveryOrderFormsAsync());
        }

        // GET: DeliveryOrderForms/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var deliveryOrderForm = await dbRepository.GetDeliveryOrderFormByIdAsync(id);
            if (deliveryOrderForm == null)
            {
                return NotFound();
            }

            return View(deliveryOrderForm);
        }

        // GET: DeliveryOrderForms/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new DeliveryOrder()
            {
                Id = await dbRepository.GetIdForNextDeliveryOrder()
            });
        }

        // POST: DeliveryOrderForms/Create
        [HttpPost]
        public async Task<IActionResult> Create(
            [Bind("Id,SenderCity,SenderAddress,RecipientCity,RecipientAddress,CargoWeight,CargoPickupDate")]
            DeliveryOrder deliveryOrderForm)
        {
            if (ModelState.IsValid)
            {
                await dbRepository.AddDeliveryOrderFormsAsync(deliveryOrderForm);

                return RedirectToAction(nameof(Index));
            }

            return View(deliveryOrderForm);
        }

        // GET: DeliveryOrderForms/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var deliveryOrderForm = await dbRepository.GetDeliveryOrderFormByIdAsync(id);
            if (deliveryOrderForm == null)
            {
                return NotFound();
            }

            return View(deliveryOrderForm);
        }

        // POST: DeliveryOrderForms/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,SenderCity,SenderAddress,RecipientCity,RecipientAddress,CargoWeight,CargoPickupDate")]
            DeliveryOrder deliveryOrderForm)
        {
            if (id != deliveryOrderForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await dbRepository.UpdateDeliveryOrderFormsAsync(deliveryOrderForm);

                return RedirectToAction(nameof(Index));
            }

            return View(deliveryOrderForm);
        }

        // GET: DeliveryOrderForms/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deliveryOrderForm = await dbRepository.GetDeliveryOrderFormByIdAsync(id);
            if (deliveryOrderForm == null)
            {
                return NotFound();
            }

            return View(deliveryOrderForm);
        }

        // POST: DeliveryOrderForms/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await dbRepository.DeleteDeliveryOrderFormsAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}