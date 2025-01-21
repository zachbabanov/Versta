using DatabaseWorker.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWorker
{
    internal class VerstaTestTaskRepository : IVerstaTestTaskRepository
    {
        private readonly VerstaTestTaskContext dbContext;
        public VerstaTestTaskRepository(VerstaTestTaskContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        public async Task<List<DeliveryOrder>> GetDeliveryOrderFormsAsync()
        {
            List<DeliveryOrder>? deliveryOrderForm = await dbContext.DeliveryOrder
                .Include(x => x.DeletedDeliveryOrderInfo)
                .Where(c => c.Id != c.DeletedDeliveryOrderInfo.DeliveryOrderId)
                .ToListAsync();
            if (deliveryOrderForm == null)
            {
                throw new ArgumentNullException(nameof(deliveryOrderForm));
            }

            return deliveryOrderForm;
        }

        public async Task<DeliveryOrder?> GetDeliveryOrderFormByIdAsync(int id)
        {
            DeliveryOrder? deliveryOrderForm = await dbContext.DeliveryOrder
                .Include(x => x.DeletedDeliveryOrderInfo)
                .Where(c => c.Id != c.DeletedDeliveryOrderInfo.DeliveryOrderId)
                .FirstOrDefaultAsync(x => x.Id == id);
            return deliveryOrderForm;
        }

        public async Task AddDeliveryOrderFormsAsync(DeliveryOrder deliveryOrderForm)
        {
            if (deliveryOrderForm == null)
            {
                throw new ArgumentNullException(nameof(deliveryOrderForm));
            }

            await dbContext.DeliveryOrder.AddAsync(deliveryOrderForm);
            await dbContext.SaveChangesAsync();
        }
        
        public async Task UpdateDeliveryOrderFormsAsync(DeliveryOrder updatedDeliveryOrderForm)
        {
            if (updatedDeliveryOrderForm == null)
            {
                throw new ArgumentNullException(nameof(updatedDeliveryOrderForm));
            }

            dbContext.Update(updatedDeliveryOrderForm);
            await dbContext.SaveChangesAsync();
        }
        
        public async Task DeleteDeliveryOrderFormsAsync(int id)
        {
            await using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    DeliveryOrder? deliveryOrderForm = dbContext.DeliveryOrder.FirstOrDefault(x => x.Id == id)!;

                    if (deliveryOrderForm == null)
                    {
                        throw new ArgumentNullException(nameof(deliveryOrderForm));
                    }
                    
                    await dbContext.DeletedDeliveryOrderInfo.AddAsync(new DeletedDeliveryOrderInfo
                    {
                        DeliveryOrderId = deliveryOrderForm.Id,
                        DeliveryOrder = deliveryOrderForm,
                        DeletedAt = DateTime.Now,

                    });

                    deliveryOrderForm.IsDeleted = true;

                    dbContext.DeliveryOrder.
                        Update(deliveryOrderForm);

                    await dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task<int> GetIdForNextDeliveryOrder()
        {
            try
            {
                int maxId = await dbContext.DeliveryOrder.MaxAsync(x => x.Id);
                return maxId + 1;
            }
            catch (InvalidOperationException ex) 
            {
                return 1;
            }
        }
    }
}
