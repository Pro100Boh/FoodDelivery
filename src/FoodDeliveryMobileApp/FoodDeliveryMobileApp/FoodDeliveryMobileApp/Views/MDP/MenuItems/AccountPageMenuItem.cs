using FoodDeliveryMobileApp.ViewModels;
using System;

namespace FoodDeliveryMobileApp.Views.MDP
{
    public class AccountPageMenuItem : MenuItemBase
    {
        public override Type TargetType
        {
            get
            {
                if (AccountViewModel.Instance.IsAuthorized)
                    return typeof(ManageAccountPage);
                else
                    return typeof(LogInPage);
            }

            set => base.TargetType = value;
        }
    }
}
