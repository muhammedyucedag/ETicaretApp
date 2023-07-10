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
        readonly ICompletedOrderReadRepository _completedOrderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
            _completedOrderReadRepository = completedOrderReadRepository;
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

            var data2 = from order in data
                   join completedOrder in _completedOrderReadRepository.Table
                   on order.Id equals completedOrder.OrderId into co
                   from _co in co.DefaultIfEmpty()
                   select new
                   {
                       Id = order.Id,
                       CreatedDate = order.CreatedDate,
                       OrderCode = order.OrderCode,
                       Basket = order.Basket,
                       Completed = _co != null ? true : false
                   };

            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data2.Select(o => new
                {
                    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPirce = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UsernName = o.Basket.User.UserName,
                    o.Completed
                }).ToListAsync()
            };
        }

        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var data =  _orderReadRepository.Table
                .Include(o => o.Basket)
                .ThenInclude(b => b.BasketItems)
                .ThenInclude(bi => bi.Product);

            var data2 = await (from order in data
                        join completeOrder in _completedOrderReadRepository.Table
                        on order.Id equals completeOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            Id = order.Id,
                            CreatedDate = order.CreatedDate,
                            OrderCode = order.OrderCode,
                            Basket = order.Basket,
                            Completed = _co != null ? true : false,
                            Address = order.Address,
                            Description = order.Description
                        }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));

            return new()
            {
                Id = data2.Id.ToString(),
                BasketItems = data2.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Price,
                    bi.Product.Name,
                    bi.Quantity
                }),
                Address = data2.Address,
                CreatedDate = data2.CreatedDate,
                Description = data2.Description,
                OrderCode = data2.OrderCode,
                Completed  = data2.Completed
            };
        }


        public async Task CompleteOrderAsync(string id)
        {
            Order order = await _orderReadRepository.GetByIdAsync(id);
            if (order != null)
            {
                await _completedOrderWriteRepository.AddAsync(new() { OrderId = Guid.Parse(id) });
                await _completedOrderWriteRepository.SaveAsync();
            }
        }
    }
}
