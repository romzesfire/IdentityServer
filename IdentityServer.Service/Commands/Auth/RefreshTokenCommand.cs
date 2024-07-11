using IdentityServer.DTO.Exception;
using IdentityServer.DTO.Interfaces;
using IdentityServer.DTO.Interfaces.Identity;
using IdentityServer.DTO.Model;
using IdentityServer.Service.Services;
using MediatR;

namespace IdentityServer.Service.Commands.Auth
{
    public class RefreshTokenCommand : IRequest<RefreshResponseDTO>
    {
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
    }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshResponseDTO>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IIdentityService _identityService;
        private readonly ITokenValidator _tokenValidator;
        
        public RefreshTokenCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator, ITokenValidator tokenValidator)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
            _tokenValidator = tokenValidator;
        }


        public async Task<RefreshResponseDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken) || string.IsNullOrWhiteSpace(request.RefreshToken)
                                                               || !request.AccessToken.StartsWith(
                                                                   request.RefreshToken.Split('.')[1]))
            {
                throw new BadRequestException("Invalid or empty tokens");
            }

            var userId = _tokenValidator.GetUserIdFromToken(request.AccessToken);
            
            if (userId == null)
            {
                throw new BadRequestException("Invalid access token");
            }
            var (id, userName, email, roles) = await _identityService.GetUserDetailsAsync(userId);

            var jwtToken = _tokenGenerator.GenerateJwtToken((userId, userName, roles));
            var refreshToken = _tokenGenerator.GenerateRefreshToken(jwtToken);
            
            return new RefreshResponseDTO
            {
                AccessToken = jwtToken,
                RefreshToken = refreshToken
            };
        }
    }
}
