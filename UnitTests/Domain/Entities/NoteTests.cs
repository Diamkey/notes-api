using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using UnitTests.Fakes;

namespace UnitTests.Domain.Entities;

public class NoteTests
{
        [Fact]
        public void Create_WithValidContent_ShouldCreateNote()
        {
            // Arrange
            var content = "Test note content";
            
            // Act
            var note = Note.Create(content);
            
            // Assert
            note.Should().NotBeNull();
            note.Id.Should().NotBeEmpty();
            note.Content.Should().Be(content);
            note.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void Create_WithWhitespaceContent_ShouldCreateNoteWithTrimmedContent()
        {
            // Arrange
            var content = "  Test note content  ";

            // Act
            var note = Note.Create(content);

            // Assert
            note.Content.Should().Be("Test note content");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Create_WithEmptyOrWhitespaceContent_ShouldThrowNoteContentEmptyException(string content)
        {
            // Arrange
            // Act
            var act = () => Note.Create(content);
            
            // Assert
            act.Should().Throw<NoteContentEmptyException>();
        }
        
        [Fact]
        public void Create_WithTimeProvider_ShouldCreateNoteWithTimeProvider()
        {
            // Arrange
            var content = "Test note content";
            var fixedTime = new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero);
            var timeProvider = new FakeTimeProvider(fixedTime);
            
            // Act
            var note = Note.Create(content, timeProvider);
            
            // Assert
            note.CreatedAt.Should().BeCloseTo(fixedTime, TimeSpan.FromSeconds(1));
        }
}