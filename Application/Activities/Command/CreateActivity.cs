using Application.Activities.DTOs;
using Application.Interface;
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

    public class Handler(AppDbContext context, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await userAccessor.GetUserAsync();

            var activity = mapper.Map<Activity>(request.createActivityDto);
            context.Activities.Add(activity);
            var attendee= new ActivityAttendee
            {
                UserId = user.Id,
                ActivityId = activity.Id,
                IsHost = true
            };
            activity.Attendees.Add(attendee);
            await context.SaveChangesAsync(cancellationToken);
            return activity.Id;
        }
    }
}
