using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillProfi.Domain;

namespace SkillProfi.Persistence.EntityTypeConfigurations;

public sealed class SocialMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
{
	public void Configure(EntityTypeBuilder<SocialMedia> builder)
	{
		builder.HasKey(socialMedia => socialMedia.Id);
		builder.Property(socialMedia => socialMedia.CreationDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired();
		builder.Property(socialMedia => socialMedia.UpdatingDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired(false);
		builder.Property(socialMedia => socialMedia.IconUrl).HasMaxLength(30).IsRequired();
		builder.Property(socialMedia => socialMedia.Link).HasMaxLength(30).IsRequired();
	}
}
