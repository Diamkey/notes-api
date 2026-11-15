using Application.Exceptions;
using Application.Features.Notes.Commands;
using Application.Repositories;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;

namespace UnitTests.Application.Features.Notes.Commands;

public class DeleteNoteTests
{
    private readonly INoteRepository repositoryMock;
    private readonly DeleteNoteRequestHandler handler;

    public DeleteNoteTests()
    {
        repositoryMock = Substitute.For<INoteRepository>();
        handler = new DeleteNoteRequestHandler(repositoryMock);
    }

    [Fact]
    public async Task Handle_WithValidId_ShouldDeleteNote()
    {
        // Arrange
        var note = Note.Create("test");
        var request = new DeleteNoteRequest(note.Id);
        repositoryMock.GetNoteByIdAsync(note.Id).Returns(note);
        repositoryMock.DeleteNoteAsync(note).Returns(Task.CompletedTask);
        
        // Act
        await handler.Handle(request, CancellationToken.None);
        
        // Assert
        await repositoryMock.Received(1).GetNoteByIdAsync(note.Id);
        await repositoryMock.Received(1).DeleteNoteAsync(note);
    }

    [Fact]
    public async Task Handle_WithInvalidId_ShouldThrowNoteByIdNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new DeleteNoteRequest(id);
        repositoryMock.GetNoteByIdAsync(id).Returns(Task.FromResult<Note?>(null));
        
        // Act
        var act = async () => await handler.Handle(request, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<NoteByIdNotFoundException>();
    }
}