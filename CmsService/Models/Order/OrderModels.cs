using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Models.Order
{
    public class OrderModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Tell { get; set; }
        public string RegistererTell { get; set; }
        public string InDate { get; set; }
        public string OutDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public string Hotel { get; set; }
        public string HotelAddress { get; set; }
        public Vehicle? Vehicle { get; set; }
        public string VehicleNumber { get; set; }
        public string DepartureTime { get; set; }
        public string ArriveTime { get; set; }

        public OrderStatus Status { get; set; }
        public DateTime RegDate { get; set; } = DateTime.Now;
        public bool IsPaid { get; set; }

        ///// <summary>
        ///// بازاریاب
        ///// </summary>
        //public int? ReffererUserId { get; set; }

        /// <summary>
        /// صاحب این سفارش
        /// </summary>
        public int? UserId { get; set; }
        /// <summary>
        /// راننده
        /// </summary>
        public int? DriverId { get; set; }

    }
    public class OrderFormDataModel
    {
        public OrderModel OrderInfo{ get; set; }

        public List<OrderDetail> OrderDetail { get; set; }
        public string Date { get; set; }
    }
    public class OrderDetail
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public int ProductPrice { get; set; }
    }

    public class SubmitOrderResult
    {
        public SubmitOrderResult()
        {

        }
        public SubmitOrderResult(int? orderId, string code=null)
        {
            OrderId = orderId;
            Code = code;
        }

        public int? OrderId { get; set; }
        public string Code{ get; set; }

    }
}
