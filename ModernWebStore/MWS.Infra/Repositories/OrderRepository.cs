﻿using MWS.Domain.Entidades;
using MWS.Domain.Repositories;
using MWS.Domain.Specs;
using MWS.Infra.Persistence.DataContexts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MWS.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private StoreDataContext _context;

        public OrderRepository(StoreDataContext context)
        {
            _context = context;
        }

        public void Create(Order order)
        {
            _context.Orders.Add(order);
        }

        public List<Order> Get(string email, int skip, int take)
        {
            return _context.Orders
                .Where(OrderSpecs.GetOrdersFromUser(email))
                .OrderByDescending(x => x.Date)
                .Skip(skip)
                .Take(take).ToList();
        }

        public List<Order> GetCanceled(string email)
        {
            return _context.Orders
                .Where(OrderSpecs.GetCanceledOrders(email))
                .OrderByDescending(x => x.Date)
                .ToList();
        }

        public List<Order> GetCreated(string email)
        {
            return _context.Orders
                .Where(OrderSpecs.GetCreatedOrders(email))
                .OrderByDescending(x => x.Date)
                .ToList();
        }

        public List<Order> GetDelivered(string email)
        {
            return _context.Orders
                .Where(OrderSpecs.GetDeliveredOrders(email))
                .OrderByDescending(x => x.Date)
                .ToList();
        }

        public Order GetDetails(int id, string email)
        {
            return _context.Orders
                .Include(x => x.OrderItems)
                .Where(OrderSpecs.GetOrderDetails(id, email))
                .FirstOrDefault();
        }

        public Order GetHeader(int id, string email)
        {
            return _context.Orders
                .Where(OrderSpecs.GetOrderDetails(id, email))
                .FirstOrDefault();
        }

        public List<Order> GetPaid(string email)
        {
            return _context.Orders
                .Where(OrderSpecs.GetPaidOrders(email))
                .OrderByDescending(x => x.Date)
                .ToList();
        }

        public void Update(Order order)
        {
            _context.Entry<Order>(order).State = EntityState.Modified;
        }
    }
}