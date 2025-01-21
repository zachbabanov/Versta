using DatabaseWorker.Models.ValidationAttribute;
using System.ComponentModel.DataAnnotations;

namespace DatabaseWorker.Models
{
    public class DeliveryOrder
    {
        [Display(Name = "Номер Заказа")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Город отправителя")]
        public string? SenderCity { get; set; }
        [Required]
        [Display(Name = "Адрес отправителя")]
        public string? SenderAddress { get; set; }
        [Required]
        [Display(Name = "Город получателя")]
        public string? RecipientCity { get; set; }
        [Required]
        [Display(Name = "Адрес получателя")]
        public string? RecipientAddress { get; set; }
        [Required]
        [Display(Name = "Вес груза")]
        public float CargoWeight { get; set; }
        [Required]
        [DateNotLessThanNow]
        [Display(Name = "Дата забора груза")]
        public DateTime CargoPickupDate { get; set; }
        public bool IsDeleted { get; set; }
        public DeletedDeliveryOrderInfo? DeletedDeliveryOrderInfo { get; set; }
    }
}
