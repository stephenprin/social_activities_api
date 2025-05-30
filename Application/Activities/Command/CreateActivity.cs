using Application.Activities.DTOs;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities.Command;

public class CreateActivity
{
    public class Command : IRequest<string>
    {
        public required CreateActivityDto createActivityDto { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
        
            var activity = mapper.Map<Activity>(request.createActivityDto);
            context.Activities.Add(activity);
            await context.SaveChangesAsync(cancellationToken);

            return activity.Id;
        }
    }
}
