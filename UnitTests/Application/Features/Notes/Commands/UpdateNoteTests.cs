using Application.Exceptions;
using Application.Features.Notes.Commands;
using Application.Repositories;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;

namespace UnitTests.Application.Features.Notes.Commands;

public class UpdateNoteTests
{
    private readonly INoteRepository repositoryMock;
    private readonly UpdateNoteRequestHandler handler;

    public UpdateNoteTests()
    {
        repositoryMock = Substitute.For<INoteRepository>();
        handler = new UpdateNoteRequestHandler(repositoryMock);        
    }

    [Fact]
    public async Task Handle_WithValidIdAndContent_ShouldUpdateNote()
    {
        // Arrange
        var note = Note.Create("initial content");
        var updatedContent = "updated content";
        var request = new UpdateNoteRequest(note.Id,  updatedContent);
        repositoryMock.GetNoteByIdAsync(note.Id).Returns(note);
        repositoryMock.UpdateNoteAsync(Arg.Any<Note>()).Returns(Task.CompletedTask);
        
        // Act
        await handler.Handle(request, CancellationToken.None);
        
        // Assert
        repositoryMock.Received(1).GetNoteByIdAsync(note.Id);
        repositoryMock.Received(1).UpdateNoteAsync(Arg.Is<Note>(n => 
            n.Content == updatedContent));
    }

    [Fact]
    public async Task Handle_WithInvalidId_ShouldThrowNoteByIdNotFoundException()
    {
        // Arrange
        var request = new UpdateNoteRequest(Guid.NewGuid(), "content");
        repositoryMock.GetNoteByIdAsync(Arg.Any<Guid>()).Returns((Note?)null);
        
        // Act
        var act = async () => await handler.Handle(request, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<NoteByIdNotFoundException>();
    }
}