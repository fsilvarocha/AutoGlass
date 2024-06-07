using AutoGlass.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoGlass.Infra.Data.Mappings;

public class ProdutoMap : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("Produto");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(250);

        builder.Property(x => x.IdFornecedor)
          .IsRequired()
          .HasColumnType("VARCHAR")
          .HasMaxLength(32);

        builder.Property(x => x.DescricaoFornecedor)
         .IsRequired()
         .HasColumnType("NVARCHAR")
         .HasMaxLength(200);

        builder.Property(x => x.CNPJFornecedor)
         .IsRequired()
         .HasColumnType("VARCHAR")
         .HasMaxLength(14);

        builder
          .Property(c => c.FabricadoEm)
          .HasDefaultValueSql("GETDATE()")
          .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore
          .Metadata.PropertySaveBehavior.Ignore);
    }
}
