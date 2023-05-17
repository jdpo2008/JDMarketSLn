using AutoMapper;
using JDMarketSLn.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDMarketSLn.Application.Features.Accounts.Commands.ChangePassword
{
    public class ChangePasswordRequest : IMapFrom<ChangePasswordCommand>
    {

        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangePasswordCommand, ChangePasswordRequest>();

        }
    }
}
