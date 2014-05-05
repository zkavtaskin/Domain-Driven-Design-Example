using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace eCommerce.WebService.App_Start
{
    public class MappingConfig
    {
        public static void RegisterMapping()
        {
            Mapper.AddProfile(new eCommerce.ApplicationLayer.Map());
        }
    }
}