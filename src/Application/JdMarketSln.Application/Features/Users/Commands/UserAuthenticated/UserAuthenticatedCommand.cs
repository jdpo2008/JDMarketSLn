using AutoMapper;
using JdMarketSln.Application.Exceptions;
using System.Security.Cryptography;
using JdMarketSln.Application.Wrappers;
using JdMarketSln.Domain.Entities;
using JdMarketSln.Domain.Settings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JdMarketSln.Application.Helpers;

namespace JdMarketSln.Application.Features.Users.Commands.UserAuthenticated
{
    public class UserAuthenticatedCommand : IRequest<Response<UserAuthenticatedDto>>
    {
        public string Email { get; set; } = default;
        public string Password { get; set; } = default;
    }

    public class UserAuthenticatedCommandHandler : IRequestHandler<UserAuthenticatedCommand, Response<UserAuthenticatedDto>>
    {
        private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;

        public UserAuthenticatedCommandHandler(UserManager<User> userManager, IOptions<JWTSettings> jwtSettings, IMapper mapper)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
        }
        public async Task<Response<UserAuthenticatedDto>> Handle(UserAuthenticatedCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<UserAuthenticatedRequest>(command);

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                throw new ApiException($"No Accounts Registered with {request.Email}.");
            }

            //var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                throw new ApiException($"Invalid Credentials for '{request.Email}'.");
            }

            if (!user.EmailConfirmed)
            {
                throw new ApiException($"Account Not Confirmed for '{request.Email}'.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            var response = _mapper.Map<UserAuthenticatedDto>(user);

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.RefreshToken = RandomTokenString();

            return new Response<UserAuthenticatedDto>(response);
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private async Task<JwtSecurityToken> GenerateJWToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id.ToString()),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
