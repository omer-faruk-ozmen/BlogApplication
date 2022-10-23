using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogApplication.Api.Application.Interfaces.Repositories.User;
using BlogApplication.Common.Infrastructure;
using BlogApplication.Common.Infrastructure.Exceptions;
using BlogApplication.Common.Models.Queries;
using BlogApplication.Common.Models.RequestModels.User;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogApplication.Api.Application.Features.Commands.User.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserViewModel>
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public LoginUserCommandHandler(IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository, IMapper mapper, IConfiguration configuration)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var dbUser = await _userReadRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

            if (dbUser == null)
                throw new DatabaseValidationException("User not found!");

            var pass = PasswordEncryptor.Encrypt(request.Password);

            if (dbUser.Password != pass)
                throw new DatabaseValidationException("Email or Password is wrong!");

            if (!dbUser.EmailConfirmed)
                throw new DatabaseValidationException("Email address is not confirmed yet!");

            var result = _mapper.Map<LoginUserViewModel>(dbUser);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,dbUser.Id.ToString()),
                new Claim(ClaimTypes.Email,dbUser.EmailAddress),
                new Claim(ClaimTypes.Name,dbUser.UserName),
                new Claim(ClaimTypes.GivenName,dbUser.FirstName),
                new Claim(ClaimTypes.Surname,dbUser.LastName),
            };

            result.Token = GenerateToken(claims);

            return result;
        }

        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthConfig:SecretKey"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiry = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(claims: claims, expires: expiry, signingCredentials: credentials,
                notBefore: DateTime.UtcNow);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
