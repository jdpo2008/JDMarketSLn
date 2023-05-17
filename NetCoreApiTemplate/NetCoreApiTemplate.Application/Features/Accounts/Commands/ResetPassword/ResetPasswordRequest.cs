using AutoMapper;
using JDMarketSLn.Application.Common.Mappings;

namespace JDMarketSLn.Application.Features.Accounts.Commands.ResetPassword
{
    public class ResetPasswordRequest : IMapFrom<ResetPasswordCommand>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ResetPasswordCommand, ResetPasswordRequest>();
        }
    }
}
