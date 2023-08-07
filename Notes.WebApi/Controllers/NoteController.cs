using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;

namespace Notes.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : BaseController
    {
        private readonly IMapper _mapper;
        public NoteController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var listQuery = new GetListNoteQuery()
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(listQuery);
            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDetailsVm>> GetDetailsNote(Guid id)
        {
            var query = new GetNoteDetailsQuery()
            {
                Id = id,
                UserId = UserId

            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto noteDto)
        {
            var command = _mapper.Map<CreateNoteCommand>(noteDto);
            command.UserId = UserId;
            var noteId = await Mediator.Send(command);
            return Created("id",noteId);
        }
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateNoteDto noteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(noteDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand()
            {
                UserId = UserId,
                Id = id,
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
