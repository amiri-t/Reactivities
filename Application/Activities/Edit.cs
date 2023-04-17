using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity {get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly IMapper _mapper;
            private readonly DataContext _context;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);
                // in the line below, we used that code to update only the title, but with
                // smth called mapper we are gonna do the same thing but for every property
                // activity.Title = request.Activity.Title ?? activity.Title;
                _mapper.Map(request.Activity, activity);
                await _context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}