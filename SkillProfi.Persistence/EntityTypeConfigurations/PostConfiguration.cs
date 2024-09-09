using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillProfi.Application.Common;
using SkillProfi.Domain;

namespace SkillProfi.Persistence.EntityTypeConfigurations;

public sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{
	public void Configure(EntityTypeBuilder<Post> builder)
	{
		builder.HasKey(post => post.Id);
		builder.Property(post => post.CreationDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired();
		builder.Property(post => post.UpdatingDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired(false);
		builder.Property(post => post.Title).HasMaxLength(FieldLimits.PostTitleMaxLength).IsRequired();
		builder.Property(post => post.ImageUrl).HasMaxLength(FieldLimits.PostImageUrlMaxLength).IsRequired(false);
		builder.Property(post => post.Description).HasMaxLength(FieldLimits.PostDescriptionMaxLength).IsRequired();
	}
}
