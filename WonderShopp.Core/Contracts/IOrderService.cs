﻿using System.Collections.Generic;
using WonderShopp.Core.Models;
using WonderShopp.Core.ViewModels;

namespace WonderShopp.Services
{
    public interface IOrderService
    {
        void CreateOrder(Order baseOrder, List<CartItemVM> cartItems);
        List<Order> GetOrderList();
        Order GetOrder(string Id);
        void UpdateOrder(Order updatedOrder);
    }
}