using Application.DTOs;
using Application.Features.Notes.Queries;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;
using UnitTests.Factories;

namespace UnitTests.Application.Features.Notes.Queries;

public class GetAllNotesTests
{
    private readonly INoteRepository repositoryMock;
    private readonly IMapper mapper;
    private readonly GetAllNotesRequestHandler handler;

    public GetAllNotesTests()
    {
        repositoryMock = Substitute.For<INoteRepository>();
        mapper = MapperFactory.Create();
        handler = new GetAllNotesRequestHandler(repositoryMock, mapper);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldReturnAllNotes()
    {
        // Arrange
        var note1 = Note.Create("test1");
        var note2 = Note.Create("test2");
        var notes = new List<Note>()
        {
            note1,
            note2
        };
        repositoryMock.GetAllNotesAsync().Returns(notes);
        var notesDtos = mapper.Map<List<NoteDto>>(notes);
        
        // Act
        var result = await handler.Handle(new GetAllNotesRequest(), CancellationToken.None);
        
        // Assert
        result.Should().HaveCount(2);
        result.Should().BeEquivalentTo(notesDtos);
    }
}