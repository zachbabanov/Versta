using DatabaseWorker.Models;

namespace DatabaseWorker
{
    public interface IVerstaTestTaskRepository
    {
        Task<List<DeliveryOrder>> GetDeliveryOrderFormsAsync();
        Task<DeliveryOrder?> GetDeliveryOrderFormByIdAsync(int id);
        Task AddDeliveryOrderFormsAsync(DeliveryOrder deliveryOrderForm);
        Task UpdateDeliveryOrderFormsAsync(DeliveryOrder deliveryOrderForm);
        Task DeleteDeliveryOrderFormsAsync(int id);
        Task<int> GetIdForNextDeliveryOrder();
    }
}
