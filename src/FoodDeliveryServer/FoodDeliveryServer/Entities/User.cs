using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDeliveryServer.Entities
{
    [Table("Users")]
    public class User : IdentityUser
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
