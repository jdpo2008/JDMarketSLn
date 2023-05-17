using JDMarketSLn.Domain.Common;
using JDMarketSLn.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Domain.Events.User
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
