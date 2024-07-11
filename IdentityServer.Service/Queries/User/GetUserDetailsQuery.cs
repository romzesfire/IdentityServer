using IdentityServer.DTO.Interfaces.Identity;
using IdentityServer.DTO.Model;
using MediatR;

namespace IdentityServer.Service.Queries.User
{
    public class GetUserDetailsQuery : IRequest<UserDetailsResponseDTO>
    {
        public string UserId { get; set; }
    }

    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsResponseDTO>
    {
        private readonly IIdentityService _identityService;

        public GetUserDetailsQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<UserDetailsResponseDTO> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var (userId, userName, email, roles ) = await _identityService.GetUserDetailsAsync(request.UserId);
            return new UserDetailsResponseDTO() { Id = userId, UserName = userName, Email = email, Roles = roles };
        }
    }
}
