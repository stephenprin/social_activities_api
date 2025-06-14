using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Core;
using Application.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Command
{
    public class UpdateAttendance
    {
        public class Command: IRequest<Result<Unit>>
        {
            public string Id { get; set; }
        }
        public class Handler(IUserAccessor userAccessor, AppDbContext context) : IRequestHandler<Command, Result<Unit>>
        {
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities.Include(x => x.Attendees)
                      .ThenInclude(x => x.User).SingleOrDefaultAsync(x => x.Id==request.Id, cancellationToken);
                   if(activity == null)
                {
                    return Result<Unit>.Failure("Activity not found", 404);
                }
                   var user= await userAccessor.GetUserAsync();
                var attendee = activity.Attendees.FirstOrDefault(x => x.UserId == user.Id);
                var isHost = activity.Attendees.Any(x => x.UserId == user.Id && x.IsHost);

                if (attendee != null) {
                    if (isHost) activity.IsCancelled = !activity.IsCancelled;
                    else activity.Attendees.Remove(attendee);
                }
                else
                {
                    activity.Attendees.Add(new Domain.ActivityAttendee
                    {
                        User = user,
                        Activity = activity,
                        IsHost = false
                    });
                }

                var result = await context.SaveChangesAsync(cancellationToken) > 0;
                return result ? Result<Unit>.Success(Unit.Value)
                    : Result<Unit>.Failure("Problem updating attendance", 400);

            }
        }
    }
}
