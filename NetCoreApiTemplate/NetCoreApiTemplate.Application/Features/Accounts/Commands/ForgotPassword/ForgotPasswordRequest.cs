using AutoMapper;
using NetCoreApiTemplate.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Features.Accounts.Commands.ForgotPassword
{
    public class ForgotPasswordRequest : IMapFrom<ForgotPasswordCommand>
    {
        [Required]
        public string Email { get; set; }
        //public string Origin { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ForgotPasswordCommand, ForgotPasswordRequest>();
        }
    }
}
