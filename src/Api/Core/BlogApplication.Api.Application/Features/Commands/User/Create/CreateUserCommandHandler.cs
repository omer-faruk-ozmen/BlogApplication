using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogApplication.Api.Application.Interfaces.Repositories.User;
using BlogApplication.Common;
using BlogApplication.Common.Events.User;
using BlogApplication.Common.Infrastructure;
using BlogApplication.Common.Infrastructure.Exceptions;
using BlogApplication.Common.Models.RequestModels;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository)
        {
            _mapper = mapper;
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }

        public async Task<Guid> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var existsUser = await _userReadRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

            if (existsUser is not null)
                throw new DatabaseValidationException("User already exists");

            var dbUser = _mapper.Map<Domain.Models.User>(request);

            dbUser.UserName = request.FirstName + request.LastName + new Random(100000);


            var rows = await _userWriteRepository.AddAsync(dbUser);

            //Email Changed/Created
            if (rows > 0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = request.EmailAddress
                };
                QueueFactory.SendMessageToExchange(exchangeName: BlogConstants.UserExchangeName,
                    exchangeType: BlogConstants.DefaultExchangeType,
                    queueName: BlogConstants.UserEmailChangedQueueName,
                    obj: @event);
            }

            return dbUser.Id;
        }
    }
}
