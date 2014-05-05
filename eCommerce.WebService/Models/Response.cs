using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.WebService.Models
{
    public class Response
    {
        public bool Errored { get; set; }
        public string ErrorMessage { get; set; }
    }
}