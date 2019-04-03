using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryServer.Entities
{
    [Table("Ingradients")]
    public class Ingradient
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required, MaxLength(500)]
        public string Name { get; set; }

        public ICollection<PizzaIngradients> PizzaIngradients { get; set; }
    }
}
