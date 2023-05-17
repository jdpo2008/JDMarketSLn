using AutoMapper;
using System.Security.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using JDMarketSLn.Domain.Settings;
using JDMarketSLn.Application.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using JDMarketSLn.Application.Common.Helpers;
using JDMarketSLn.Application.Common.Exceptions;
using JDMarketSLn.Domain.Entities.Identity;

namespace JDMarketSLn.Application.Features.Accounts.Commands.Authenticated
{
    public class AuthenticatedCommand : IRequest<AuthenticatedDto>
    {
        [Required]
        public string Email { get; set; } = default;
        [Required]
        public string Password { get; set; } = default;
    }

    public class UserAuthenticatedCommandHandler : IRequestHandler<AuthenticatedCommand, AuthenticatedDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<User> _signInManager;
        private readonly JWTSettings _jwtSettings;
        private readonly IMapper _mapper;
        //private readonly ILoggerService _logger;
        //private readonly IEmailService _emailService;

        public UserAuthenticatedCommandHandler(UserManager<ApplicationUser> userManager, IOptions<JWTSettings> jwtSettings, IMapper mapper)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
            //_emailService = emailService;
        }
        public async Task<AuthenticatedDto> Handle(AuthenticatedCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<AuthenticatedRequest>(command);

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                //_logger.LogWarning($"Invalid Credentials for {request.Email}, Username o Password Incorrects.");
                throw new ApiException($"Invalid Credentials for {request.Email}, Username o Password Incorrects.");
            }

            //var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                //_logger.LogWarning($"Invalid Credentials for '{request.Email}', Username o Password Incorrects.");
                throw new ApiException($"Invalid Credentials for '{request.Email}', Username o Password Incorrects.");
            }

            if (!user.EmailConfirmed)
            {
                //_logger.LogWarning($"Account Not Confirmed for '{request.Email}'.");
                throw new ApiException($"Account Not Confirmed for '{request.Email}'.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            //var refreshToken = GenerateRefreshToken(IpHelper.GetIpAddress());

            TokenResponse token = new TokenResponse();
            //token.RefresToken = refreshToken.Token;
            token.AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            token.Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes);

            var response = _mapper.Map<AuthenticatedDto>(user);
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.TwoFactorEnabled = user.TwoFactorEnabled;
            response.Token = token;

            //await _emailService.SendWelcomeEmailAsync(user.UserName, user.Email, cancellationToken);

            //var email = await _emailService.SendAsync(new EmailRequest() { To = user.Email, From = "informes@eduka.com.pe", Subject = "Inicio de Sesion correcto", Body = "Esta es el cuerpo del correo para probar el envio" });

            //_logger.LogInfo($"Authenticated {user.Email}");

            return response;
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
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
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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

        //private RefreshToken GenerateRefreshToken(string ipAddress)
        //{
        //    return new RefreshToken
        //    {
        //        Token = RandomTokenString(),
        //        Expires = DateTime.UtcNow.AddDays(7),
        //        Created = DateTime.UtcNow,
        //        CreatedByIp = ipAddress
        //    };
        //}
    }
}
