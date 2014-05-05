using eCommerce.WebService.Models;
using eCommerce.ApplicationLayer.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace eCommerce.WebService.Controllers
{
    /*
     * Usage:
     * http://localhost:50300/api/product/add?name=iPhone5&quantity=6&cost=422&productcodeid=B2773EBF-CD0C-4F31-83E2-691973E32531
     * http://localhost:50300/api/product/get/65D03D7E-E41A-49BC-8680-DC942BABD10A
     */

    public class ProductController : ApiController
    {
        readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public Response<ProductDto> Add([FromUri]ProductDto productDto)
        {
            Response<ProductDto> response = new Response<ProductDto>();
            try
            {
                response.Object = this.productService.Add(productDto);
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
        public Response<ProductDto> Get(Guid id)
        {
            Response<ProductDto> response = new Response<ProductDto>();
            try
            {
                response.Object = this.productService.Get(id);
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
