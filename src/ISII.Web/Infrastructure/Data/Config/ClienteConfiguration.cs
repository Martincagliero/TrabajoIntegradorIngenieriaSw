using ISII.Web.Domain.ClienteAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISII.Web.Infrastructure.Data.Config;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
  public void Configure(EntityTypeBuilder<Cliente> builder)
  {
    builder.Property(cliente => cliente.Id)
      .HasValueGenerator<
        VogenIntIdValueGenerator<AppDbContext, Cliente, ClienteId>>()
      .HasVogenConversion()
        .HasSentinel(ClienteId.New)
      .IsRequired();

    builder.Property(cliente => cliente.Nombre)
      .HasMaxLength(100)
      .IsRequired();
  }
}