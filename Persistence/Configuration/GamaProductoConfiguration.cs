using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configuration
{
    public class GamaProductoConfiguration : IEntityTypeConfiguration<GamaProducto>
    {
        public void Configure(EntityTypeBuilder<GamaProducto> builder)
        {
            builder.Property(e => e.Id).HasColumnType("int");
            builder.HasKey(e => e.Gama).HasName("PRIMARY");

            builder.ToTable("gama_producto");

            builder.Property(e => e.Gama)
                .HasMaxLength(50)
                .HasColumnName("gama");
            builder.Property(e => e.DescripcionHtml)
                .HasColumnType("text")
                .HasColumnName("descripcion_html");
            builder.Property(e => e.DescripcionTexto)
                .HasColumnType("text")
                .HasColumnName("descripcion_texto");
            builder.Property(e => e.Imagen)
                .HasMaxLength(256)
                .HasColumnName("imagen");
        }
    }
}