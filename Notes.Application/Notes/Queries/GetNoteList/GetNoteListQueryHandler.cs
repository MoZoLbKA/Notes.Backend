using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Intrerfaces;
using Notes.Application.Notes.Queries.GetNoteDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryHandler : IRequestHandler<GetListNoteQuery,NoteListVm>
    {
        private readonly INotesDbContext _context;
        private readonly IMapper _mapper;

        public GetNoteListQueryHandler(INotesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<NoteListVm> Handle(GetListNoteQuery request, CancellationToken cancellationToken)
        {
            var notesQuery = await _context.Notes.Where(note => note.UserId == request.UserId).
                ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider).ToListAsync();
            return new NoteListVm() { Notes = notesQuery };
        }
    }
}
