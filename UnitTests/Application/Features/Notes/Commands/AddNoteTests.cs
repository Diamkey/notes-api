using Application.Features.Notes.Commands;
using Application.Mappings;
using Application.Repositories;
using AutoMapper;
using Castle.Core.Logging;
using Domain.Entities;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using UnitTests.Factories;

namespace UnitTests.Application.Features.Notes.Commands;

public sealed class AddNoteTests
{
    private readonly INoteRepository repositoryMock;
    private readonly IMapper mapper;
    private readonly AddNoteRequestHandler handler;

    public AddNoteTests()
    {
        repositoryMock = Substitute.For<INoteRepository>();
        mapper = MapperFactory.Create();
        handler = new AddNoteRequestHandler(repositoryMock, mapper);
    }

    [Fact]
    public async Task Handle_WithValidContent_ShouldAddNote()
    {
        // Arrange
        var content = "Test";
        var request = new AddNoteRequest(content);
        repositoryMock.AddNoteAsync(Arg.Any<Note>());
        
        // Act
        await handler.Handle(request, CancellationToken.None);
        
        // Assert
        await repositoryMock.Received(1).AddNoteAsync(Arg.Is<Note>(n => n.Content == content));
    }
}