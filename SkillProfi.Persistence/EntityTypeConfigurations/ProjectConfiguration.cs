using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillProfi.Application.Common;
using SkillProfi.Domain;

namespace SkillProfi.Persistence.EntityTypeConfigurations;

public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
	public void Configure(EntityTypeBuilder<Project> builder)
	{
		builder.HasKey(project => project.Id);
		builder.Property(project => project.CreationDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired();
		builder.Property(project => project.UpdatingDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired(false);
		builder.Property(project => project.Title).HasMaxLength(FieldLimits.ProjectTitleMaxLength).IsRequired();
		builder.Property(project => project.ImageUrl).HasMaxLength(FieldLimits.ProjectImageUrlMaxLength).IsRequired(false);
		builder.Property(project => project.Description).HasMaxLength(FieldLimits.ProjectDescriptionMaxLength).IsRequired();
	}
}
