using System;
using System.Collections.Generic;
using ShopSolution.Core.Entities.OrderAggregate;

namespace ShopSolution.API.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShipTiAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Subtotal { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; }
    }
}