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
using BlogApplication.Common.Models.RequestModels.User;
using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.User.Update
{


    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, Guid>
    {

        private readonly IMapper _mapper;
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserReadRepository userReadRepository, IUserWriteRepository userWriteRepository)
        {
            _mapper = mapper;
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }

        public async Task<Guid> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var dbUser = await _userReadRepository.GetByIdAsync(request.Id);


            if (dbUser == null)
                throw new DatabaseValidationException("User not found!");

            var dbEmailAddress = dbUser.EmailAddress;
            var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;

            _mapper.Map(request, dbUser);



            var rows = await _userWriteRepository.UpdateAsync(dbUser);

            //Check if email changed
            if (emailChanged && rows > 0)
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
            dbUser.EmailConfirmed = false;

            await _userWriteRepository.UpdateAsync(dbUser);

            return dbUser.Id;
        }
    }
}
