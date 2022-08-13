using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogApplication.Api.Application.Interfaces.Repositories.User;
using BlogApplication.Api.Domain.Models;
using BlogApplication.Common.Infrastructure.Exceptions;
using BlogApplication.Common.Models.Queries;
using MediatR;

namespace BlogApplication.Api.Application.Features.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQueryRequest, UserDetailViewModel>
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IUserReadRepository userReadRepository, IMapper mapper)
        {
            _userReadRepository = userReadRepository;
            _mapper = mapper;
        }

        public async Task<UserDetailViewModel> Handle(GetUserDetailQueryRequest request, CancellationToken cancellationToken)
        {
            User dbUser = null;

            if (request.UserId != Guid.Empty)
            {
                dbUser = await _userReadRepository.GetByIdAsync(request.UserId);
                if (dbUser == null)
                    throw new DatabaseValidationException("User not found!");
            }

            else if (!string.IsNullOrEmpty(request.UserName))
            {
                dbUser = await _userReadRepository.GetSingleAsync(i => i.UserName == request.UserName);
                if (dbUser == null)
                    throw new DatabaseValidationException("User not found!");
            }



            return _mapper.Map<UserDetailViewModel>(dbUser);
        }
    }
}
