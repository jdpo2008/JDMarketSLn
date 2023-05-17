using NetCoreApiTemplate.Domain.Common;
using NetCoreApiTemplate.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Domain.Events.User
{
    public class CreateUserEvent : BaseEvent
    {

        public CreateUserEvent(ApplicationUser User)
        {
            this.User = User;  
        }

        public ApplicationUser User { get; }
    }
}
