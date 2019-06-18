using System;

namespace FoodDeliveryMobileApp.Views.MDP
{
    public abstract class MenuItemBase
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual Type TargetType { get; set; }
    }
}
