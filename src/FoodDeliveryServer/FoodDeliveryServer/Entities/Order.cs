using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryServer.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<OrderedProduct> OrderedProducts { get; set; }
    }
}
