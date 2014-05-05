using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.WebService.Models
{
    public sealed class Response<TReturn> : Response
    {
        public TReturn Object { get; set; }
    }
}