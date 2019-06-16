using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryServer.Entities
{
    [Table("OrderedProducts")]
    public class OrderedProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderedProductId { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        [Required, MaxLength(500)]
        public string ProductName { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal ProductPrice { get; set; }
    }
}
