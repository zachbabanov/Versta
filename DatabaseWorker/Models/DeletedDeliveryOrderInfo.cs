using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseWorker.Models
{
    public class DeletedDeliveryOrderInfo
    {
        [Key]
        public int DeliveryOrderId { get; set; }
        public DeliveryOrder DeliveryOrder { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
