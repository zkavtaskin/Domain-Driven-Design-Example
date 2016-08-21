using eCommerce.ApplicationLayer.History;
using eCommerce.WebService.Models;
using System;
using System.Web.Http;

namespace eCommerce.WebService.Controllers
{
    public class HistoryController : ApiController
    {
        readonly IHistoryService historyService;

        public HistoryController(IHistoryService historyService)
        {
            this.historyService = historyService;
        }

        [HttpGet]
        public Response<HistoryDto> All()
        {
            Response<HistoryDto> response = new Response<HistoryDto>();
            try
            {
                response.Object = this.historyService.GetHistory();
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
