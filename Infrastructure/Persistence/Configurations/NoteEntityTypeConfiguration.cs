using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal sealed class NoteEntityTypeConfiguration
    : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);
        
        builder.Property(n => n.Content).IsRequired();
        
        builder.Property(n => n.CreatedAt).IsRequired();
    }
}