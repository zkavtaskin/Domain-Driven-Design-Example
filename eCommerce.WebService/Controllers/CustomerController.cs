using eCommerce.WebService.Models;
using eCommerce.ApplicationLayer.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eCommerce.WebService.Controllers
{
    /*
     * Example Usage:
     * http://localhost:50300/api/customer/add?FirstName=john2&LastName=smith2&Email=john2.smith2@microsoft.com
     * http://localhost:50300/api/customer/Getbyid/5D5020DA-47DF-4C82-A722-C8DEAF06AE23
     * http://localhost:50300/api/customer/IsEmailAvailable?email=smith.john@microsoft.com
     * http://localhost:50300/api/customer/RemoveById/5D5020DA-47DF-4C82-A722-C8DEAF06AE23
     * http://localhost:50300/api/customer/update?id=5D5020DA-47DF-4C82-A722-C8DEAF06AE23&Email=smith.john@microsoft.com
     */
    public class CustomerController : ApiController
    {
        readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet]
        public Response<CustomerDto> Add([FromUri]CustomerDto customer)
        {
            Response<CustomerDto> response = new Response<CustomerDto>();
            try
            {
                response.Object = this.customerService.Add(customer);
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
        public Response<bool> IsEmailAvailable(string email)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                response.Object = this.customerService.IsEmailAvailable(email);
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
        public Response<CustomerDto> GetById(Guid id)
        {
            Response<CustomerDto> response = new Response<CustomerDto>();
            try
            {
                response.Object = this.customerService.Get(id);
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
        public Response RemoveById(Guid id)
        {
            Response response = new Response();
            try
            {
                this.customerService.Remove(id);
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
        public Response Update([FromUri]CustomerDto customer)
        {
            Response response = new Response();
            try
            {
                this.customerService.Update(customer);
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
