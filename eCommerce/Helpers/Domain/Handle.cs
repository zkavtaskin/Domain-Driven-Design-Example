using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.Helpers.Domain
{
    public interface Handles<T> 
        where T : DomainEvent
    {
        void Handle(T args); 
    } 
}
