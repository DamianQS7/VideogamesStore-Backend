using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideogamesStore.API.Models;

namespace VideogamesStore.API.Data.EntityConfigurations;

public class GameEntityConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.Property(game => game.Name)
               .HasMaxLength(50);

        builder.Property(game => game.Description)
               .HasMaxLength(500);

        builder.Property(game => game.Name)
               .HasPrecision(5, 2);
    }
}
