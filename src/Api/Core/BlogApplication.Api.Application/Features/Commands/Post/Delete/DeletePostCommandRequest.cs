using MediatR;

namespace BlogApplication.Api.Application.Features.Commands.Post.Delete;

public class DeletePostCommandRequest:IRequest<bool>
{
    public Guid Id { get; set; }
}