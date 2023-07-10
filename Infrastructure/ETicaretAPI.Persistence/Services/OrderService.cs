﻿using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.Repository;
using ETicaretAPI.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;
        readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
        }

        public async Task CompleteOrderAsync(string id)
        {
            Order order = await _orderReadRepository.GetByIdAsync(id);
            if (order != null)
                await _completedOrderWriteRepository.AddAsync(new() { OrderId = Guid.Parse(id) });
        }

        public async Task CreateOrderAsync(CreateOrderDto createOrder)
        {
            var orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);

            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderCode = orderCode
            });
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.User)
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product);
               
                var data = query.Skip(page * size).Take(size);


            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data.Select(o => new
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPirce = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UsernName = o.Basket.User.UserName
                }).ToListAsync()
            };
        }

        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var data = await _orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            return new()
            {
                Id = data.Id.ToString(),
                CreatedDate = data.CreatedDate,
                BasketItems = data.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Price,
                    bi.Product.Name,
                    bi.Quantity
                }),
                Address = data.Address,
                Description = data.Description,
                OrderCode = data.OrderCode
            };
        }
    }
}
