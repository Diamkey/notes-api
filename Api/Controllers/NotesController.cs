using Application.Features.Notes.Commands;
using Application.Features.Notes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NotesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllNotes()
    {
        var notes = await mediator.Send(new GetAllNotesRequest());
        return Ok(notes);
    }

    [HttpPost]
    public async Task<IActionResult> AddNote([FromBody] AddNoteRequest request)
    {
        await mediator.Send(request);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteNote([FromBody] DeleteNoteRequest request)
    {
        await mediator.Send(request);
        return NoContent();
    }
}