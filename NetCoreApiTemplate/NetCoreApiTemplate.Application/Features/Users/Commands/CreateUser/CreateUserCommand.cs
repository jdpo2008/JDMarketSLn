using AutoMapper;
using AutoMapper.Configuration.Annotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using NetCoreApiTemplate.Application.Common.Exceptions;
using NetCoreApiTemplate.Application.Common.Interfaces;
using NetCoreApiTemplate.Application.Common.Models;
using NetCoreApiTemplate.Application.Common.Request.Email;
using NetCoreApiTemplate.Domain.Entities.Identity;
using NetCoreApiTemplate.Domain.Events.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace NetCoreApiTemplate.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Result>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public string? Origin { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper, IEmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var validatedUser = await _userManager.FindByEmailAsync(request.Email);
            if (validatedUser != null)
            {
                List<string> errors = new List<string>
                {
                  $"User {request.Email} exist in bd. LogIn in the system"
                };

                return Result.Failure(errors, StatusCodes.Status400BadRequest);

            }

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                List<string> errors = new List<string>
                {
                  $"Username '{request.UserName}' is already taken."
                };

                return Result.Failure(errors, StatusCodes.Status400BadRequest);
            }

            var defaultUser = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,

            };

            defaultUser.AddDomainEvent(new CreateUserEvent(defaultUser));

            var result = await _userManager.CreateAsync(defaultUser, request.PasswordHash);

            if (!result.Succeeded)
            {
                var error = "";
                foreach (var item in result.Errors)
                {
                    error += item.Description + " ";
                }
                throw new ApiException($"{error}");
            }

            await _userManager.AddToRoleAsync(defaultUser, "Basic");

            var verificationUri = await SendVerificationEmail(defaultUser, request.Origin ?? "");

            await _emailService.SendConfirmationEmailAsync(request.Email, $"{request.FirstName} {request.LastName}", verificationUri, cancellationToken);

            return Result.Success(StatusCodes.Status201Created);
        }

        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/v1/Account/confirm-email/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id.ToString());
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            return verificationUri;
        }
    }
}
