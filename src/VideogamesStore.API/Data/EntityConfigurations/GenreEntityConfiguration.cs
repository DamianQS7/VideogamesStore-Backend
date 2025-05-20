using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Data.EntityConfigurations;

public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(genre => genre.Name)
               .HasMaxLength(20);
    }
}
