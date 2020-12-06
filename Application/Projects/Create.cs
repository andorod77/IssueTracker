using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Projects
{
  public class Create
  {
    public class Command : IRequest
    {
      public Guid Id { get; set; }
      public string ProjName { get; set; }
      public DateTime DateCreated { get; set; }
      public DateTime? Deadline { get; set; }
      public int CreatedBy { get; set; }
      public int Status { get; set; }

      public int NoOfTasks { get; set; }
      public int ActiveTasks { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly DataContext _context;
      public Handler(DataContext context)
      {
        _context = context;
      }

      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        var project = new Project {
            Id = request.Id,
            ProjName = request.ProjName,
            DateCreated = request.DateCreated,
            Deadline = request.Deadline,
            CreatedBy = request.CreatedBy,
            Status = request.Status,
            NoOfTasks = request.NoOfTasks,
            ActiveTasks = request.ActiveTasks
        };

        _context.Projects.Add(project);

        var success = await _context.SaveChangesAsync() > 0;

        if (success) return Unit.Value;

        throw new Exception("Problem saving new project");
      }
    }


  }
}