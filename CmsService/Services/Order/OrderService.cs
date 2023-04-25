using CmsApiService.Entities;
using CmsApiService.Models;
using CmsApiService.Models.Order;
using CmsApiService.Services.Dapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsApiService.Services
{
    public class OrderService : IOrderService
    { 

        private readonly IDapperService<object> _dapperService;
        private readonly ILogger<ProductService> _logger;
        public OrderService(  ILogger<ProductService> logger, Dapper.IDapperService<object> dapperService)
        {
            _dapperService = dapperService;
            _logger = logger;
        }

        public SubmitOrderResult SubmitOrder(OrderFormDataModel model)
        {

            try
            {
                string sql = $@"declare @orderId int;
                    INSERT INTO [dbo].[Order]
                        ([Status],[IsPaid],[RegDate],[FullName],[Tell],[RegistererTell],[InDate],[OutDate],[AdultCount],[ChildCount],[Hotel],[HotelAddress],[Vehicle],[VehicleNumber],[DepartureTime],[ArriveTime],ReservedDateFa, Code)
                    VALUES
                        (0,0,GETDATE(),@FullName ,@Tell ,@RegistererTell ,@InDate ,@OutDate ,@AdultCount, @ChildCount ,@Hotel ,@HotelAddress ,@Vehicle ,@VehicleNumber ,@DepartureTime ,@ArriveTime ,@ReservedDateFa,@Code)
                    select @orderId= @@IDENTITY;
                    ";
                foreach (var item in model.OrderDetail)
                {
                    sql += $@"insert into OrderDetail (OrderId, ProductId, Count,ProductPrice) VALUES (@orderId, {item.ProductId},{item.Count},{item.ProductPrice});";
                }
                sql += "select id as OrderId, Code from [Order] where id=@orderid ";
                var result = _dapperService.Query<SubmitOrderResult>(sql, new
                {
                    FullName = model.OrderInfo.FullName
                     , Tell = model.OrderInfo.Tell
                     , RegistererTell = model.OrderInfo.RegistererTell
                     , InDate = model.OrderInfo.InDate
                     , OutDate = model.OrderInfo.OutDate
                     , AdultCount = model.OrderInfo.AdultCount
                     , ChildCount = model.OrderInfo.ChildCount
                     , Hotel = model.OrderInfo.Hotel
                     , HotelAddress = model.OrderInfo.HotelAddress
                     , Vehicle = model.OrderInfo.Vehicle
                     , VehicleNumber = model.OrderInfo.VehicleNumber
                     , DepartureTime = model.OrderInfo.DepartureTime
                     , ArriveTime = model.OrderInfo.ArriveTime
                     , ReservedDateFa = model.Date.Replace("-", "/")
                     ,Code= Guid.NewGuid().ToString("N")

               }).FirstOrDefault();

                return result;
            }
            catch (Exception ex)
            {
                return new SubmitOrderResult(null);
            }
        }
    }
}
