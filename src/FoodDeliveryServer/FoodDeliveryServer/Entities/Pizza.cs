using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryServer.Entities
{
    [Table("Pizza")]
    public class Pizza
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, MaxLength(500)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [MaxLength(200)]
        public string Image { get; set; }

        public ICollection<PizzaIngradients> PizzaIngradients { get; set; }
    }
}
