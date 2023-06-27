﻿using ETicaretAPI.Application.DTOs.Order;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDto createOrder);
        Task<List<ListOrder>> GetAllOrdersAsync(int page, int size);
    }
}
