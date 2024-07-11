using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;
using IdentityServer.DTO.Exception;
using IdentityServer.DTO.Interfaces;
using IdentityServer.DTO.Interfaces.Identity;
using IdentityServer.DTO.Model;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace IdentityServer.Service.Commands.Auth
{
    public class AuthCommand : IRequest<AuthResponseDTO>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponseDTO>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IIdentityService _identityService;

        public AuthCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponseDTO> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SigninUserAsync(request.UserName, request.Password);

            if (!result)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var (userId, userName, email, roles) = await _identityService.GetUserDetailsAsync(await _identityService.GetUserIdAsync(request.UserName));

            var jwtToken = _tokenGenerator.GenerateJwtToken((userId, userName, roles));
            var refreshToken = _tokenGenerator.GenerateRefreshToken(jwtToken);
            return new AuthResponseDTO()
            {
                UserId = userId,
                Name = userName,
                AccessToken = jwtToken,
                RefreshToken = refreshToken
            };
        }
    }
}
