using FoodDelivery.Enums;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Payments;

public class CreatePaymentDto
{
    [Required]
    public Guid OrderId { get; set; }

    [Required]
    public PaymentMethod PaymentMethod { get; set; }
}
