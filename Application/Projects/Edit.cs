using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Projects
{
  public class Edit
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
        var project = await _context.Projects.FindAsync(request.Id);
        
        if (project == null)
        throw new Exception("Project could not be found");

        project.ProjName = request.ProjName ?? project.ProjName;
        project.CreatedBy = (request.CreatedBy > 0) ? request.CreatedBy : project.CreatedBy;
        project.Deadline = request.Deadline ?? project.Deadline;
        project.Status = (request.Status > 0) ? request.Status : project.Status;
        project.NoOfTasks = (request.NoOfTasks > 0) ? request.NoOfTasks : project.NoOfTasks;
        project.ActiveTasks = (request.ActiveTasks > 0) ? request.ActiveTasks : project.ActiveTasks;

        var success = await _context.SaveChangesAsync() > 0;

        if (success) return Unit.Value;

        throw new Exception("Problem saving new project");
      }
    }


  }
}