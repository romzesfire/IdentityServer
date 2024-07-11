using IdentityServer.DTO.Interfaces.Identity;
using MediatR;

namespace IdentityServer.Service.Commands.User.Create
{
    public class CreateVisitorUserCommand : IRequest<int>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }
    }

    public class CreateVisitorUserCommandHandler : IRequestHandler<CreateVisitorUserCommand, int>
    {
        private readonly IIdentityService _identityService;
        public CreateVisitorUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(CreateVisitorUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CreateUserAsync(request.UserName, 
                request.Password, request.ConfirmationPassword, request.Email, new List<string> { "Visitor" });
            return result.isSucceed ? 1 : 0;
        }
    }
}
