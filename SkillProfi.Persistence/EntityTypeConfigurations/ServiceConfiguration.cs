using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillProfi.Application.Common;
using SkillProfi.Domain;

namespace SkillProfi.Persistence.EntityTypeConfigurations;

public sealed class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
	public void Configure(EntityTypeBuilder<Service> builder)
	{
		builder.HasKey(service => service.Id);
		builder.Property(service => service.CreationDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired();
		builder.Property(service => service.UpdatingDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired(false);
		builder.Property(service => service.Title).HasMaxLength(FieldLimits.ServiceTitleMaxLength).IsRequired();
		builder.Property(service => service.Description).HasMaxLength(FieldLimits.ServiceDescriptionMaxLength).IsRequired();
	}
}
