﻿using IdentityServer.DTO.Interfaces.Identity;
using IdentityServer.DTO.Model;
using MediatR;

namespace IdentityServer.Service.Queries.User
{
    public class GetUserQuery : IRequest<List<UserResponseDTO>>
    {
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<UserResponseDTO>>
    {
        private readonly IIdentityService _identityService;

        public GetUserQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<List<UserResponseDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _identityService.GetAllUsersAsync();
            return users.Select(x => new UserResponseDTO()
            {
                Id = x.id,
                UserName = x.userName,
                Email = x.email
            }).ToList();
        }
    }
}
