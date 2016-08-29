using eCommerce.WebService.Models;
using eCommerce.ApplicationLayer.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eCommerce.WebService.Controllers
{
    /*
     * http://localhost:50300/api/cart/add?customerid=5D5020DA-47DF-4C82-A722-C8DEAF06AE23&productid=65D03D7E-E41A-49BC-8680-DC942BABD10A&quantity=1
     * http://localhost:50300/api/cart/getbyid?customerid=5D5020DA-47DF-4C82-A722-C8DEAF06AE23
     * http://localhost:50300/api/cart/remove?customerid=5D5020DA-47DF-4C82-A722-C8DEAF06AE23&productid=65d03d7e-e41a-49bc-8680-dc942babd10a&Quantity=1
     * http://localhost:50300/api/cart/checkout?customerid=5D5020DA-47DF-4C82-A722-C8DEAF06AE23
     */
    public class CartController : ApiController
    {
        readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet]
        public Response<CartDto> Add(Guid customerId, [FromUri]CartProductDto cartDto)
        {
            Response<CartDto> response = new Response<CartDto>();
            try
            {
                response.Object = this.cartService.Add(customerId, cartDto);
            }
            catch (Exception ex)
            {
                //log error
                response.Errored = true;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        [HttpGet]
        public Response<CartDto> GetById(Guid customerId)
        {
            Response<CartDto> response = new Response<CartDto>();
            try
            {
                response.Object = this.cartService.Get(customerId);
            }
            catch (Exception ex)
            {
                //log error
                response.Errored = true;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        [HttpGet]
        public Response<CartDto> Remove(Guid customerId, Guid productId)
        {
            Response<CartDto> response = new Response<CartDto>();
            try
            {
                response.Object = this.cartService.Remove(customerId, productId);
            }
            catch (Exception ex)
            {
                //log error
                response.Errored = true;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }


        [HttpGet]
        public Response<CheckOutResultDto> Checkout(Guid customerId)
        {
            Response<CheckOutResultDto> response = new Response<CheckOutResultDto>();
            try
            {
                response.Object = this.cartService.CheckOut(customerId);
            }
            catch (Exception ex)
            {
                //log error
                response.Errored = true;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

    }
}