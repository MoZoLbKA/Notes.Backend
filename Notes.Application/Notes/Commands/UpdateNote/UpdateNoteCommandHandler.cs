using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Intrerfaces;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Unit>
    {
        private readonly INotesDbContext _context;
        public UpdateNoteCommandHandler(INotesDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notes.FirstOrDefaultAsync(note => note.Id == request.Id,
                cancellationToken);
            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }
            entity.Details = request.Details;
            entity.Title = request.Title;
            entity.EditTime = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }


    }
}
